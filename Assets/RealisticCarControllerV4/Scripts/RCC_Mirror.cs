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

/// <summary>
/// It must be attached to external camera. This external camera will be used as mirror.
/// </summary>
public class RCC_Mirror : RCC_Core {

    private Camera cam;     //  Getting camera component.

    private void Awake() {

        //  Getting camera.
        cam = GetComponent<Camera>();

        //  If no camera found, return.
        if (!cam)
            enabled = false;

        //  Inverting the camera for mirror effect.
        InvertCamera();

    }

    private void OnEnable() {

        StartCoroutine(FixDepth());

    }

    /// <summary>
    /// Fixing depth of the camera.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FixDepth() {

        yield return new WaitForEndOfFrame();
        cam.depth = 1f;

    }

    /// <summary>
    /// Inverting the camera for mirror effect.
    /// </summary>
    private void InvertCamera() {

        cam.ResetWorldToCameraMatrix();
        cam.ResetProjectionMatrix();
        cam.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));

    }

    private void OnPreRender() {

        GL.invertCulling = true;

    }

    private void OnPostRender() {

        GL.invertCulling = false;

    }

    private void Update() {

        //  Enable or disable with controllable state of the vehicle.
        cam.enabled = CarController.canControl;

    }

}
