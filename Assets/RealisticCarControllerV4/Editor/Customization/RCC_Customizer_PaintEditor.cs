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
using UnityEditor.SceneManagement;

[CustomEditor(typeof(RCC_Customizer_PaintManager))]
public class RCC_Customizer_PaintEditor : Editor {

    RCC_Customizer_PaintManager prop;

    public override void OnInspectorGUI() {

        prop = (RCC_Customizer_PaintManager)target;
        serializedObject.Update();

        EditorGUILayout.HelpBox("All painters have target renderers and material index. If your vehicle has multiple paintable renderers, create new painter for each renderer and set their target material indexes. Click 'Get All Paints' after editing painters.", MessageType.None);

        DrawDefaultInspector();

        if (GUILayout.Button("Get All Painters"))
            prop.paints = prop.GetComponentsInChildren<RCC_Customizer_Paint>(true);

        if (GUILayout.Button("Create New Painter")) {

            GameObject newPainter = new GameObject("Painter");
            newPainter.transform.SetParent(prop.transform);
            newPainter.transform.localPosition = Vector3.zero;
            newPainter.transform.localRotation = Quaternion.identity;
            newPainter.AddComponent<RCC_Customizer_Paint>();

            // Register the creation of the object for undo/redo functionality.
            Undo.RegisterCreatedObjectUndo(newPainter, "Add Painter");

            // Mark the scene as dirty so Unity knows it has changed.
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

        if (GUILayout.Button("Back"))
            Selection.activeGameObject = prop.GetComponentInParent<RCC_Customizer>(true).gameObject;

    }

}
