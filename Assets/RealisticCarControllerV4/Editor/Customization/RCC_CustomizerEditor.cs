//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(RCC_Customizer))]
public class RCC_CustomizerEditor : Editor {

    RCC_Customizer prop;
    GUISkin skin;
    private Color guiColor;

    RCC_Customizer_SpoilerManager spoilers;
    RCC_Customizer_SirenManager sirens;
    RCC_Customizer_UpgradeManager upgrades;
    RCC_Customizer_PaintManager paints;
    RCC_Customizer_WheelManager wheels;
    RCC_Customizer_CustomizationManager customization;
    RCC_Customizer_DecalManager decals;
    RCC_Customizer_NeonManager neons;

    private void OnEnable() {

        guiColor = GUI.color;
        skin = Resources.Load<GUISkin>("RCC_WindowSkin");

    }

    private void GetAllComponents() {

        spoilers = prop.GetComponentInChildren<RCC_Customizer_SpoilerManager>(true);
        sirens = prop.GetComponentInChildren<RCC_Customizer_SirenManager>(true);
        upgrades = prop.GetComponentInChildren<RCC_Customizer_UpgradeManager>(true);
        paints = prop.GetComponentInChildren<RCC_Customizer_PaintManager>(true);
        wheels = prop.GetComponentInChildren<RCC_Customizer_WheelManager>(true);
        customization = prop.GetComponentInChildren<RCC_Customizer_CustomizationManager>(true);
        decals = prop.GetComponentInChildren<RCC_Customizer_DecalManager>(true);
        neons = prop.GetComponentInChildren<RCC_Customizer_NeonManager>(true);

    }

