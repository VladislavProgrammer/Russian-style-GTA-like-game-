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
/// Upgrades traction strength of the car controller.
/// </summary>
public class RCC_Customizer_Handling : RCC_Core {

    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (modApplier == null)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    /// <summary>
    /// Current handling level.
    /// </summary>
    private int _handlingLevel = 0;
    public int HandlingLevel {
        get {
            return _handlingLevel;
        }
        set {
            if (value <= 5)
                _handlingLevel = value;
        }
    }

    /// <summary>
    /// Default handling strength.
    /// </summary>
    [HideInInspector] public float defHandling = -1f;

    /// <summary>
    /// Efficiency of the upgrade.
    /// </summary>
    [Range(1f, 2f)] public float efficiency = 1.2f;

    /// <summary>
    /// Updates handling and initializes it.
    /// </summary>
    public void Initialize() {

        if (defHandling <= 0)
            defHandling = CarController.tractionHelperStrength;

        CarController.tractionHelperStrength = Mathf.Lerp(defHandling, defHandling * efficiency, HandlingLevel / 5f);

    }

    /// <summary>
    /// Updates handling strength and save it.
    /// </summary>
    public void UpdateStats() {

        if (defHandling <= 0)
            defHandling = CarController.tractionHelperStrength;

        CarController.tractionHelperStrength = Mathf.Lerp(defHandling, defHandling * efficiency, HandlingLevel / 5f);

    }

    public void Restore() {

        HandlingLevel = 0;

        if (defHandling <= 0)
            defHandling = CarController.tractionHelperStrength;

        CarController.tractionHelperStrength = defHandling;

    }

}
