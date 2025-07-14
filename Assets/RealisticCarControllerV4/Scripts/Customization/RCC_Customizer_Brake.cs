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
/// Upgrades brake torque of the car controller.
/// </summary>
public class RCC_Customizer_Brake : RCC_Core {

    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (modApplier == null)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    private int _brakeLevel = 0;

    /// <summary>
    /// Current brake level. Maximum is 5.
    /// </summary>
    public int BrakeLevel {
        get {
            return _brakeLevel;
        }
        set {
            if (value <= 5)
                _brakeLevel = value;
        }
    }

    /// <summary>
    /// Default brake torque of the vehicle.
    /// </summary>
    [HideInInspector] public float defBrake = -1f;

    /// <summary>
    /// Efficiency of the upgrade.
    /// </summary>
    [Range(1f, 2f)] public float efficiency = 1.2f;

    /// <summary>
    /// Updates brake torque and initializes it.
    /// </summary>
    public void Initialize() {

        if (defBrake <= 0)
            defBrake = CarController.brakeTorque;

        CarController.brakeTorque = Mathf.Lerp(defBrake, defBrake * efficiency, BrakeLevel / 5f);

    }

    /// <summary>
    /// Updates brake torque and save it.
    /// </summary>
    public void UpdateStats() {

        if (defBrake <= 0)
            defBrake = CarController.brakeTorque;

        CarController.brakeTorque = Mathf.Lerp(defBrake, defBrake * efficiency, BrakeLevel / 5f);

    }

    public void Restore() {

        BrakeLevel = 0;

        if (defBrake <= 0)
            defBrake = CarController.brakeTorque;

        CarController.brakeTorque = defBrake;

    }

}