    public override void OnInspectorGUI() {

        prop = (RCC_Customizer)target;
        serializedObject.Update();
        GUI.skin = skin;

        GetAllComponents();

        EditorGUILayout.HelpBox("Customizer system that includes paints, wheels, spoilers, configurations, sirens, upgrades, and more. Customization data will be saved and loaded with json.", MessageType.Info, true);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("initializeMethod"), new GUIContent("Initialize Method", "When should be customizer initialized?"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("saveFileName"), new GUIContent("Save File Name", "Save File Name."), false);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("autoSave"), new GUIContent("Auto Save Loadout", "Saves last changes."), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("autoLoadLoadout"), new GUIContent("Auto Load Loadout", "Loads all last changes."), false);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("loadout"), new GUIContent("Loadout", "Loadout."), true);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Hide All")) {

            prop.HideAll();
            EditorUtility.SetDirty(prop);

        }

        if (GUILayout.Button("Show All")) {

            prop.ShowAll();
            EditorUtility.SetDirty(prop);

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (!spoilers) {

            EditorGUILayout.HelpBox("Spoiler Manager not found!", MessageType.None);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(120f))) {

                GameObject create = Instantiate(RCC_CustomizationSetups.Instance.spoilers, prop.transform.position, prop.transform.rotation, prop.transform);
                create.transform.SetParent(prop.transform);
                create.transform.localPosition = Vector3.zero;
                create.transform.localRotation = Quaternion.identity;
                create.name = RCC_CustomizationSetups.Instance.spoilers.name;

                EditorUtility.SetDirty(prop);

                // Register the creation of the object for undo/redo functionality.
                Undo.RegisterCreatedObjectUndo(create, "Create Spoiler");

                // Mark the scene as dirty so Unity knows it has changed.
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            }

        } else {

            EditorGUILayout.HelpBox("Spoiler Manager found!", MessageType.None);

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select", GUILayout.Width(120f)))
                Selection.activeGameObject = spoilers.gameObject;

            GUI.color = Color.red;

            if (GUILayout.Button("X", GUILayout.Width(25f))) {

                DestroyImmediate(spoilers.gameObject);
                EditorUtility.SetDirty(prop);

            }

            GUI.color = guiColor;

            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (!sirens) {

            EditorGUILayout.HelpBox("Siren Manager not found!", MessageType.None);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(120f))) {

                GameObject create = Instantiate(RCC_CustomizationSetups.Instance.sirens, prop.transform.position, prop.transform.rotation, prop.transform);
                create.transform.SetParent(prop.transform);
                create.transform.localPosition = Vector3.zero;
                create.transform.localRotation = Quaternion.identity;
                create.name = RCC_CustomizationSetups.Instance.sirens.name;

                EditorUtility.SetDirty(prop);

                // Register the creation of the object for undo/redo functionality.
                Undo.RegisterCreatedObjectUndo(create, "Create Siren");

                // Mark the scene as dirty so Unity knows it has changed.
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            }

        } else {

            EditorGUILayout.HelpBox("Siren Manager found!", MessageType.None);

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select", GUILayout.Width(120f)))
                Selection.activeGameObject = sirens.gameObject;

            GUI.color = Color.red;

            if (GUILayout.Button("X", GUILayout.Width(25f))) {

                DestroyImmediate(sirens.gameObject);
                EditorUtility.SetDirty(prop);

            }

            GUI.color = guiColor;

            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (!upgrades) {

            EditorGUILayout.HelpBox("Upgrade Manager not found!", MessageType.None);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(120f))) {

                GameObject create = Instantiate(RCC_CustomizationSetups.Instance.upgrades, prop.transform.position, prop.transform.rotation, prop.transform);
                create.transform.SetParent(prop.transform);
                create.transform.localPosition = Vector3.zero;
                create.transform.localRotation = Quaternion.identity;
                create.name = RCC_CustomizationSetups.Instance.upgrades.name;

                EditorUtility.SetDirty(prop);

                // Register the creation of the object for undo/redo functionality.
                Undo.RegisterCreatedObjectUndo(create, "Create Upgrade");

                // Mark the scene as dirty so Unity knows it has changed.
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            }

        } else {

            EditorGUILayout.HelpBox("Upgrade Manager found!", MessageType.None);

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select", GUILayout.Width(120f)))
                Selection.activeGameObject = upgrades.gameObject;

            GUI.color = Color.red;

            if (GUILayout.Button("X", GUILayout.Width(25f))) {

                DestroyImmediate(upgrades.gameObject);
                EditorUtility.SetDirty(prop);

            }

            GUI.color = guiColor;

            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (!paints) {

            EditorGUILayout.HelpBox("Paint Manager not found!", MessageType.None);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(120f))) {

                GameObject create = Instantiate(RCC_CustomizationSetups.Instance.paints, prop.transform.position, prop.transform.rotation, prop.transform);
                create.transform.SetParent(prop.transform);
                create.transform.localPosition = Vector3.zero;
                create.transform.localRotation = Quaternion.identity;
                create.name = RCC_CustomizationSetups.Instance.paints.name;

                EditorUtility.SetDirty(prop);

                // Register the creation of the object for undo/redo functionality.
                Undo.RegisterCreatedObjectUndo(create, "Create Paint");

                // Mark the scene as dirty so Unity knows it has changed.
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            }

        } else {

            EditorGUILayout.HelpBox("Paint Manager found!", MessageType.None);

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select", GUILayout.Width(120f)))
                Selection.activeGameObject = paints.gameObject;

            GUI.color = Color.red;

            if (GUILayout.Button("X", GUILayout.Width(25f))) {

                DestroyImmediate(paints.gameObject);
                EditorUtility.SetDirty(prop);

            }

            GUI.color = guiColor;

            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (!wheels) {

            EditorGUILayout.HelpBox("Wheel Manager not found!", MessageType.None);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(120f))) {

                GameObject create = Instantiate(RCC_CustomizationSetups.Instance.wheels, prop.transform.position, prop.transform.rotation, prop.transform);
                create.transform.SetParent(prop.transform);
                create.transform.localPosition = Vector3.zero;
                create.transform.localRotation = Quaternion.identity;
                create.name = RCC_CustomizationSetups.Instance.wheels.name;

                EditorUtility.SetDirty(prop);

                // Register the creation of the object for undo/redo functionality.
                Undo.RegisterCreatedObjectUndo(create, "Create Wheels");

                // Mark the scene as dirty so Unity knows it has changed.
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            }

        } else {

            EditorGUILayout.HelpBox("Wheel Manager found!", MessageType.None);

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select", GUILayout.Width(120f)))
                Selection.activeGameObject = wheels.gameObject;

            GUI.color = Color.red;

            if (GUILayout.Button("X", GUILayout.Width(25f))) {

                DestroyImmediate(wheels.gameObject);
                EditorUtility.SetDirty(prop);

            }

            GUI.color = guiColor;

            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (!customization) {

            EditorGUILayout.HelpBox("Customization Manager not found!", MessageType.None);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(120f))) {

                GameObject create = Instantiate(RCC_CustomizationSetups.Instance.customization, prop.transform.position, prop.transform.rotation, prop.transform);
                create.transform.SetParent(prop.transform);
                create.transform.localPosition = Vector3.zero;
                create.transform.localRotation = Quaternion.identity;
                create.name = RCC_CustomizationSetups.Instance.customization.name;

                EditorUtility.SetDirty(prop);

                // Register the creation of the object for undo/redo functionality.
                Undo.RegisterCreatedObjectUndo(create, "Create Customization");

                // Mark the scene as dirty so Unity knows it has changed.
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            }

        } else {

            EditorGUILayout.HelpBox("Customization Manager found!", MessageType.None);

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select", GUILayout.Width(120f)))
                Selection.activeGameObject = customization.gameObject;

            GUI.color = Color.red;

            if (GUILayout.Button("X", GUILayout.Width(25f))) {

                DestroyImmediate(customization.gameObject);
                EditorUtility.SetDirty(prop);

            }

            GUI.color = guiColor;

            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (!decals) {

            EditorGUILayout.HelpBox("Decal Manager not found!", MessageType.None);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(120f))) {

                GameObject create = Instantiate(RCC_CustomizationSetups.Instance.decals, prop.transform.position, prop.transform.rotation, prop.transform);
                create.transform.SetParent(prop.transform);
                create.transform.localPosition = Vector3.zero;
                create.transform.localRotation = Quaternion.identity;
                create.name = RCC_CustomizationSetups.Instance.decals.name;

                EditorUtility.SetDirty(prop);

                // Register the creation of the object for undo/redo functionality.
                Undo.RegisterCreatedObjectUndo(create, "Create Decals");

                // Mark the scene as dirty so Unity knows it has changed.
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            }

        } else {

            EditorGUILayout.HelpBox("Decal Manager found!", MessageType.None);

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select", GUILayout.Width(120f)))
                Selection.activeGameObject = decals.gameObject;

            GUI.color = Color.red;

            if (GUILayout.Button("X", GUILayout.Width(25f))) {

                DestroyImmediate(decals.gameObject);
                EditorUtility.SetDirty(prop);

            }

            GUI.color = guiColor;

            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (!neons) {

            EditorGUILayout.HelpBox("Neon Manager not found!", MessageType.None);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(120f))) {

                GameObject create = Instantiate(RCC_CustomizationSetups.Instance.neons, prop.transform.position, prop.transform.rotation, prop.transform);
                create.transform.SetParent(prop.transform);
                create.transform.localPosition = Vector3.zero;
                create.transform.localRotation = Quaternion.identity;
                create.name = RCC_CustomizationSetups.Instance.neons.name;

                EditorUtility.SetDirty(prop);

                // Register the creation of the object for undo/redo functionality.
                Undo.RegisterCreatedObjectUndo(create, "Create Neon");

                // Mark the scene as dirty so Unity knows it has changed.
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            }

        } else {

            EditorGUILayout.HelpBox("Neon Manager found!", MessageType.None);

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select", GUILayout.Width(120f)))
                Selection.activeGameObject = neons.gameObject;

            GUI.color = Color.red;

            if (GUILayout.Button("X", GUILayout.Width(25f))) {

                DestroyImmediate(neons.gameObject);
                EditorUtility.SetDirty(prop);

            }

            GUI.color = guiColor;

            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Saving and loading loadouts can be done in the runtime.", MessageType.None);

        EditorGUILayout.BeginHorizontal();

        if (!EditorApplication.isPlaying)
            GUI.enabled = false;

        if (GUILayout.Button("Save\nLoadout"))
            prop.Save();

        if (GUILayout.Button("Load\nLoadout"))
            prop.Load();

        if (GUILayout.Button("Apply\nLoadout"))
            prop.Initialize();

        if (GUILayout.Button("Restore\nLoadout"))
            prop.Delete();

        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

    }

}
