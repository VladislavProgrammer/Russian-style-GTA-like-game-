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
/// Manager for all upgradable scripts (Engine, Brake, Handling).
/// </summary>
public class RCC_Customizer_UpgradeManager : RCC_Core {

    //  Mod applier.
    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (modApplier == null)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    /// <summary>
    /// Upgradable engine component.
    /// </summary>
    public RCC_Customizer_Engine Engine {

        get {

            if (engine == null)
                engine = GetComponentInChildren<RCC_Customizer_Engine>(true);

            if (engine == null) {

                GameObject newEngine = new GameObject("Engine");
                newEngine.transform.SetParent(transform);
                newEngine.transform.localPosition = Vector3.zero;
                newEngine.transform.localRotation = Quaternion.identity;
                engine = newEngine.AddComponent<RCC_Customizer_Engine>();

            }

            return engine;

        }

    }
    private RCC_Customizer_Engine engine;

    /// <summary>
    /// Upgradable brake component.
    /// </summary>
    public RCC_Customizer_Brake Brake {

        get {

            if (brake == null)
                brake = GetComponentInChildren<RCC_Customizer_Brake>(true);

            if (brake == null) {

                GameObject newBrake = new GameObject("Brake");
                newBrake.transform.SetParent(transform);
                newBrake.transform.localPosition = Vector3.zero;
                newBrake.transform.localRotation = Quaternion.identity;
                brake = newBrake.AddComponent<RCC_Customizer_Brake>();

            }

            return brake;

        }

    }

    private RCC_Customizer_Brake brake;

    /// <summary>
    /// Upgradable handling component.
    /// </summary>
    public RCC_Customizer_Handling Handling {

        get {

            if (handling == null)
                handling = GetComponentInChildren<RCC_Customizer_Handling>(true);

            if (handling == null) {

                GameObject newHandling = new GameObject("Handling");
                newHandling.transform.SetParent(transform);
                newHandling.transform.localPosition = Vector3.zero;
                newHandling.transform.localRotation = Quaternion.identity;
                handling = newHandling.AddComponent<RCC_Customizer_Handling>();

            }

            return handling;

        }

    }

    private RCC_Customizer_Handling handling;

    /// <summary>
    /// Upgradable speed component.
    /// </summary>
    public RCC_Customizer_Speed Speed {

        get {

            if (speed == null)
                speed = GetComponentInChildren<RCC_Customizer_Speed>(true);

            if (handling == null) {

                GameObject newSpeed = new GameObject("Speed");
                newSpeed.transform.SetParent(transform);
                newSpeed.transform.localPosition = Vector3.zero;
                newSpeed.transform.localRotation = Quaternion.identity;
                speed = newSpeed.AddComponent<RCC_Customizer_Speed>();

            }

            return speed;

        }

    }

    private RCC_Customizer_Speed speed;

    /// <summary>
    /// Current upgraded engine level.
    /// </summary>
    public int EngineLevel {

        get {

            if (Engine != null)
                return Engine.EngineLevel;

            return 0;

        }

    }

    /// <summary>
    /// Current upgraded brake level.
    /// </summary>
    public int BrakeLevel {

        get {

            if (Engine != null)
                return Brake.BrakeLevel;

            return 0;

        }

    }

    /// <summary>
    /// Current upgraded handling level.
    /// </summary>
    public int HandlingLevel {

        get {

            if (Engine != null)
                return Handling.HandlingLevel;

            return 0;

        }

    }

    /// <summary>
    /// Current upgraded speed level.
    /// </summary>
    public int SpeedLevel {

        get {

            if (Speed != null)
                return Speed.SpeedLevel;

            return 0;

        }

    }

