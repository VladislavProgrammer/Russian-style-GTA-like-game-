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
/// Locks rotation of the shadow projector to avoid stretching.
/// </summary>
public class RCC_ShadowRotConst : RCC_Core {

    private void Update() {

        transform.rotation = Quaternion.Euler(90f, CarController.transform.eulerAngles.y, 0f);

    }

}
