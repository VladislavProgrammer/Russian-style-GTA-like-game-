//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// UI neon button.
/// </summary>
public class RCC_UI_Neon : RCC_Core {

    /// <summary>
    /// Target material.
    /// </summary>
    public Material material;

    public void Upgrade() {

        //  Finding the player vehicle.
        RCC_CarControllerV4 playerVehicle = RCCSceneManager.activePlayerVehicle;

        //  If no player vehicle found, return.
        if (!playerVehicle)
            return;

        //  If player vehicle doesn't have the customizer component, return.
        if (!playerVehicle.Customizer)
            return;

        //  If player vehicle doesn't have the decal manager component, return.
        if (!playerVehicle.Customizer.NeonManager)
            return;

        //  Set the decal.
        playerVehicle.Customizer.NeonManager.Upgrade(material);

    }

}
