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

/// <summary>
/// UI siren button.
/// </summary>
public class RCC_UI_Siren : RCC_Core {

    public int index = 0;

    public void Upgrade() {

        RCC_CustomizationManager handler = RCC_CustomizationManager.Instance;

        if (!handler) {

            Debug.LogError("You are trying to customize the vehicle, but there is no ''RCC_CustomizationManager'' in your scene yet.");
            return;

        }

        handler.Siren(index);

    }

}
