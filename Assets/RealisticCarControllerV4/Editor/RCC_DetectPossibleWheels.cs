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
using UnityEditor;

public class RCC_DetectPossibleWheels : Editor {

    public static GameObject[] DetectPossibleAllWheels(GameObject vehicle) {

        List<GameObject> allWheels = new List<GameObject>();
        MeshFilter[] meshFilter = vehicle.GetComponentsInChildren<MeshFilter>();

        if (meshFilter != null) {

            for (int i = 0; i < meshFilter.Length; i++) {

                if (meshFilter[i].sharedMesh != null) {

                    Bounds bounds = meshFilter[i].sharedMesh.bounds;

                    float depth = bounds.size.x;
                    float height = bounds.size.y;
                    float width = bounds.size.z;

                    // Simple cylindrical detection based on aspect ratios
                    if (Mathf.Abs(width - height) < 0.05f && depth < width) {

                        allWheels.Add(meshFilter[i].gameObject);

                    }

                }

            }

        }

        return allWheels.ToArray();

    }

    public static GameObject[] DetectPossibleFrontWheels(GameObject vehicle) {

        GameObject[] allWheels = DetectPossibleAllWheels(vehicle);
        List<GameObject> frontWheels = new List<GameObject>();

        for (int i = 0; i < allWheels.Length; i++) {

            if (IsInFront(vehicle, allWheels[i]))
                frontWheels.Add(allWheels[i]);

        }

        return frontWheels.ToArray();

    }

    public static GameObject[] DetectPossibleRearWheels(GameObject vehicle) {

        GameObject[] allWheels = DetectPossibleAllWheels(vehicle);
        List<GameObject> rearWheels = new List<GameObject>();

        for (int i = 0; i < allWheels.Length; i++) {

            if (!IsInFront(vehicle, allWheels[i]))
                rearWheels.Add(allWheels[i]);

        }

        return rearWheels.ToArray();

    }

    private static bool IsInFront(GameObject vehicle, GameObject wheel) {

        // Get the forward direction of the parent
        Vector3 parentForward = vehicle.transform.forward;

        // Get the direction from the parent to the child
        Vector3 directionToChild = wheel.transform.position - vehicle.transform.position;

        // Calculate the dot product
        float dotProduct = Vector3.Dot(parentForward, directionToChild);

        // If the dot product is positive, the child is in front
        return dotProduct > 0;

    }

}
