//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damage class.
/// </summary>
[System.Serializable]
public class RCC_Damage {

    [HideInInspector] public RCC_CarControllerV4 carController;     //  Car controller.
    public bool automaticInstallation = true;       //  If set to enabled, all parts of the vehicle will be processed. If disabled, each part can be selected individually.
    private bool initialized = false;       //  Initialized?

    // Mesh deformation
    [Space()]
    [Header("Mesh Deformation")]
    public bool meshDeformation = true;
    public DeformationMode deformationMode = DeformationMode.Fast;

    public enum DeformationMode { Accurate, Fast }
    [Range(1, 100)] public int damageResolution = 100;      //  Resolution of the deformation.
    public LayerMask damageFilter = -1;     // LayerMask filter. Damage will be taken from the objects with these layers.
    public float damageRadius = .75f;        // Verticies in this radius will be effected on collisions.
    public float damageMultiplier = 1f;     // Damage multiplier.
    public float maximumDamage = .5f;       // Maximum Vert Distance For Limiting Damage. 0 Value Will Disable The Limit.
    private readonly float minimumCollisionImpulse = .5f;       // Minimum collision force.
    private readonly float minimumVertDistanceForDamagedMesh = .002f;        // Comparing Original Vertex Positions Between Last Vertex Positions To Decide Mesh Is Repaired Or Not.

    public struct OriginalMeshVerts { public Vector3[] meshVerts; }     // Struct for Original Mesh Verticies positions.
    public struct OriginalWheelPos { public Vector3 wheelPosition; public Quaternion wheelRotation; }
    public struct MeshCol { public Collider col; public bool created; }

    public OriginalMeshVerts[] originalMeshData;        // Array for struct above.
    public OriginalMeshVerts[] damagedMeshData;     // Array for struct above.
    public OriginalWheelPos[] originalWheelData;       // Array for struct above.
    public OriginalWheelPos[] damagedWheelData;        // Array for struct above.

    [Space()]
    public bool repairNow = false;      // Repairing now.
    public bool repaired = true;        // Returns true if vehicle is completely repaired.
    private bool deformingNow = false;      //  Deforming the mesh now.
    private bool deformed = true;        //  Returns true if vehicle is completely deformed.
    private float deformationTime = 0f;     //  Timer for deforming the vehicle. 

    [Space()]
    public bool recalculateNormals = true;      //  Recalculate normals while deforming / restoring the mesh.
    public bool recalculateBounds = true;       //  Recalculate bounds while deforming / restoring the mesh.

    // Wheel deformation
    [Space()]
    [Header("Wheel Deformation")]
    public bool wheelDamage = true;     //	Use wheel damage.
    public float wheelDamageRadius = 1f;        //   Wheel damage radius.
    public float wheelDamageMultiplier = 1f;        //  Wheel damage multiplier.
    public bool wheelDetachment = true;     //	Use wheel detachment.

    // Light deformation
    [Space()]
    [Header("Light Deformation")]
    public bool lightDamage = true;     //	Use light damage.
    public float lightDamageRadius = .5f;        //Light damage radius.
    public float lightDamageMultiplier = 1f;        //Light damage multiplier.

    // Part deformation
    [Space()]
    [Header("Part Deformation")]
    public bool partDamage = true;     //	Use part damage.
    public float partDamageRadius = 1f;        //Light damage radius.
    public float partDamageMultiplier = 1f;        //Light damage multiplier.

    [Space()]
    public MeshFilter[] meshFilters;    //  Collected mesh filters.
    public RCC_DetachablePart[] detachableParts;        //  Collected detachable parts.
    public RCC_Light[] lights;      //  Collected lights.
    public RCC_WheelCollider[] wheels;      //  Collected wheels.

    private Vector3 contactPoint = Vector3.zero;
    private Vector3[] contactPoints;

    public RCC_Octree[] octrees;

