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
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using System;

public class RCC_PopupWindow_PossibleWheels : EditorWindow {

    public static GameObject[] possibleWheels = null;
    public static List<GameObject> allSelectedWheels = new List<GameObject>();

    // UnityAction delegate
    public Action<GameObject[]> onButtonClick;

    GUISkin skin;

    public void Awake() {

        skin = Resources.Load("RCC_WindowSkin") as GUISkin;

    }

    // Method to show the window
    public static void ShowWindow(GameObject[] allPossibleWheels, Action<GameObject[]> onButtonClickAction) {

        possibleWheels = allPossibleWheels;

        // Create an instance of the popup window
        RCC_PopupWindow_PossibleWheels window = (RCC_PopupWindow_PossibleWheels)GetWindow(typeof(RCC_PopupWindow_PossibleWheels), true, "RCC_PopupWindow_PossibleWheels");
        window.onButtonClick = onButtonClickAction; // Assign the action
        window.Show();

    }

    // Called when the window is created and opened
    private void OnEnable() {

        allSelectedWheels = new List<GameObject>();
        minSize = new Vector2(500, 300);

    }

    // Method to render the window's content
    private void OnGUI() {

        GUI.skin = skin;

        // Add any GUI elements here
        GUILayout.Label("I've found these possible gameobjects for front wheels, but I'm not %100 sure.\nCan you verify the wheels for me?", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("I can list children gameobjects of a wheel as well, be sure to pick the parent wheel!", MessageType.None);

        if (possibleWheels != null) {

            for (int i = 0; i < possibleWheels.Length; i++) {

                Color guiColor = GUI.color;
                string buttonString = "Select: ";

                if (allSelectedWheels.Contains(possibleWheels[i])) {

                    GUI.color = Color.green;
                    buttonString = "Selected: ";

                }

                if (GUILayout.Button(buttonString + possibleWheels[i].name)) {

                    if (!allSelectedWheels.Contains(possibleWheels[i]))
                        allSelectedWheels.Add(possibleWheels[i]);
                    else
                        allSelectedWheels.Remove(possibleWheels[i]);

                }

                GUI.color = guiColor;

            }

        }

        GUILayout.FlexibleSpace();

        EditorGUILayout.HelpBox("If axes of your model is wrong, there will be miscalculations!", MessageType.None);

        // Add more GUI elements as needed
        if (GUILayout.Button("Save & Close")) {

            onButtonClick?.Invoke(allSelectedWheels.ToArray()); // Invoke the UnityAction when the button is clicked
            allSelectedWheels = new List<GameObject>();
            this.Close(); // Close the window when the button is clicked

        }

    }

}
