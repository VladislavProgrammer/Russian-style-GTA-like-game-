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

[CustomEditor(typeof(RCC_Customizer_Handling))]
public class RCC_Customizer_Upgrade_HandlingEditor : Editor {

    RCC_Customizer_Handling prop;

    public override void OnInspectorGUI() {

        prop = (RCC_Customizer_Handling)target;
        serializedObject.Update();

        EditorGUILayout.HelpBox("Handling strength will be calculated by efficiency value of the upgrader depending on the level.\nDefault handling strength: " + prop.GetComponentInParent<RCC_CarControllerV4>(true).tractionHelperStrength + "\nFully upgraded handling strength: " + prop.GetComponentInParent<RCC_CarControllerV4>(true).tractionHelperStrength * prop.efficiency + "\nCurrent level: " + prop.HandlingLevel, MessageType.None);

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

        if (GUILayout.Button("Back"))
            Selection.activeGameObject = prop.GetComponentInParent<RCC_Customizer>(true).gameObject;

    }

}
