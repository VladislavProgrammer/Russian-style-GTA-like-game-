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

/// <summary>
/// UI change wheel button.
/// </summary>
public class RCC_UI_Wheel : RCC_Core {

    public int wheelIndex = 0;

    public void OnClick() {

        RCC_CustomizationManager handler = RCC_CustomizationManager.Instance;

        if (!handler) {

            Debug.LogError("You are trying to customize the vehicle, but there is no ''RCC_CustomizationManager'' in your scene yet.");
            return;

        }

        handler.ChangeWheels(wheelIndex);

    }

}
