//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Modification manager. You'll need this script in your scene to make customization work. UI elements are not required, optional.
/// </summary>
public class RCC_CustomizationManager : RCC_Singleton<RCC_CustomizationManager> {

    public RCC_Customizer vehicle;       //	Current player vehicle.

    public RCC_CustomizationManager() {

        if (RCCSceneManager.activePlayerVehicle)
            vehicle = RCCSceneManager.activePlayerVehicle.Customizer;

    }

    private void Start() {

        if (RCCSceneManager.activePlayerVehicle)
            vehicle = RCCSceneManager.activePlayerVehicle.Customizer;

    }

    private void OnEnable() {

        RCC_SceneManager.OnVehicleChanged += RCC_SceneManager_OnVehicleChanged;

    }

    private void RCC_SceneManager_OnVehicleChanged() {

        vehicle = RCCSceneManager.activePlayerVehicle.Customizer;

    }

    /// <summary>
    /// Sets the new target as currentApplier.
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetTarget(RCC_Customizer newTarget) {

        vehicle = newTarget;

    }

    /// <summary>
    /// Paints the target vehicle.
    /// </summary>
    /// <param name="color"></param>
    public void Paint(Color color) {

        // If no any player vehicle, return.
        if (!vehicle)
            return;

        vehicle.PaintManager.Paint(color);

        if (vehicle.SpoilerManager && vehicle.SpoilerManager.paintSpoilers)
            vehicle.SpoilerManager.Paint(color);

    }

    /// <summary>
    /// Changes the wheels of the target vehicle.
    /// </summary>
    /// <param name="wheelIndex"></param>
    public void ChangeWheels(int wheelIndex) {

        // If no any player vehicle, return.
        if (!vehicle)
            return;

        vehicle.WheelManager.UpdateWheel(wheelIndex);

    }

    /// <summary>
    /// Upgrades speed of the target vehicle.
    /// </summary>
    public void UpgradeSpeed() {

        // If no any player vehicle, return.
        if (!vehicle)
            return;

        vehicle.UpgradeManager.UpgradeEngine();

    }

    /// <summary>
    /// Upgrades handling of the target vehicle.
    /// </summary>
    public void UpgradeHandling() {

        // If no any player vehicle, return.
        if (!vehicle)
            return;

        vehicle.UpgradeManager.UpgradeHandling();

    }

    /// <summary>
    /// Upgrades brakes of the target vehicle.
    /// </summary>
    public void UpgradeBrake() {

        // If no any player vehicle, return.
        if (!vehicle)
            return;

        vehicle.UpgradeManager.UpgradeBrake();

    }

    /// <summary>
    /// Changes the spoiler of the target vehicle.
    /// </summary>
    /// <param name="index"></param>
    public void Spoiler(int index) {

        // If no any player vehicle, return.
        if (!vehicle)
            return;

        vehicle.SpoilerManager.Upgrade(index);

    }

    /// <summary>
    /// Changes the siren of the target vehicle.
    /// </summary>
    /// <param name="index"></param>
    public void Siren(int index) {

        // If no any player vehicle, return.
        if (!vehicle)
            return;

        vehicle.SirenManager.Upgrade(index);

    }

    private void OnDisable() {

        RCC_SceneManager.OnVehicleChanged -= RCC_SceneManager_OnVehicleChanged;

    }

}
