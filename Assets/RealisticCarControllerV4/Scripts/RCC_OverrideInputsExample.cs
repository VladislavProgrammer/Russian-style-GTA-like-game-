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
using UnityEngine.UI;
using TMPro;

public class RCC_OverrideInputsExample : RCC_Core {

    public RCC_CarControllerV4 targetVehicle;
    public bool takePlayerVehicle = true;
    public RCC_Inputs newInputs = new RCC_Inputs();

    private bool overrideNow = false;

    public Slider throttle;
    public Slider brake;
    public Slider steering;
    public Slider handbrake;
    public Slider nos;

    public TextMeshProUGUI statusText;

    private void Update() {

        newInputs.throttleInput = throttle.value;
        newInputs.brakeInput = brake.value;
        newInputs.steerInput = steering.value;
        newInputs.handbrakeInput = handbrake.value;
        newInputs.boostInput = nos.value;

        if (takePlayerVehicle)
            targetVehicle = RCCSceneManager.activePlayerVehicle;

        if (targetVehicle && overrideNow)
            targetVehicle.OverrideInputs(newInputs);

        if (statusText && targetVehicle)
            statusText.text = "Status: " + (overrideNow ? "Enabled" : "Disabled");

    }

    public void EnableOverride() {

        if (!targetVehicle)
            return;

        overrideNow = true;

        if (targetVehicle)
            targetVehicle.OverrideInputs(newInputs);

    }

    public void DisableOverride() {

        if (!targetVehicle)
            return;

        overrideNow = false;

        if (targetVehicle)
            targetVehicle.DisableOverrideInputs();

    }

}
