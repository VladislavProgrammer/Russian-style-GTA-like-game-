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

[CustomEditor(typeof(RCC_Customizer_Speed))]
public class RCC_Customizer_Upgrade_SpeedEditor : Editor {

    RCC_Customizer_Speed prop;

    public override void OnInspectorGUI() {

        prop = (RCC_Customizer_Speed)target;
        serializedObject.Update();

        EditorGUILayout.HelpBox("Maximum speed will be calculated by efficiency value of the upgrader depending on the level.\nDefault maximum speed: " + prop.GetComponentInParent<RCC_CarControllerV4>(true).maxspeed + "\nFully upgraded maximum speed: " + prop.GetComponentInParent<RCC_CarControllerV4>(true).maxspeed * prop.efficiency + "\nCurrent level: " + prop.SpeedLevel, MessageType.None);

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

        if (GUILayout.Button("Back"))
            Selection.activeGameObject = prop.GetComponentInParent<RCC_Customizer>(true).gameObject;

    }

}
