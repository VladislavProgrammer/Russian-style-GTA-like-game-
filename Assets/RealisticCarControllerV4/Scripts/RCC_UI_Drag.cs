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
using UnityEngine.EventSystems;

/// <summary>
/// Mobile UI Drag used for orbiting RCC Camera.
/// </summary>
public class RCC_UI_Drag : RCC_Core, IDragHandler, IEndDragHandler {

    /// <summary>
    /// While dragging.
    /// </summary>
    /// <param name="data"></param>
    public void OnDrag(PointerEventData data) {

        //  Return if no player camera found.
        if (!RCCSceneManager.activePlayerCamera)
            return;

        RCCSceneManager.activePlayerCamera.OnDrag(data);

    }

    public void OnEndDrag(PointerEventData data) {



    }

}
