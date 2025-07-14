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

[CustomEditor(typeof(RCC_Customizer_Brake))]
public class RCC_Customizer_Upgrade_BrakeEditor : Editor {

    RCC_Customizer_Brake prop;

    public override void OnInspectorGUI() {

        prop = (RCC_Customizer_Brake)target;
        serializedObject.Update();

        EditorGUILayout.HelpBox("Brake torque will be calculated by efficiency value of the upgrader depending on the level.\nDefault brake torque: " + prop.GetComponentInParent<RCC_CarControllerV4>(true).brakeTorque + "\nFully upgraded brake torque: " + prop.GetComponentInParent<RCC_CarControllerV4>(true).brakeTorque * prop.efficiency + "\nCurrent level: " + prop.BrakeLevel, MessageType.None);

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

        if (GUILayout.Button("Back"))
            Selection.activeGameObject = prop.GetComponentInParent<RCC_Customizer>(true).gameObject;

    }

}
