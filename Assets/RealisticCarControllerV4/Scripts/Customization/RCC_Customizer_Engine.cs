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
/// Upgrades engine of the car controller.
/// </summary>
public class RCC_Customizer_Engine : RCC_Core {

    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (modApplier == null)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    private int _engineLevel = 0;

    /// <summary>
    /// Current engine level. Maximum is 5.
    /// </summary>
    public int EngineLevel {
        get {
            return _engineLevel;
        }
        set {
            if (value <= 5)
                _engineLevel = value;
        }
    }

    /// <summary>
    /// Default engine torque.
    /// </summary>
    [HideInInspector] public float defEngine = -1f;

    /// <summary>
    /// Efficiency of the upgrade.
    /// </summary>
    [Range(1f, 2f)] public float efficiency = 1.2f;

    /// <summary>
    /// Updates engine torque and initializes it.
    /// </summary>
    public void Initialize() {

        if (defEngine <= 0)
            defEngine = CarController.maxEngineTorque;

        CarController.maxEngineTorque = Mathf.Lerp(defEngine, defEngine * efficiency, EngineLevel / 5f);

    }

    /// <summary>
    /// Updates engine torque and save it.
    /// </summary>
    public void UpdateStats() {

        if (defEngine <= 0)
            defEngine = CarController.maxEngineTorque;

        CarController.maxEngineTorque = Mathf.Lerp(defEngine, defEngine * efficiency, EngineLevel / 5f);

    }

    public void Restore() {

        EngineLevel = 0;

        if (defEngine <= 0)
            defEngine = CarController.maxEngineTorque;

        CarController.maxEngineTorque = defEngine;

    }

}
