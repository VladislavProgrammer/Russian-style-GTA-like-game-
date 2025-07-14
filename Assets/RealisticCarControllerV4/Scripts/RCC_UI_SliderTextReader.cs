//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Receives float from UI Slider, and displays the value as a text.
/// </summary>
public class RCC_UI_SliderTextReader : RCC_Core {

    public Slider slider;       //  UI Slider.
    public TextMeshProUGUI text;       //  UI Text.

    private void Awake() {

        if (!slider)
            slider = GetComponentInParent<Slider>();

        if (!text)
            text = GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Update() {

        if (!slider || !text)
            return;

        text.text = slider.value.ToString("F1");

    }

}
