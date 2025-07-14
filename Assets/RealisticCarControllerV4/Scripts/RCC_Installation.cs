//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RCC_Installation {

    public static void Check() {

        bool layer_RCC = false;
        bool layer_RCC_WheelCollider = false;
        bool layer_RCC_DetachablePart = false;
        bool layer_RCC_Prop = false;

        string[] missingLayers = new string[4];

        layer_RCC = LayerExists("RCC_Vehicle");
        layer_RCC_WheelCollider = LayerExists("RCC_WheelCollider");
        layer_RCC_DetachablePart = LayerExists("RCC_DetachablePart");
        layer_RCC_Prop = LayerExists("RCC_Prop");

        if (!layer_RCC)
            missingLayers[0] = "RCC_Vehicle";

        if (!layer_RCC_WheelCollider)
            missingLayers[1] = "RCC_WheelCollider";

        if (!layer_RCC_DetachablePart)
            missingLayers[2] = "RCC_DetachablePart";

        if (!layer_RCC_Prop)
            missingLayers[3] = "RCC_Prop";

        if (!layer_RCC || !layer_RCC_WheelCollider || !layer_RCC_DetachablePart || !layer_RCC_Prop) {

            if (EditorUtility.DisplayDialog("Realistic Car Controller | Found Missing Layers For Realistic Car Controller", "These layers will be added to the Tags and Layers\n\n" + missingLayers[0] + "\n" + missingLayers[1] + "\n" + missingLayers[2] + "\n" + missingLayers[3], "Add")) {

                CheckLayer("RCC_Vehicle");
                CheckLayer("RCC_WheelCollider");
                CheckLayer("RCC_DetachablePart");
                CheckLayer("RCC_Prop");

            }

        }

    }

    public static void CollisionMatrix() {

        bool layer_RCC;
        bool layer_RCC_WheelCollider;
        bool layer_RCC_DetachablePart;
        bool layer_RCC_Prop;

        layer_RCC = LayerExists("RCC_Vehicle");
        layer_RCC_WheelCollider = LayerExists("RCC_WheelCollider");
        layer_RCC_DetachablePart = LayerExists("RCC_DetachablePart");
        layer_RCC_Prop = LayerExists("RCC_Prop");

        if (layer_RCC && layer_RCC_DetachablePart)
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer(RCC_Settings.Instance.RCCLayer), LayerMask.NameToLayer(RCC_Settings.Instance.DetachablePartLayer), true);

        if (layer_RCC_WheelCollider && layer_RCC_DetachablePart)
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer(RCC_Settings.Instance.WheelColliderLayer), LayerMask.NameToLayer(RCC_Settings.Instance.DetachablePartLayer), true);

        if (layer_RCC_WheelCollider && layer_RCC_Prop)
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer(RCC_Settings.Instance.WheelColliderLayer), LayerMask.NameToLayer(RCC_Settings.Instance.PropLayer), true);

    }

    public static bool CheckTag(string tagName) {

        if (TagExists(tagName))
            return true;

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        if (!PropertyExists(tagsProp, 0, tagsProp.arraySize, tagName)) {

            int index = tagsProp.arraySize;

            tagsProp.InsertArrayElementAtIndex(index);
            SerializedProperty sp = tagsProp.GetArrayElementAtIndex(index);

            sp.stringValue = tagName;
            Debug.Log("Tag: " + tagName + " has been added.");

            tagManager.ApplyModifiedProperties();

            return true;

        }

        return false;

    }

    public static string NewTag(string name) {

        CheckTag(name);

        if (name == null || name == "")
            name = "Untagged";

        return name;

    }

    public static bool RemoveTag(string tagName) {

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        if (PropertyExists(tagsProp, 0, tagsProp.arraySize, tagName)) {

            SerializedProperty sp;

            for (int i = 0, j = tagsProp.arraySize; i < j; i++) {

                sp = tagsProp.GetArrayElementAtIndex(i);

                if (sp.stringValue == tagName) {

                    tagsProp.DeleteArrayElementAtIndex(i);
                    Debug.Log("Tag: " + tagName + " has been removed.");
                    tagManager.ApplyModifiedProperties();
                    return true;

                }

            }

        }

        return false;

    }

    public static bool TagExists(string tagName) {

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        return PropertyExists(tagsProp, 0, 10000, tagName);

    }

    public static bool CheckLayer(string layerName) {

        if (LayerExists(layerName))
            return true;

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        if (!PropertyExists(layersProp, 0, 31, layerName)) {

            SerializedProperty sp;

            for (int i = 8, j = 31; i < j; i++) {

                sp = layersProp.GetArrayElementAtIndex(i);

                if (sp.stringValue == "") {

                    sp.stringValue = layerName;
                    Debug.Log("Layer: " + layerName + " has been added.");
                    tagManager.ApplyModifiedProperties();
                    return true;

                }

                if (i == j)
                    Debug.Log("All allowed layers have been filled.");

            }

        }

        return false;

    }

    public static string NewLayer(string name) {

        if (name != null || name != "")
            CheckLayer(name);

        return name;

    }

    public static bool RemoveLayer(string layerName) {

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        if (PropertyExists(layersProp, 0, layersProp.arraySize, layerName)) {

            SerializedProperty sp;

            for (int i = 0, j = layersProp.arraySize; i < j; i++) {

                sp = layersProp.GetArrayElementAtIndex(i);

                if (sp.stringValue == layerName) {

                    sp.stringValue = "";
                    Debug.Log("Layer: " + layerName + " has been removed.");
                    // Save settings
                    tagManager.ApplyModifiedProperties();
                    return true;

                }

            }

        }

        return false;

    }

    public static bool LayerExists(string layerName) {

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        return PropertyExists(layersProp, 0, 31, layerName);

    }

    private static bool PropertyExists(SerializedProperty property, int start, int end, string value) {

        for (int i = start; i < end; i++) {

            SerializedProperty t = property.GetArrayElementAtIndex(i);

            if (t.stringValue.Equals(value))
                return true;

        }

        return false;

    }

    public static void CheckAllLayers() {

        CheckAllVehicleLayers();
        CheckAllDetachablePartLayers();
        CheckAllTrailerLayers();
        CheckAllPropLayers();

    }

    public static void CheckAllVehicleLayers() {

        List<RCC_CarControllerV4> foundPrefabs = new List<RCC_CarControllerV4>();

        bool cancelled = false;

        try {

            string[] paths = SearchByFilter("t:prefab").ToArray();
            int progress = 0;

            foreach (string path in paths) {

                if (EditorUtility.DisplayCancelableProgressBar("Searching for RCC Vehicles..", $"Scanned {progress}/{paths.Length} prefabs. Found {foundPrefabs.Count} new vehicles.", progress / (float)paths.Length)) {

                    cancelled = true;
                    break;

                }

                progress++;

                RCC_CarControllerV4 rcc = AssetDatabase.LoadAssetAtPath<RCC_CarControllerV4>(path);

                if (!rcc)
                    continue;

                foundPrefabs.Add(rcc);

            }

        }

        finally {

            EditorUtility.ClearProgressBar();

            if (!cancelled) {

                List<RCC_CarControllerV4> allVehicles = new List<RCC_CarControllerV4>();

                for (int i = 0; i < foundPrefabs.Count; i++) {

                    if (!allVehicles.Contains(foundPrefabs[i]))
                        allVehicles.Add(foundPrefabs[i]);

                }

                foreach (RCC_CarControllerV4 target in allVehicles) {

                    target.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.RCCLayer);

                    foreach (Transform item in target.GetComponentsInChildren<Transform>(true))
                        item.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.RCCLayer);

                    foreach (RCC_WheelCollider item in target.GetComponentsInChildren<RCC_WheelCollider>(true))
                        item.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.WheelColliderLayer);

                    foreach (RCC_DetachablePart item in target.GetComponentsInChildren<RCC_DetachablePart>(true))
                        item.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.DetachablePartLayer);

                }

            }

            Resources.UnloadUnusedAssets();

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        CollisionMatrix();

    }

    public static void CheckAllDetachablePartLayers() {

        List<RCC_DetachablePart> foundPrefabs = new List<RCC_DetachablePart>();

        bool cancelled = false;

        try {

            string[] paths = SearchByFilter("t:prefab").ToArray();
            int progress = 0;

            foreach (string path in paths) {

                if (EditorUtility.DisplayCancelableProgressBar("Searching for RCC Detachable Parts..", $"Scanned {progress}/{paths.Length} prefabs. Found {foundPrefabs.Count} new detachable parts.", progress / (float)paths.Length)) {

                    cancelled = true;
                    break;

                }

                progress++;

                RCC_DetachablePart rcc = AssetDatabase.LoadAssetAtPath<RCC_DetachablePart>(path);

                if (!rcc)
                    continue;

                foundPrefabs.Add(rcc);

            }

        }

        finally {

            EditorUtility.ClearProgressBar();

            if (!cancelled) {

                List<RCC_DetachablePart> allParts = new List<RCC_DetachablePart>();

                for (int i = 0; i < foundPrefabs.Count; i++) {

                    if (!allParts.Contains(foundPrefabs[i]))
                        allParts.Add(foundPrefabs[i]);

                }

                foreach (RCC_DetachablePart target in allParts) {

                    target.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.DetachablePartLayer);

                    foreach (Transform item in target.GetComponentsInChildren<Transform>(true))
                        item.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.DetachablePartLayer);

                }

            }

            Resources.UnloadUnusedAssets();

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        CollisionMatrix();

    }

    public static void CheckAllPropLayers() {

        List<RCC_Prop> foundPrefabs = new List<RCC_Prop>();

        bool cancelled = false;

        try {

            string[] paths = SearchByFilter("t:prefab").ToArray();
            int progress = 0;

            foreach (string path in paths) {

                if (EditorUtility.DisplayCancelableProgressBar("Searching for RCC Props..", $"Scanned {progress}/{paths.Length} prefabs. Found {foundPrefabs.Count} new props.", progress / (float)paths.Length)) {

                    cancelled = true;
                    break;

                }

                progress++;

                RCC_Prop rcc = AssetDatabase.LoadAssetAtPath<RCC_Prop>(path);

                if (!rcc)
                    continue;

                foundPrefabs.Add(rcc);

            }

        }

        finally {

            EditorUtility.ClearProgressBar();

            if (!cancelled) {

                List<RCC_Prop> allProps = new List<RCC_Prop>();

                for (int i = 0; i < foundPrefabs.Count; i++) {

                    if (!allProps.Contains(foundPrefabs[i]))
                        allProps.Add(foundPrefabs[i]);

                }

                foreach (RCC_Prop target in allProps) {

                    target.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.PropLayer);

                    foreach (Transform item in target.GetComponentsInChildren<Transform>(true))
                        item.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.PropLayer);

                }

            }

            Resources.UnloadUnusedAssets();

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        CollisionMatrix();

    }

    public static void CheckAllTrailerLayers() {

        List<RCC_TruckTrailer> foundPrefabs = new List<RCC_TruckTrailer>();

        bool cancelled = false;

        try {

            string[] paths = SearchByFilter("t:prefab").ToArray();
            int progress = 0;

            foreach (string path in paths) {

                if (EditorUtility.DisplayCancelableProgressBar("Searching for RCC Trailers..", $"Scanned {progress}/{paths.Length} prefabs. Found {foundPrefabs.Count} new trailers.", progress / (float)paths.Length)) {

                    cancelled = true;
                    break;

                }

                progress++;

                RCC_TruckTrailer rcc = AssetDatabase.LoadAssetAtPath<RCC_TruckTrailer>(path);

                if (!rcc)
                    continue;

                foundPrefabs.Add(rcc);

            }

        }

        finally {

            EditorUtility.ClearProgressBar();

            if (!cancelled) {

                List<RCC_TruckTrailer> allTrailers = new List<RCC_TruckTrailer>();

                for (int i = 0; i < foundPrefabs.Count; i++) {

                    if (!allTrailers.Contains(foundPrefabs[i]))
                        allTrailers.Add(foundPrefabs[i]);

                }

                foreach (RCC_TruckTrailer target in allTrailers) {

                    target.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.RCCLayer);

                    foreach (Transform item in target.GetComponentsInChildren<Transform>(true))
                        item.gameObject.layer = LayerMask.NameToLayer(RCC_Settings.Instance.RCCLayer);

                }

            }

            Resources.UnloadUnusedAssets();

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        CollisionMatrix();

    }

    public static IEnumerable<string> SearchByFilter(string filter) {

        foreach (string guid in AssetDatabase.FindAssets(filter))
            yield return AssetDatabase.GUIDToAssetPath(guid);

    }

}
#endif