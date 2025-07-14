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

/// <summary>
/// Teleports the vehicle in zone to the target spawn point.
/// </summary>
public class RCC_Teleporter : RCC_Core {

    public Transform spawnPoint;        //  Target spawn point.

    private void OnTriggerEnter(Collider col) {

        //  If trigger enabled, return.
        if (col.isTrigger)
            return;

        //  Getting car controller.
        RCC_CarControllerV4 carController = col.gameObject.GetComponentInParent<RCC_CarControllerV4>();

        //  If no car controller found, return.
        if (!carController)
            return;

        //  Transport the vehicle.
        RCC.Transport(carController, spawnPoint.position, spawnPoint.rotation);

    }

}
