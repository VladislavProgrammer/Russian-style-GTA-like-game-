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
/// Upgrades speed of the car controller.
/// </summary>
public class RCC_Customizer_Speed : RCC_Core {

    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (modApplier == null)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    private int _speedLevel = 0;

    /// <summary>
    /// Current speed level. Maximum is 5.
    /// </summary>
    public int SpeedLevel {
        get {
            return _speedLevel;
        }
        set {
            if (value <= 5)
                _speedLevel = value;
        }
    }

    /// <summary>
    /// Default differential ratio of the vehicle.
    /// </summary>
    [HideInInspector] public float defSpeed = -1f;

    /// <summary>
    /// Efficiency of the upgrade.
    /// </summary>
    [Range(1f, 2f)] public float efficiency = 1.2f;

    /// <summary>
    /// Updates differential ratio and initializes it.
    /// </summary>
    public void Initialize() {

        if (defSpeed <= 0)
            defSpeed = CarController.maxspeed;

        CarController.maxspeed = Mathf.Lerp(defSpeed, defSpeed * efficiency, SpeedLevel / 5f);

    }

    /// <summary>
    /// Updates speed and save it.
    /// </summary>
    public void UpdateStats() {

        if (defSpeed <= 0)
            defSpeed = CarController.maxspeed;

        CarController.maxspeed = Mathf.Lerp(defSpeed, defSpeed * efficiency, SpeedLevel / 5f);

    }

    public void Restore() {

        SpeedLevel = 0;

        if (defSpeed <= 0)
            defSpeed = CarController.maxspeed;

        CarController.maxspeed = defSpeed;

    }

}
