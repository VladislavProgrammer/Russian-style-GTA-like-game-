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
/// Repairs the vehicle in the zone.
/// </summary>
public class RCC_RepairStation : RCC_Core {

    private RCC_CarControllerV4 targetVehicle;      //  Target vehicle in the zone.

    public GameObject text;

    private void OnTriggerEnter(Collider col) {

        //  If trigger enabled, return.
        if (col.isTrigger)
            return;

        //  Get the vehicle in the zone.
        if (targetVehicle == null)
            targetVehicle = col.gameObject.GetComponentInParent<RCC_CarControllerV4>();

        //  And repair if target vehicle found in the zone.
        if (targetVehicle)
            targetVehicle.Repair();

    }

    private void Update() {

        if (text && Camera.main)
            text.transform.rotation = Camera.main.transform.rotation;

    }

    private void OnTriggerExit(Collider col) {

        if (!targetVehicle)
            return;

        //  Setting target vehicle to null if it gets out of the zone.
        if (col.gameObject.GetComponentInParent<RCC_CarControllerV4>())
            targetVehicle = null;

    }

}
