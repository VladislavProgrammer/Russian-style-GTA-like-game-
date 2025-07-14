//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Mobile UI Drag used for orbiting Showroom Camera.
/// </summary>
public class RCC_UI_ShowroomCameraDrag : RCC_Core, IDragHandler, IEndDragHandler {

    private RCC_ShowroomCamera showroomCamera;

    private void Awake() {

        showroomCamera = FindObjectOfType<RCC_ShowroomCamera>();

    }

    public void OnDrag(PointerEventData data) {

        if (showroomCamera)
            showroomCamera.OnDrag(data);

    }

    public void OnEndDrag(PointerEventData data) {



    }

}
