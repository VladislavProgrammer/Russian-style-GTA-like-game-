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

[CustomEditor(typeof(RCC_Customizer_Engine))]
public class RCC_Customizer_Upgrade_EngineEditor : Editor {

    RCC_Customizer_Engine prop;

    public override void OnInspectorGUI() {

        prop = (RCC_Customizer_Engine)target;
        serializedObject.Update();

        EditorGUILayout.HelpBox("Engine torque will be calculated by efficiency value of the upgrader depending on the level.\nDefault engine torque: " + prop.GetComponentInParent<RCC_CarControllerV4>(true).maxEngineTorque + "\nFully upgraded engine torque: " + prop.GetComponentInParent<RCC_CarControllerV4>(true).maxEngineTorque * prop.efficiency + "\nCurrent level: " + prop.EngineLevel, MessageType.None);

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

        if (GUILayout.Button("Back"))
            Selection.activeGameObject = prop.GetComponentInParent<RCC_Customizer>(true).gameObject;

    }

}