    public void Initialize() {

        if (Engine) {

            //  Setting upgraded engine torque if saved.
            Engine.EngineLevel = ModApplier.loadout.engineLevel;
            Engine.Initialize();

        }

        if (Brake) {

            //  Setting upgraded brake torque if saved.
            Brake.BrakeLevel = ModApplier.loadout.brakeLevel;
            Brake.Initialize();

        }

        if (Handling) {

            //  Setting upgraded handling strength if saved.
            Handling.HandlingLevel = ModApplier.loadout.handlingLevel;
            Handling.Initialize();

        }

        if (Speed) {

            //  Setting upgraded speed if saved.
            Speed.SpeedLevel = ModApplier.loadout.speedLevel;
            Speed.Initialize();

        }

    }

    /// <summary>
    /// Upgrades the engine torque.
    /// </summary>
    public void UpgradeEngine() {

        //  If engine is missing, return.
        if (!Engine)
            return;

        //  If level is maximum, return.
        if (EngineLevel >= 5)
            return;

        //  Upgrading.
        Engine.EngineLevel++;
        Engine.UpdateStats();

        //  Refreshing the loadout.
        ModApplier.Refresh(this);

        //  Saving the loadout.
        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Upgrades the brake torque.
    /// </summary>
    public void UpgradeBrake() {

        //  If brake is missing, return.
        if (!Brake)
            return;

        //  If level is maximum, return.
        if (BrakeLevel >= 5)
            return;

        //  Upgrading.
        Brake.BrakeLevel++;
        Brake.UpdateStats();

        //  Refreshing the loadout.
        ModApplier.Refresh(this);

        //  Saving the loadout.
        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Upgrades the traction helper (Handling).
    /// </summary>
    public void UpgradeHandling() {

        //  If handling is missing, return.
        if (!Handling)
            return;

        //  If level is maximum, return.
        if (HandlingLevel >= 5)
            return;

        //  Upgrading.
        Handling.HandlingLevel++;
        Handling.UpdateStats();

        //  Refreshing the loadout.
        ModApplier.Refresh(this);

        //  Saving the loadout.
        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Upgrades the speed.
    /// </summary>
    public void UpgradeSpeed() {

        //  If speed is missing, return.
        if (!Speed)
            return;

        //  If level is maximum, return.
        if (SpeedLevel >= 5)
            return;

        //  Upgrading.
        Speed.SpeedLevel++;
        Speed.UpdateStats();

        //  Refreshing the loadout.
        ModApplier.Refresh(this);

        //  Saving the loadout.
        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Upgrades the engine torque.
    /// </summary>
    public void UpgradeEngineWithoutSave(int level) {

        //  If engine is missing, return.
        if (!Engine)
            return;

        //  If level is maximum, return.
        if (level >= 5)
            return;

        //  Upgrading.
        Engine.EngineLevel = level;
        Engine.UpdateStats();

    }

    /// <summary>
    /// Upgrades the brake torque.
    /// </summary>
    public void UpgradeBrakeWithoutSave(int level) {

        //  If brake is missing, return.
        if (!Brake)
            return;

        //  If level is maximum, return.
        if (level >= 5)
            return;

        //  Upgrading.
        Brake.BrakeLevel = level;
        Brake.UpdateStats();

    }

    /// <summary>
    /// Upgrades the traction helper (Handling).
    /// </summary>
    public void UpgradeHandlingWithoutSave(int level) {

        //  If handling is missing, return.
        if (!Handling)
            return;

        //  If level is maximum, return.
        if (level >= 5)
            return;

        //  Upgrading.
        Handling.HandlingLevel = level;
        Handling.UpdateStats();

    }

    /// <summary>
    /// Upgrades the soeed.
    /// </summary>
    public void UpgradeSpeedWithoutSave(int level) {

        //  If handling is missing, return.
        if (!Speed)
            return;

        //  If level is maximum, return.
        if (level >= 5)
            return;

        //  Upgrading.
        Speed.SpeedLevel = level;
        Speed.UpdateStats();

    }

    /// <summary>
    /// Restores the settings to default.
    /// </summary>
    public void Restore() {

        //  Getting defalut values of the car controller.
        if (Engine)
            Engine.Restore();

        if (Brake)
            Brake.Restore();

        if (Handling)
            Handling.Restore();

        if (Speed)
            Speed.Restore();

    }

}