    /// <summary>
    /// Collecting all meshes and detachable parts of the vehicle.
    /// </summary>
    public void Initialize(RCC_CarControllerV4 _carController) {

        // Getting the main car controller
        carController = _carController;

        if (automaticInstallation) {

            if (meshDeformation)
                CollectProperMeshFilters();

            if (lightDamage)
                GetLights(carController.GetComponentsInChildren<RCC_Light>());

            if (partDamage)
                GetParts(carController.GetComponentsInChildren<RCC_DetachablePart>());

            if (wheelDamage)
                GetWheels(carController.GetComponentsInChildren<RCC_WheelCollider>());

        }

        initialized = true;

    }

    /// <summary>
    /// Collects all proper mesh filters excluding non-readable meshes and wheel meshes.
    /// </summary>
    private void CollectProperMeshFilters() {

        MeshFilter[] allMeshFilters = carController.gameObject.GetComponentsInChildren<MeshFilter>(true);
        List<MeshFilter> properMeshFilters = new List<MeshFilter>();

        // Create a HashSet for faster exclusion of wheel transforms
        HashSet<Transform> wheelTransforms = new HashSet<Transform> {

        carController.FrontLeftWheelTransform,
        carController.FrontRightWheelTransform,
        carController.RearLeftWheelTransform,
        carController.RearRightWheelTransform

    };

        // Track non-readable meshes for a summarized error message
        List<string> nonReadableMeshes = new List<string>();

        foreach (MeshFilter mf in allMeshFilters) {

            if (mf.mesh != null) {

                // If the mesh is not readable, store the error message and skip
                if (!mf.mesh.isReadable) {

                    nonReadableMeshes.Add(mf.transform.name);
                    continue;

                }

                // Exclude any meshes that belong to the wheel transforms
                if (!wheelTransforms.Contains(mf.transform) && !IsDescendantOfAny(mf.transform, wheelTransforms))
                    properMeshFilters.Add(mf);

            }

        }

        // Log a summarized error message for non-readable meshes, if any
        if (nonReadableMeshes.Count > 0)
            Debug.LogError("The following meshes are not deformable due to Read/Write being disabled: " + string.Join(", ", nonReadableMeshes));

        GetMeshes(properMeshFilters.ToArray());

    }

    /// <summary>
    /// Checks if the transform is a descendant of any transform in the given set.
    /// </summary>
    private bool IsDescendantOfAny(Transform transform, HashSet<Transform> potentialParents) {

        foreach (var parent in potentialParents) {

            if (transform.IsChildOf(parent))
                return true;

        }

        return false;

    }

    /// <summary>
    /// Gets all meshes.
    /// </summary>
    /// <param name="allMeshFilters"></param>
    public void GetMeshes(MeshFilter[] allMeshFilters) {

        meshFilters = allMeshFilters;

    }

    /// <summary>
    /// Gets all lights.
    /// </summary>
    /// <param name="allLights"></param>
    public void GetLights(RCC_Light[] allLights) {

        lights = allLights;

    }

    /// <summary>
    /// Gets all detachable parts.
    /// </summary>
    /// <param name="allParts"></param>
    public void GetParts(RCC_DetachablePart[] allParts) {

        detachableParts = allParts;

    }

    /// <summary>
    /// Gets all wheels
    /// </summary>
    /// <param name="allWheels"></param>
    public void GetWheels(RCC_WheelCollider[] allWheels) {

        wheels = allWheels;

    }

