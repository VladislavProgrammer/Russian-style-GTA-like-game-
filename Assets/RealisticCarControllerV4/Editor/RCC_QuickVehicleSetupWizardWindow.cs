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

public class RCC_QuickVehicleSetupWizardWindow : EditorWindow {

    private GUISkin skin;
    private string nextStepButtonString;

    private GameObject selectedRoot;

    public GameObject body;
    public GameObject wheel_FL, wheel_FR, wheel_RL, wheel_RR;

    public int width = 500;
    public int height = 260;

    public int toolbarIndex = 0;

    public float startTime = 0f;
    public float endTime = 0f;

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller/Quick Vehicle Setup Wizard", false, -85)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller/Quick Vehicle Setup Wizard", false, -85)]
    public static void OpenWindow() {

        GetWindow(typeof(RCC_QuickVehicleSetupWizardWindow), false);

    }

    [MenuItem("Tools/BoneCracker Games/Realistic Car Controller/Quick Vehicle Setup Wizard", true, -85)]
    [MenuItem("GameObject/BoneCracker Games/Realistic Car Controller/Quick Vehicle Setup Wizard", true, -85)]
    public static bool CheckOpenWindow() {

        if (!Selection.activeGameObject)
            return false;

        if (Selection.gameObjects.Length > 1)
            return false;

        if (!Selection.activeGameObject.activeSelf)
            return false;

        if (EditorUtility.IsPersistent(Selection.activeGameObject))
            return false;

        return true;

    }

    private void OnEnable() {

        startTime = 0f;
        endTime = 0f;

        minSize = new Vector2(width, height);
        maxSize = minSize;

        skin = (GUISkin)Resources.Load("RCC_WindowSkin");
        nextStepButtonString = "Next";

    }

    public void OnGUI() {

        GUI.skin = skin;

        EditorGUILayout.BeginVertical(GUI.skin.box);

        switch (toolbarIndex) {

            case 0:
                ToolbarMenu_Welcome();
                break;

            case 1:
                ToolbarMenu_CreateBehavior();
                break;

            case 2:
                ToolbarMenu_CustomizeBehavior();
                break;

            case 3:
                ToolbarMenu_SelectFrontWheels();
                break;

            case 4:
                ToolbarMenu_SelectRearWheels();
                break;

            case 5:
                ToolbarMenu_Collider();
                break;

            case 6:
                ToolbarMenu_Finish();
                break;

        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndVertical();

        Color defGuiColor = GUI.color;
        GUI.color = Color.green;

        if (toolbarIndex == 1 && selectedRoot == null)
            GUI.enabled = false;

        if (toolbarIndex == 3 && (wheel_FL == null || wheel_FR == null))
            GUI.enabled = false;

        if (toolbarIndex == 4 && (wheel_RL == null || wheel_RR == null))
            GUI.enabled = false;

        if (GUILayout.Button(nextStepButtonString))
            Next();

        GUI.enabled = true;
        GUI.color = defGuiColor;

        Repaint();

    }

    private void ToolbarMenu_Welcome() {

        GUILayout.Label("Welcome", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("Welcome to Quick Vehicle Setup Wizard. You'll be able to create your new vehicle with a few steps. Setup includes these simple steps;\n\n1. Selecting the root of the vehicle gameobject in the scene and adding the main controller.\n2. Selecting the front wheels\n3. Selecting the rear wheels\n4. Adding / checking body collider.\n5. Adding addon components.\n\nPlease proceed to the next step to get started.\n\nThis setup only covers the main essentials, editing lights, damage, audio, and other things can be done by accesing to the corresponding addon component after the setup.", MessageType.None);
        nextStepButtonString = "Next";

        startTime = (float)EditorApplication.timeSinceStartup;
        endTime = 0f;

        selectedRoot = null;
        wheel_FL = null;
        wheel_FR = null;
        wheel_RL = null;
        wheel_RR = null;
        body = null;

    }

    private void ToolbarMenu_CreateBehavior() {

        GUILayout.Label("Add Main Controller", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("We must add the main controller (RCC_CarControllerV3) to the root of the vehicle gameobject. Don't select multiple gameobjects, or prefabs located in the project directory. \n\n1. Drag and drop the vehicle model, gameobject, or prefab to the scene.\n2. Select the root of your vehicle gameobject in the scene.\n3. And add the main controller by clicking the below green button.", MessageType.None);
        nextStepButtonString = "Yes, I've Selected The Root Of The GameObject, Next";

        EditorGUILayout.Space();

        selectedRoot = null;

        if (Selection.activeGameObject != null) {

            if (Selection.gameObjects.Length == 1 && Selection.activeGameObject.scene.name != null && !EditorUtility.IsPersistent(Selection.activeGameObject))
                selectedRoot = Selection.activeGameObject;

        }

        if (selectedRoot != null) {

            GUILayout.Label("This GameObject Is Root Of My Vehicle", EditorStyles.centeredGreyMiniLabel);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label(selectedRoot.name);
            EditorGUILayout.EndVertical();

        }

    }

    private void ToolbarMenu_CustomizeBehavior() {

        GUILayout.Label("Customize Controller", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("All drivetrain components have been added and connected together. Now you can proceed to selecting the wheels step. Doesn't it look like it's weird to have an empty step like this? Oh, I'm too lazy to remove this step right now, maybe I can remove this step in the next update. But hey, seems like you're doing great job, move on :)", MessageType.None);
        nextStepButtonString = "You Talk Too Much, Next";

        bool hasCorrectOrientation = RCC_CheckAxisOrientation.IsAxisOrientationCorrect(selectedRoot);

        if (!hasCorrectOrientation)
            EditorGUILayout.HelpBox("Root of your vehicle model has wrong X Y Z axis orientation. It's essential to have proper axes, otherwise vehicle won't work properly. ", MessageType.Error);

    }

    private void ToolbarMenu_SelectFrontWheels() {

        GUILayout.Label("Front Wheels", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("This vehicle has two axes for front wheels and rear wheels. Please select the front left and front right wheels now.\n\nYou can have any amount of axles on your vehicle. Axles can be accesed through the Axles component on the vehicle. You can create / edit / remove any axle. But once you create another new axle, you must select its left and right wheel manually. No worries, editor will guide you.", MessageType.None);
        nextStepButtonString = "Yes, I've Selected Front Left And Right Wheels, Next";

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        wheel_FL = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Front Left Wheel"), wheel_FL, typeof(GameObject), true);
        EditorGUILayout.Space();
        wheel_FR = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Front Right Wheel"), wheel_FR, typeof(GameObject), true);

        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Try Detect Front Wheels")) {

            GameObject[] possibleFrontWheels = RCC_DetectPossibleWheels.DetectPossibleFrontWheels(selectedRoot);

            if (possibleFrontWheels != null && possibleFrontWheels.Length >= 2) {

                RCC_PopupWindow_PossibleWheels.ShowWindow(possibleFrontWheels, (input) => {

                    possibleFrontWheels = input;

                    for (int i = 0; i < possibleFrontWheels.Length; i++) {

                        if (IsOnRight(selectedRoot, possibleFrontWheels[i]))
                            wheel_FR = possibleFrontWheels[i];
                        else
                            wheel_FL = possibleFrontWheels[i];

                    }

                });

            } else {

                EditorUtility.DisplayDialog("Realistic Car Controller | Couldn't find any possible wheel models", "Are you sure your model has separated wheel models? If wheel models are part of the main body, it won't work. Be sure your model has separated wheel models.", "Ok");

            }

        }

    }

    private bool IsOnRight(GameObject vehicle, GameObject wheel) {

        // Transform the child's position to the parent's local space
        Vector3 localPosition = vehicle.transform.InverseTransformPoint(wheel.transform.position);

        // Check if the local X coordinate is positive
        return localPosition.x > 0;

    }

    private void ToolbarMenu_SelectRearWheels() {

        GUILayout.Label("Rear Wheels", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("This vehicle has two axes for front wheels and rear wheels. Please select the rear left and rear right wheels now.\n\nYou can have any amount of axles on your vehicle. Axles can be accesed through the Axles component on the vehicle. You can create / edit / remove any axle. But once you create another new axle, you must select its left and right wheel manually. No worries, editor will guide you.", MessageType.None);
        nextStepButtonString = "Yes, I've Selected Rear Left And Right Wheels, Next";

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        wheel_RL = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Rear Left Wheel"), wheel_RL, typeof(GameObject), true);
        EditorGUILayout.Space();
        wheel_RR = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Rear Right Wheel"), wheel_RR, typeof(GameObject), true);

        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Try Detect Rear Wheels")) {

            GameObject[] possibleRearWheels = RCC_DetectPossibleWheels.DetectPossibleRearWheels(selectedRoot);

            if (possibleRearWheels != null && possibleRearWheels.Length >= 2) {

                RCC_PopupWindow_PossibleWheels.ShowWindow(possibleRearWheels, (input) => {

                    possibleRearWheels = input;

                    for (int i = 0; i < possibleRearWheels.Length; i++) {

                        if (IsOnRight(selectedRoot, possibleRearWheels[i]))
                            wheel_RR = possibleRearWheels[i];
                        else
                            wheel_RL = possibleRearWheels[i];

                    }

                });

            } else {

                EditorUtility.DisplayDialog("Realistic Car Controller | Couldn't find any possible wheel models", "Are you sure your model has separated wheel models? If wheel models are part of the main body, it won't work. Be sure your model has separated wheel models.", "Ok");

            }

        }

    }

    private void ToolbarMenu_Collider() {

        GUILayout.Label("Body Collider", EditorStyles.boldLabel);

        Collider[] colliders = selectedRoot.GetComponentsInChildren<Collider>();
        bool colliderFound = false;

        for (int i = 0; i < colliders.Length; i++) {

            if (!(colliders[i] as WheelCollider))
                colliderFound = true;

        }

        if (!colliderFound)
            EditorGUILayout.HelpBox("This vehicle doesn't have a body collider. Please select the main body part of your vehicle, and add a mesh collider.", MessageType.None);
        else
            EditorGUILayout.HelpBox("Few colliders have been found in the vehicle. I'm not sure which one is the body collider, please make sure your vehicle as a body collider.", MessageType.None);

        nextStepButtonString = "Yes, I've Added A Body Collider, Next";

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        body = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Main Body"), body, typeof(GameObject), true);
        EditorGUILayout.Space();

        if (!body || (body && body.GetComponent<MeshCollider>()))
            GUI.enabled = false;

        if (GUILayout.Button("Add MeshCollider To Selected Body")) {

            MeshCollider meshCol = body.AddComponent<MeshCollider>();
            meshCol.convex = true;
            meshCol.material = RCC_Settings.Instance.colliderMaterial;

        }

        GUI.enabled = true;

        EditorGUILayout.EndVertical();

    }

    private void ToolbarMenu_Finish() {

        GUILayout.Label("Congratulations!", EditorStyles.boldLabel);

        if (endTime == 0)
            endTime = (float)EditorApplication.timeSinceStartup;

        float timeToCreate = endTime - startTime;
        EditorGUILayout.HelpBox("Congratulations, you've created a new vehicle within " + timeToCreate.ToString("F0") + " seconds!\n\nThis vehicle using default resources, you can change any of them by clicking the corresponding addon component button. Have fun!", MessageType.None);
        nextStepButtonString = "Thanks, See You Next Time...";

    }

    public void Next() {

        if (toolbarIndex == 1 && selectedRoot) {

            RCC_CarControllerV4 newVehicle = RCC_CreateNewVehicle.NewVehicle(selectedRoot);

            if (newVehicle)
                selectedRoot = newVehicle.gameObject;
            else
                Close();

        }

        if (toolbarIndex == 3 && wheel_FL && wheel_FR) {

            selectedRoot.GetComponent<RCC_CarControllerV4>().FrontLeftWheelTransform = wheel_FL.transform;
            selectedRoot.GetComponent<RCC_CarControllerV4>().FrontRightWheelTransform = wheel_FR.transform;

        }

        if (toolbarIndex == 4 && wheel_RL && wheel_RR) {

            selectedRoot.GetComponent<RCC_CarControllerV4>().RearLeftWheelTransform = wheel_RL.transform;
            selectedRoot.GetComponent<RCC_CarControllerV4>().RearRightWheelTransform = wheel_RR.transform;

            selectedRoot.GetComponent<RCC_CarControllerV4>().CreateWheelColliders();

            RCC_WheelCollider[] wheels = selectedRoot.GetComponentsInChildren<RCC_WheelCollider>(true);

            foreach (RCC_WheelCollider wc in wheels)
                wc.transform.position += selectedRoot.transform.up * (wc.WheelCollider.suspensionDistance / 2f);

        }

        toolbarIndex++;

        if (toolbarIndex >= 7) {

            toolbarIndex = 0;
            Close();

        }

    }

    public void ResetToDefault() {

        toolbarIndex = 0;

    }

}
