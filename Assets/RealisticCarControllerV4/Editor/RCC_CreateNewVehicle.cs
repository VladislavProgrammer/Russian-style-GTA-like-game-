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
using UnityEngine.Events;
using System;
using UnityEditor.Events;

public class RCC_CreateNewVehicle {

    public static RCC_CarControllerV4 NewVehicle(GameObject vehicle) {

        if (vehicle == null)
            return null;

        if (vehicle.GetComponentInParent<RCC_CarControllerV4>(true) != null) {

            EditorUtility.DisplayDialog("Realistic Car Controller | Already Has RCC_CarControllerV3", "Selected vehicle already has RCC_CarControllerV3. Are you sure you didn't pick the wrong house, oh vehicle?", "Close");
            return null;

        }

        if (EditorUtility.IsPersistent(Selection.activeGameObject)) {

            EditorUtility.DisplayDialog("Realistic Car Controller | Please select a vehicle in the scene", "Please select a vehicle in the scene, not in the project. Drag and drop the vehicle model to the scene, and try again.", "Close");
            return null;

        }

        bool isPrefab = PrefabUtility.IsAnyPrefabInstanceRoot(Selection.activeGameObject);

        if (isPrefab) {

            bool isModelPrefab = PrefabUtility.IsPartOfModelPrefab(Selection.activeGameObject);
            bool unpackPrefab = EditorUtility.DisplayDialog("Realistic Car Controller | Unpack Prefab", "This gameobject is connected to a " + (isModelPrefab ? "model" : "") + " prefab. Would you like to unpack the prefab completely? If you don't unpack it, you won't be able to move, reorder, or delete any children instance of the prefab.", "Unpack", "Don't Unpack");

            if (unpackPrefab)
                PrefabUtility.UnpackPrefabInstance(Selection.activeGameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

        }

        bool foundRigids = false;

        if (Selection.activeGameObject.GetComponentInChildren<Rigidbody>(true))
            foundRigids = true;

        if (foundRigids) {

            bool removeRigids = EditorUtility.DisplayDialog("Realistic Car Controller | Rigidbodies Found", "Additional rigidbodies found in your vehicle. Additional rigidbodies will affect vehicle behavior directly.", "Remove Them", "Leave Them");

            if (removeRigids) {

                foreach (Rigidbody rigidbody in Selection.activeGameObject.GetComponentsInChildren<Rigidbody>(true))
                    UnityEngine.Object.DestroyImmediate(rigidbody);

            }

        }

        bool foundWheelColliders = false;

        if (Selection.activeGameObject.GetComponentInChildren<WheelCollider>(true))
            foundWheelColliders = true;

        if (foundWheelColliders) {

            bool removeWheelColliders = EditorUtility.DisplayDialog("Realistic Car Controller | WheelColliders Found", "Additional wheelcolliders found in your vehicle.", "Remove Them", "Leave Them");

            if (removeWheelColliders) {

                foreach (WheelCollider wc in Selection.activeGameObject.GetComponentsInChildren<WheelCollider>(true))
                    UnityEngine.Object.DestroyImmediate(wc);

            }

        }

        bool fixPivot = EditorUtility.DisplayDialog("Realistic Car Controller | Fix Pivot Position Of The Vehicle", "Would you like to fix pivot position of the vehicle? If your vehicle has correct pivot position, select no.", "Fix", "No");

        if (fixPivot) {

            GameObject pivot = new GameObject(Selection.activeGameObject.name);
            pivot.transform.position = RCC_GetBounds.GetBoundsCenter(Selection.activeGameObject.transform);
            pivot.transform.rotation = Selection.activeGameObject.transform.rotation;

            pivot.AddComponent<RCC_CarControllerV4>();

            Selection.activeGameObject.transform.SetParent(pivot.transform);
            Selection.activeGameObject = pivot;

        } else {

            GameObject selectedVehicle = Selection.activeGameObject;

            selectedVehicle.AddComponent<RCC_CarControllerV4>();
            Selection.activeGameObject = selectedVehicle;

        }

        Rigidbody rigid = Selection.activeGameObject.GetComponent<Rigidbody>();
        rigid.mass = RCC_InitialSettings.Instance.mass;
        rigid.drag = RCC_InitialSettings.Instance.drag;
        rigid.angularDrag = RCC_InitialSettings.Instance.angularDrag;
        rigid.interpolation = RCC_InitialSettings.Instance.interpolation;
        rigid.collisionDetectionMode = RCC_InitialSettings.Instance.collisionDetectionMode;

        RCC_CarControllerV4 newVehicle = Selection.activeGameObject.GetComponent<RCC_CarControllerV4>();

        return newVehicle;

    }

}