    /// <summary>
    /// We will be using two structs for deformed sections. Original part struction, and deformed part struction. 
    /// All damaged meshes and wheel transforms will be using these structs. At this section, we're creating them with original struction.
    /// </summary>
    private void CheckMeshData() {

        originalMeshData = new OriginalMeshVerts[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
            originalMeshData[i].meshVerts = meshFilters[i].mesh.vertices;

        damagedMeshData = new OriginalMeshVerts[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
            damagedMeshData[i].meshVerts = meshFilters[i].mesh.vertices;

    }

    /// <summary>
    /// We will be using two structs for deformed sections. Original part struction, and deformed part struction. 
    /// All damaged meshes and wheel transforms will be using these structs. At this section, we're creating them with original struction.
    /// </summary>
    private void CheckWheelData() {

        originalWheelData = new OriginalWheelPos[wheels.Length];

        for (int i = 0; i < wheels.Length; i++) {

            originalWheelData[i].wheelPosition = wheels[i].transform.localPosition;
            originalWheelData[i].wheelRotation = wheels[i].transform.localRotation;

        }

        damagedWheelData = new OriginalWheelPos[wheels.Length];

        for (int i = 0; i < wheels.Length; i++) {

            damagedWheelData[i].wheelPosition = wheels[i].transform.localPosition;
            damagedWheelData[i].wheelRotation = wheels[i].transform.localRotation;

        }

    }

    /// <summary>
    /// Moving deformed vertices to their original positions while repairing.
    /// </summary>
    public void UpdateRepair() {

        if (!carController)
            return;

        if (!initialized)
            return;

        //  If vehicle is not repaired completely, and repairNow is enabled, restore all deformed meshes to their original structions.
        if (!repaired && repairNow) {

            if (originalMeshData == null || originalMeshData.Length < 1)
                CheckMeshData();

            int k;
            repaired = true;

            //  If deformable mesh is still exists, get all verticies of the mesh first. And then move all single verticies to the original positions. If verticies are close enough to the original
            //  position, repaired = true;
            for (k = 0; k < meshFilters.Length; k++) {

                MeshFilter currentMeshFilter = meshFilters[k];

                if (currentMeshFilter != null && currentMeshFilter.mesh != null) {

                    //  Get all verticies of the mesh first.
                    Vector3[] vertices = currentMeshFilter.mesh.vertices;

                    for (int i = 0; i < vertices.Length; i++) {

                        Vector3 originalMeshVertex = originalMeshData[k].meshVerts[i];

                        //  And then move all single verticies to the original positions
                        if (deformationMode == DeformationMode.Accurate)
                            vertices[i] += (originalMeshVertex - vertices[i]) * (Time.deltaTime * 5f);
                        else
                            vertices[i] = (originalMeshVertex);

                        //  If verticies are close enough to their original positions, repaired = true;
                        if ((originalMeshVertex - vertices[i]).sqrMagnitude >= (minimumVertDistanceForDamagedMesh * minimumVertDistanceForDamagedMesh))
                            repaired = false;

                    }

                    //  We were using the variable named "vertices" above, therefore we need to set the new verticies to the damaged mesh data.
                    //  Damaged mesh data also restored while repairing with this proccess.
                    damagedMeshData[k].meshVerts = vertices;

                    //  Setting new verticies to the all meshes. Recalculating normals and bounds, and then optimizing. This proccess can be heavy for high poly meshes.
                    //  You may want to disable last three lines.
                    currentMeshFilter.mesh.SetVertices(vertices);

                    if (recalculateNormals)
                        currentMeshFilter.mesh.RecalculateNormals();

                    if (recalculateBounds)
                        currentMeshFilter.mesh.RecalculateBounds();

                }

            }

            for (k = 0; k < wheels.Length; k++) {

                if (wheels[k] != null) {

                    //  Get all verticies of the mesh first.
                    Vector3 wheelPos = wheels[k].transform.localPosition;

                    //  And then move all single verticies to the original positions
                    if (deformationMode == DeformationMode.Accurate)
                        wheelPos += (originalWheelData[k].wheelPosition - wheelPos) * (Time.deltaTime * 5f);
                    else
                        wheelPos += (originalWheelData[k].wheelPosition - wheelPos);

                    //  If verticies are close enough to their original positions, repaired = true;
                    if ((originalWheelData[k].wheelPosition - wheelPos).sqrMagnitude >= (minimumVertDistanceForDamagedMesh * minimumVertDistanceForDamagedMesh))
                        repaired = false;

                    //  We were using the variable named "vertices" above, therefore we need to set the new verticies to the damaged mesh data.
                    //  Damaged mesh data also restored while repairing with this proccess.
                    damagedWheelData[k].wheelPosition = wheelPos;

                    wheels[k].transform.localPosition = wheelPos;
                    wheels[k].transform.localRotation = Quaternion.identity;

                    if (!wheels[k].gameObject.activeSelf)
                        wheels[k].gameObject.SetActive(true);

                    carController.ESPBroken = false;

                    wheels[k].Inflate();

                }

            }

            //  Repairing and restoring all detachable parts of the vehicle.
            for (int i = 0; i < detachableParts.Length; i++) {

                if (detachableParts[i] != null)
                    detachableParts[i].OnRepair();

            }

            //  Repairing and restoring all lights of the vehicle.
            for (int i = 0; i < lights.Length; i++) {

                if (lights[i] != null)
                    lights[i].OnRepair();

            }

            //  If all meshes are completely restored, make sure repairing now is false.
            if (repaired)
                repairNow = false;

        }

    }

    /// <summary>
    /// Moving vertices of the collided meshes to the damaged positions while deforming.
    /// </summary>
    public void UpdateDamage() {

        if (!carController)
            return;

        if (!initialized)
            return;

        if (originalMeshData == null || originalMeshData.Length < 1)
            CheckMeshData();

        //  If vehicle is not deformed completely, and deforming is enabled, deform all meshes to their damaged structions.
        if (!deformed && deformingNow) {

            int k;
            deformed = true;
            deformationTime += Time.deltaTime;

            //  If deformable mesh is still exists, get all verticies of the mesh first. And then move all single verticies to the damaged positions. If verticies are close enough to the original
            //  position, deformed = true;
            for (k = 0; k < meshFilters.Length; k++) {

                MeshFilter currentMeshFilter = meshFilters[k];

                if (currentMeshFilter != null && currentMeshFilter.mesh != null) {

                    //  Get all verticies of the mesh first.
                    Vector3[] vertices = currentMeshFilter.mesh.vertices;

                    //  And then move all single verticies to the damaged positions.
                    for (int i = 0; i < vertices.Length; i++) {

                        Vector3 targetPosition = damagedMeshData[k].meshVerts[i];

                        if (deformationMode == DeformationMode.Accurate)
                            vertices[i] += (targetPosition - vertices[i]) * (Time.deltaTime * 5f);
                        else
                            vertices[i] = (targetPosition);

                        //// If any vertex is not yet close to the damaged position, mark as not deformed
                        //if (Vector3.SqrMagnitude(targetPosition - vertices[i]) > 0.001f)
                        //    deformed = false;

                    }

                    //  Setting new verticies to the all meshes. Recalculating normals and bounds, and then optimizing. This proccess can be heavy for high poly meshes.
                    currentMeshFilter.mesh.SetVertices(vertices);

                    if (recalculateNormals)
                        currentMeshFilter.mesh.RecalculateNormals();

                    if (recalculateBounds)
                        currentMeshFilter.mesh.RecalculateBounds();

                }

            }

            for (k = 0; k < wheels.Length; k++) {

                Transform wheelTransform = wheels[k].transform;

                if (wheelTransform != null) {

                    Vector3 currentLocalPosition = wheelTransform.localPosition;
                    Vector3 targetPosition = damagedWheelData[k].wheelPosition;

                    if (deformationMode == DeformationMode.Accurate)
                        currentLocalPosition += (targetPosition - currentLocalPosition) * (Time.deltaTime * 5f);
                    else
                        currentLocalPosition += (targetPosition - currentLocalPosition);

                    wheelTransform.localPosition = currentLocalPosition;
                    //wheelTransform.localRotation = Quaternion.Euler(vertices);

                }

            }

            //  Make sure deforming proccess takes only 1 second.
            if (deformationMode == DeformationMode.Accurate && deformationTime <= 1f)
                deformed = false;

            //  If all meshes are completely deformed, make sure deforming is false and timer is set to 0.
            if (deformed) {

                deformingNow = false;
                deformationTime = 0f;

            }

        }

    }

    /// <summary>
    /// Deforming meshes.
    /// </summary>
    /// <param name="impulse"></param>
    private void DamageMesh(float impulse) {

        if (!carController || !initialized)
            return;

        if (meshFilters == null || (meshFilters != null && meshFilters.Length < 1))
            return;

        if (originalMeshData == null || originalMeshData.Length < 1)
            CheckMeshData();

        Transform carTransform = carController.transform;  // Cache the car's transform

        Vector3 localContactPointRelativeToRoot = carTransform.InverseTransformPoint(contactPoint);

        // Calculate the collision direction in local space and reverse it
        Vector3 collisionDirection = -(localContactPointRelativeToRoot).normalized;

        if (octrees == null || (octrees != null && octrees.Length < 1))
            octrees = new RCC_Octree[meshFilters.Length];

        //  We will be checking all mesh filters with these contact points. If contact point is close enough to the mesh, deformation will be applied.
        for (int i = 0; i < meshFilters.Length; i++) {

            MeshFilter currentMeshFilter = meshFilters[i];

            if (currentMeshFilter == null || currentMeshFilter.mesh == null || !currentMeshFilter.gameObject.activeSelf)
                continue;

            // Create an Octree with bounds in world space
            if (octrees[i] == null) {

                octrees[i] = new RCC_Octree(currentMeshFilter);

                foreach (var vertex in currentMeshFilter.mesh.vertices)
                    octrees[i].Insert(vertex); // Insert the local-space vertex into the Octree.

            }

            Vector3 localContactPointRelativeToMesh = currentMeshFilter.transform.InverseTransformPoint(contactPoint);

            //  Getting closest point to the mesh.
            Vector3 nearestVert = NearestVertexWithOctree(i, localContactPointRelativeToMesh, currentMeshFilter);

            // Distance.
            float distance = (nearestVert - localContactPointRelativeToMesh).sqrMagnitude;

            //  If distance between contact point and closest point of the mesh is in range...
            if (distance <= (damageRadius * damageRadius)) {

                // All vertices of the mesh
                Vector3[] vertices = damagedMeshData[i].meshVerts;

                for (int k = 0; k < vertices.Length; k++) {

                    float distanceToVertSqr = (localContactPointRelativeToMesh - vertices[k]).sqrMagnitude;

                    if (distanceToVertSqr <= (damageRadius * damageRadius)) {

                        //  Calculate damage based on the impulse and distance
                        float damage = impulse * (1f - Mathf.Clamp01(Mathf.Sqrt(distanceToVertSqr) / damageRadius));

                        // Apply deformation in the local space of the mesh (in the reverse direction)
                        vertices[k] += collisionDirection * damage * (damageMultiplier / 10f);

                        //  If deformation exceeds limits, apply limits
                        if (maximumDamage > 0f && (vertices[k] - originalMeshData[i].meshVerts[k]).sqrMagnitude > (maximumDamage * maximumDamage)) {

                            vertices[k] = originalMeshData[i].meshVerts[k] + (vertices[k] - originalMeshData[i].meshVerts[k]).normalized * maximumDamage;

                        }

                    }

                }

            }

        }

    }

    /// <summary>
    /// Deforming wheels. Actually changing their local positions and rotations based on the impact.
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="impulse"></param>
    private void DamageWheel(float impulse) {

        if (!carController || !initialized)
            return;

        if (originalWheelData == null || originalWheelData.Length < 1)
            CheckWheelData();

        Transform carTransform = carController.transform;  // Cache the car's transform

        // Pre-calculate the collision direction outside the loop
        Vector3 collisionDirection = -((contactPoint - carTransform.position).normalized);

        for (int i = 0; i < wheels.Length; i++) {

            if (wheels[i] != null && wheels[i].gameObject.activeSelf) {

                Vector3 wheelPos = damagedWheelData[i].wheelPosition;

                // Calculate the closest point on the wheel collider
                Vector3 closestPoint = wheels[i].WheelCollider.ClosestPointOnBounds(contactPoint);
                float distanceSqr = (closestPoint - contactPoint).sqrMagnitude;  // Use squared magnitude for optimization

                // If the distance between the contact point and the closest point on the wheel collider is within range
                if (distanceSqr < (wheelDamageRadius * wheelDamageRadius)) {

                    float damage = (impulse * wheelDamageMultiplier) / 30f;

                    // Decrease damage based on the distance from the contact point
                    damage -= damage * Mathf.Clamp01(distanceSqr / (wheelDamageRadius * wheelDamageRadius)) * .5f;

                    Vector3 vW = carTransform.TransformPoint(wheelPos);
                    vW += (collisionDirection * damage);
                    wheelPos = carTransform.InverseTransformPoint(vW);

                    // If damage exceeds the maximum allowed, either cap the damage or detach the wheel
                    if (maximumDamage > 0 && (wheelPos - originalWheelData[i].wheelPosition).sqrMagnitude > (maximumDamage * maximumDamage)) {

                        // Uncomment this if you want to limit the damage instead of detaching
                        // wheelPos = originalWheelData[i].wheelPosition + (wheelPos - originalWheelData[i].wheelPosition).normalized * maximumDamage;

                        if (wheelDetachment && wheels[i].gameObject.activeSelf)
                            DetachWheel(wheels[i]);  // Detach the wheel if it's active and damage is over the threshold

                    }

                    // Update the damaged wheel position
                    damagedWheelData[i].wheelPosition = wheelPos;
                }
            }
        }
    }


    /// <summary>
    /// Deforming the detachable parts.
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="impulse"></param>
    private void DamagePart(float impulse) {

        if (!carController)
            return;

        if (!initialized)
            return;

        if (detachableParts != null && detachableParts.Length >= 1) {

            for (int i = 0; i < detachableParts.Length; i++) {

                if (detachableParts[i] != null && detachableParts[i].gameObject.activeSelf) {

                    if (detachableParts[i].partCollider != null) {

                        Vector3 closestPoint = detachableParts[i].partCollider.ClosestPointOnBounds(contactPoint);

                        float distance = (closestPoint - contactPoint).sqrMagnitude;
                        float damage = impulse * partDamageMultiplier;

                        // The damage should decrease with distance from the contact point.
                        damage -= damage * Mathf.Clamp01(distance / (partDamageRadius * partDamageRadius)) * .5f;

                        if (distance <= (partDamageRadius * partDamageRadius))
                            detachableParts[i].OnCollision(damage);

                    } else {

                        if ((contactPoint - detachableParts[i].transform.position).sqrMagnitude < (partDamageRadius * partDamageRadius))
                            detachableParts[i].OnCollision(impulse);

                    }

                }

            }

        }

    }

    /// <summary>
    /// Deforming the lights.
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="impulse"></param>
    private void DamageLight(float impulse) {

        if (!carController)
            return;

        if (!initialized)
            return;

        if (lights != null && lights.Length >= 1) {

            for (int i = 0; i < lights.Length; i++) {

                if (lights[i] != null && lights[i].gameObject.activeSelf) {

                    if ((contactPoint - lights[i].transform.position).sqrMagnitude < (lightDamageRadius * lightDamageRadius)) {

                        float distance = (lights[i].transform.position - contactPoint).sqrMagnitude;
                        float damage = impulse * lightDamageMultiplier;

                        // The damage should decrease with distance from the contact point.
                        damage -= damage * Mathf.Clamp01(distance / (lightDamageRadius * lightDamageRadius)) * .5f;

                        if (distance <= (lightDamageRadius * lightDamageRadius))
                            lights[i].OnCollision(damage);

                    }

                }

            }

        }

    }

    /// <summary>
    /// Detaches the target wheel.
    /// </summary>
    /// <param name="wheelCollider"></param>
    public void DetachWheel(RCC_WheelCollider wheelCollider) {

        if (!carController)
            return;

        if (!initialized)
            return;

        if (!wheelCollider)
            return;

        if (!wheelCollider.gameObject.activeSelf)
            return;

        wheelCollider.gameObject.SetActive(false);
        Transform wheelModel = wheelCollider.wheelModel;

        GameObject clonedWheel = GameObject.Instantiate(wheelModel.gameObject, wheelModel.transform.position, wheelModel.transform.rotation, null);
        clonedWheel.SetActive(true);
        clonedWheel.AddComponent<Rigidbody>();

        GameObject clonedMeshCollider = new GameObject("Mesh Collider");
        clonedMeshCollider.transform.SetParent(clonedWheel.transform, false);
        clonedMeshCollider.transform.position = RCC_GetBounds.GetBoundsCenter(clonedWheel.transform);

        MeshFilter biggestMesh = RCC_GetBounds.GetBiggestMesh(clonedWheel.transform);

        if (biggestMesh) {

            MeshCollider mc = clonedMeshCollider.AddComponent<MeshCollider>();
            mc.sharedMesh = biggestMesh.mesh;
            mc.convex = true;

        }

        carController.ESPBroken = true;

    }

    /// <summary>
    /// Raises the collision enter event.
    /// </summary>
    /// <param name="collision">Collision.</param>
    public void OnCollision(Collision collision) {

        if (!carController)
            return;

        if (!initialized)
            return;

        if (!carController.useDamage)
            return;

        if (((1 << collision.gameObject.layer) & damageFilter) != 0) {

            float impulse = collision.impulse.magnitude / 10000f;

            if (collision.rigidbody)
                impulse *= collision.rigidbody.mass / 1000f;

            if (impulse < minimumCollisionImpulse)
                impulse = 0f;

            if (impulse > 10f)
                impulse = 10f;

            if (impulse > 0f) {

                deformingNow = true;
                deformed = false;

                repairNow = false;
                repaired = false;

                //  First, we are getting all contact points.
                ContactPoint[] contacts = collision.contacts;
                contactPoints = new Vector3[contacts.Length];

                for (int i = 0; i < contactPoints.Length; i++)
                    contactPoints[i] = contacts[i].point;

                contactPoint = ContactPointsMagnitude(contactPoints);

                if (meshFilters != null && meshFilters.Length >= 1 && meshDeformation)
                    DamageMesh(impulse);

                if (wheels != null && wheels.Length >= 1 && wheelDamage)
                    DamageWheel(impulse);

                if (detachableParts != null && detachableParts.Length >= 1 && partDamage)
                    DamagePart(impulse);

                if (lights != null && lights.Length >= 1 && lightDamage)
                    DamageLight(impulse);

            }

        }

    }

    /// <summary>
    /// Raises the collision enter event.
    /// </summary>
    /// <param name="collision">Collision.</param>
    public void OnCollisionWithRay(RaycastHit hit, float impulse) {

        if (!carController)
            return;

        if (!initialized)
            return;

        if (!carController.useDamage)
            return;

        if (impulse < minimumCollisionImpulse)
            impulse = 0f;

        if (impulse > 10f)
            impulse = 10f;

        if (impulse > 0f) {

            deformingNow = true;
            deformed = false;

            repairNow = false;
            repaired = false;

            //  First, we are getting all contact points.
            contactPoint = hit.point;

            if (meshFilters != null && meshFilters.Length >= 1 && meshDeformation)
                DamageMesh(impulse);

            if (wheels != null && wheels.Length >= 1 && wheelDamage)
                DamageWheel(impulse);

            if (detachableParts != null && detachableParts.Length >= 1 && partDamage)
                DamagePart(impulse);

            if (lights != null && lights.Length >= 1 && lightDamage)
                DamageLight(impulse);

        }

    }

    private Vector3 ContactPointsMagnitude(Vector3[] givenContactPoints) {

        Vector3 magnitude = Vector3.zero;

        for (int i = 0; i < givenContactPoints.Length; i++)
            magnitude += givenContactPoints[i];

        magnitude /= givenContactPoints.Length;

        return magnitude;

    }

    /// <summary>
    /// Finds closest vertex to the target point using the Octree.
    /// </summary>
    /// <param name="meshIndex"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public Vector3 NearestVertexWithOctree(int meshIndex, Vector3 contactPoint, MeshFilter meshFilter) {

        if (meshIndex < 0 || meshIndex >= octrees.Length || octrees[meshIndex] == null) {

            Debug.LogWarning("Invalid Octree or mesh index.");
            return Vector3.zero;

        }

        return octrees[meshIndex].FindNearestVertex(contactPoint, meshFilter);

    }

}
