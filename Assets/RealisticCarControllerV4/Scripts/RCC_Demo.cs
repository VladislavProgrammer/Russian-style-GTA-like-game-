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
using UnityEngine.SceneManagement;
#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif

/// <summary>
/// A simple manager script for all demo scenes. It has an array of spawnable player vehicles, public methods, setting new behavior modes, restart, and quit application.
/// </summary>
public class RCC_Demo : RCC_Core {

    [HideInInspector] public int selectedVehicleIndex = 0;      // An integer index value used to spawn a new vehicle.
    [HideInInspector] public int selectedBehaviorIndex = 0;     // An integer index value used to set a new behavior mode.

    /// <summary>
    /// An integer index value used for spawning a new vehicle.
    /// </summary>
    /// <param name="index"></param>
    public void SelectVehicle(int index) {

        selectedVehicleIndex = index;

    }

#if PHOTON_UNITY_NETWORKING && RCC_PHOTON

    /// <summary>
    /// An integer index value used for spawning a new photon prefab vehicle.
    /// </summary>
    /// <param name="index"></param>
    public void SelectPhotonVehicle(int index) {

        selectedVehicleIndex = index;

    }

#endif

    /// <summary>
    /// Spawns the player vehicle.
    /// </summary>
    public void Spawn() {

        // Last known position and rotation of last active vehicle.
        Vector3 lastKnownPos = new Vector3();
        Quaternion lastKnownRot = new Quaternion();

        // Checking if there is a player vehicle on the scene.
        if (RCCSceneManager.activePlayerVehicle) {

            lastKnownPos = RCCSceneManager.activePlayerVehicle.transform.position;
            lastKnownRot = RCCSceneManager.activePlayerVehicle.transform.rotation;

        }

        // If last known position and rotation is not assigned, camera's position and rotation will be used.
        if (lastKnownPos == Vector3.zero) {

            if (RCCSceneManager.activePlayerCamera) {

                lastKnownPos = RCCSceneManager.activePlayerCamera.transform.position;
                lastKnownRot = RCCSceneManager.activePlayerCamera.transform.rotation;

            }

        }

        // We don't need X and Z rotation angle. Just Y.
        lastKnownRot.x = 0f;
        lastKnownRot.z = 0f;

        // Is there any last vehicle?
        RCC_CarControllerV4 lastVehicle = RCCSceneManager.activePlayerVehicle;

#if BCG_ENTEREXIT

        BCG_EnterExitVehicle lastEnterExitVehicle;
        bool enterExitVehicleFound = false;

        if (lastVehicle) {

            lastEnterExitVehicle = lastVehicle.GetComponentInChildren<BCG_EnterExitVehicle>();

            if (lastEnterExitVehicle && lastEnterExitVehicle.driver) {

                enterExitVehicleFound = true;
                lastEnterExitVehicle.driver.GetOut();

            }

        }

#endif

        // If we have controllable vehicle by player on scene, destroy it.
        if (lastVehicle)
            Destroy(lastVehicle.gameObject);

        // Here we are creating our new vehicle.
        RCC.SpawnRCC(RCC_DemoVehicles.Instance.vehicles[selectedVehicleIndex], lastKnownPos, lastKnownRot, true, true, true);

#if BCG_ENTEREXIT

        if (enterExitVehicleFound) {

            lastEnterExitVehicle = null;

            lastEnterExitVehicle = RCC_SceneManager.Instance.activePlayerVehicle.GetComponentInChildren<BCG_EnterExitVehicle>();

            if (!lastEnterExitVehicle)
                lastEnterExitVehicle = RCC_SceneManager.Instance.activePlayerVehicle.gameObject.AddComponent<BCG_EnterExitVehicle>();

            if (BCG_EnterExitManager.Instance.activePlayer && lastEnterExitVehicle && lastEnterExitVehicle.driver == null) {

                BCG_EnterExitManager.Instance.activePlayer.GetIn(lastEnterExitVehicle);

            }

        }

#endif

    }

#if PHOTON_UNITY_NETWORKING && RCC_PHOTON

    /// <summary>
    /// Spawns the photon player vehicle.
    /// </summary>
    public void SpawnPhoton() {

        // Last known position and rotation of last active vehicle.
        Vector3 lastKnownPos = new Vector3();
        Quaternion lastKnownRot = new Quaternion();

        // Checking if there is a player vehicle on the scene.
        if (RCCSceneManager.activePlayerVehicle) {

            lastKnownPos = RCCSceneManager.activePlayerVehicle.transform.position;
            lastKnownRot = RCCSceneManager.activePlayerVehicle.transform.rotation;

        }

        // If last known position and rotation is not assigned, camera's position and rotation will be used.
        if (lastKnownPos == Vector3.zero) {

            if (RCCSceneManager.activePlayerCamera) {

                lastKnownPos = RCCSceneManager.activePlayerCamera.transform.position;
                lastKnownRot = RCCSceneManager.activePlayerCamera.transform.rotation;

            }

        }

        // We don't need X and Z rotation angle. Just Y.
        lastKnownRot.x = 0f;
        lastKnownRot.z = 0f;

        // Is there any last vehicle?
        RCC_CarControllerV4 lastVehicle = RCCSceneManager.activePlayerVehicle;

        // If we have controllable vehicle by player on scene, destroy it.
        if (lastVehicle)
            PhotonNetwork.Destroy(lastVehicle.gameObject);

        // Here we are creating our new vehicle.
        RCC_CarControllerV4 spawnedPhotonVehicle = PhotonNetwork.Instantiate("Photon Vehicles/" + RCC_DemoVehicles.Instance.vehicles[selectedVehicleIndex].gameObject.name, lastKnownPos, lastKnownRot).GetComponent<RCC_CarControllerV4>();

        //  And registering the vehicle.
        RCC.RegisterPlayerVehicle(spawnedPhotonVehicle, true, true);

    }

#endif

    /// <summary>
    /// An integer index value used for setting behavior mode.
    /// </summary>
    /// <param name="index"></param>
    public void SetBehavior(int index) {

        selectedBehaviorIndex = index;

    }

    /// <summary>
    /// Here we are setting new selected behavior to corresponding one.
    /// </summary>
    public void InitBehavior() {

        RCC.SetBehavior(selectedBehaviorIndex);

    }

    /// <summary>
    /// Sets the mobile controller type.
    /// </summary>
    /// <param name="index"></param>
    public void SetMobileController(int index) {

        switch (index) {

            case 0:
                RCC.SetMobileController(RCC_Settings.MobileController.TouchScreen);
                break;
            case 1:
                RCC.SetMobileController(RCC_Settings.MobileController.Gyro);
                break;
            case 2:
                RCC.SetMobileController(RCC_Settings.MobileController.SteeringWheel);
                break;
            case 3:
                RCC.SetMobileController(RCC_Settings.MobileController.Joystick);
                break;

        }

    }

    /// <summary>
    /// Sets the quality.
    /// </summary>
    /// <param name="index">Index.</param>
    public void SetQuality(int index) {

        QualitySettings.SetQualityLevel(index);

    }

    /// <summary>
    /// Simply restarting the current scene.
    /// </summary>
    public void RestartScene() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    /// <summary>
    /// Simply quit application. Not working on Editor.
    /// </summary>
    public void Quit() {

        Application.Quit();

    }

}
