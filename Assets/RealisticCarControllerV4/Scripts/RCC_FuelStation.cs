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
/// Fuel station. When a vehicle enters the trigger, fuel tank will be filled up.
/// </summary>
public class RCC_FuelStation : RCC_Core {

    private RCC_CarControllerV4 targetVehicle;      //  Target vehicle.
    public float refillSpeed = 1f;      //  Refill speed.

    public GameObject text;

    private void OnTriggerStay(Collider col) {

        targetVehicle = col.gameObject.GetComponentInParent<RCC_CarControllerV4>();

    }

    private void Update() {

        if (text && Camera.main)
            text.transform.rotation = Camera.main.transform.rotation;

        //  If target vehicle is null, return.
        if (!targetVehicle)
            return;

        //  Refill the tank with given speed * time.
        if (targetVehicle)
            targetVehicle.fuelTank += refillSpeed * Time.deltaTime;

    }

    private void OnTriggerExit(Collider col) {

        //  Setting target vehicle to null when vehicle exits the trigger.
        if (col.gameObject.GetComponentInParent<RCC_CarControllerV4>())
            targetVehicle = null;

    }

}
