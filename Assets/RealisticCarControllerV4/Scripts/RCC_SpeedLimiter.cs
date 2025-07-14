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
/// Used to slow down the vehicle by increasing drag.
/// </summary>
public class RCC_SpeedLimiter : RCC_Core {

    private float defaultDrag = -1f;

    private void OnTriggerStay(Collider other) {

        RCC_CarControllerV4 carController = other.GetComponentInParent<RCC_CarControllerV4>();

        if (!carController)
            return;

        if (defaultDrag == -1)
            defaultDrag = carController.Rigid.drag;

        carController.Rigid.drag = .02f * carController.speed;

    }

    private void OnTriggerExit(Collider other) {

        RCC_CarControllerV4 carController = other.GetComponentInParent<RCC_CarControllerV4>();

        if (!carController)
            return;

        carController.Rigid.drag = defaultDrag;

    }

}
