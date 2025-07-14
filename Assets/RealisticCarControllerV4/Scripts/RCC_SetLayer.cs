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

public class RCC_SetLayer : RCC_Core {

    public string layer = "Default";
    public bool setChildren = false;

    private void OnEnable() {

        gameObject.layer = LayerMask.NameToLayer(layer);

        if (setChildren) {

            foreach (Transform item in transform)
                item.gameObject.layer = LayerMask.NameToLayer(layer);

        }

    }

}
