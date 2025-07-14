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

[CustomEditor(typeof(RCC_Customizer_NeonManager))]
public class RCC_Customizer_NeonEditor : Editor {

    RCC_Customizer_NeonManager prop;

    public override void OnInspectorGUI() {

        prop = (RCC_Customizer_NeonManager)target;
        serializedObject.Update();

        EditorGUILayout.HelpBox("Neon manager will only work with URP, builtin render pipeline won't work. Be sure your URP asset has 'Decal Renderer' as a renderer feature. ", MessageType.None);

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Back"))
            Selection.activeGameObject = prop.GetComponentInParent<RCC_Customizer>(true).gameObject;

    }

}
