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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays UI info.
/// </summary>
public class RCC_InfoLabel : RCC_Singleton<RCC_InfoLabel> {

    private TextMeshProUGUI text;      //  UI text.
    private float timer = 1.5f;       //  Timeout to close the info panel.

    private void Awake() {

        //  Getting text component and disabling it.
        text = GetComponent<TextMeshProUGUI>();

        if (text)
            text.enabled = false;

    }

    private void OnEnable() {

        if (text)
            text.text = "";

        timer = 1.5f;

    }

    private void Update() {

        //  If timer is below 1.5, text is enabled. Otherwise disable.
        if (timer < 1.5f) {

            if (!text.enabled)
                text.enabled = true;

        } else {

            if (text.enabled)
                text.enabled = false;

        }

        //  Increasing timer.
        timer += Time.deltaTime;

    }

    /// <summary>
    /// Shows info.
    /// </summary>
    /// <param name="info"></param>
    public void ShowInfo(string info) {

        timer = 0f;

        //  If no text found, return.
        if (!text)
            return;

        //  Display info.
        text.text = info;

    }

    private void OnDisable() {

        timer = 1.5f;

        //  If no text found, return.
        if (!text)
            return;

        text.text = "";

    }

}
