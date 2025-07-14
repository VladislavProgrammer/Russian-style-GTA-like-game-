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
/// Customization trigger used on customization demo scene. It will enable customization mode when player vehicle triggers.
/// </summary>
public class RCC_CustomizationTrigger : RCC_Core {

    public GameObject trigger;      //  Trigger object.
    private RCC_CarControllerV4 vehicle;        //  Current vehicle.

    private void OnTriggerEnter(Collider other) {

        //  Getting car controller.
        RCC_CarControllerV4 carController = other.GetComponentInParent<RCC_CarControllerV4>();

        //  If trigger is not a vehicle, return.
        if (!carController)
            return;

        if (!RCC_CustomizationDemo.Instance) {

            Debug.LogError("''RCC_CustomizationDemo'' couldn't found in the scene!");
            return;

        }

        //  Enable customization mode, disable trigger.
        RCC_CustomizationDemo.Instance.EnableCustomization(carController);

        trigger.SetActive(false);

        vehicle = carController;

    }

    private void Update() {

        //  If no any vehicle triggered, return.
        if (trigger.activeSelf || !vehicle)
            return;

        //  Id distance is higher than 20 meters, reenable the trigger again.
        if (Vector3.Distance(transform.position, vehicle.transform.position) > 20f) {

            trigger.SetActive(true);
            vehicle = null;

        }

    }

}
