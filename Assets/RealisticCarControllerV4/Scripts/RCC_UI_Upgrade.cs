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
using TMPro;

/// <summary>
/// UI upgrade button.
/// </summary>
public class RCC_UI_Upgrade : RCC_Core {

    public UpgradeClass upgradeClass = UpgradeClass.Engine;
    public enum UpgradeClass { Engine, Handling, Brake, Speed }

    public TextMeshProUGUI levelText;

    private void OnEnable() {

        CheckLevel();

    }

    public void CheckLevel() {

        if (!levelText)
            return;

        RCC_CarControllerV4 currentPlayer = RCCSceneManager.activePlayerVehicle;

        if (!currentPlayer)
            return;

        if (!currentPlayer) {

            Debug.LogError("There are no any player controller vehicle in the scene yet.");
            levelText.text = "-";
            return;

        }

        if (!currentPlayer.Customizer) {

            Debug.LogError("You are trying to customize the player vehicle, customization is not enabled on this vehicle.");
            levelText.text = "-";
            return;

        }

        if (!currentPlayer.Customizer.UpgradeManager) {

            Debug.LogError("You are trying to customize the player vehicle, customization is enabled but upgrade manager couldn't found on this vehicle.");
            levelText.text = "-";
            return;

        }

        switch (upgradeClass) {

            case UpgradeClass.Engine:
                levelText.text = currentPlayer.Customizer.UpgradeManager.EngineLevel.ToString();
                break;
            case UpgradeClass.Handling:
                levelText.text = currentPlayer.Customizer.UpgradeManager.HandlingLevel.ToString();
                break;
            case UpgradeClass.Brake:
                levelText.text = currentPlayer.Customizer.UpgradeManager.BrakeLevel.ToString();
                break;
            case UpgradeClass.Speed:
                levelText.text = currentPlayer.Customizer.UpgradeManager.SpeedLevel.ToString();
                break;

        }

    }

    public void OnClick() {

        RCC_CarControllerV4 currentPlayer = RCCSceneManager.activePlayerVehicle;

        if (!currentPlayer) {

            Debug.LogError("There are no any player controller vehicle in the scene yet.");
            return;

        }

        if (!currentPlayer.Customizer) {

            Debug.LogError("You are trying to customize the player vehicle, customization is not enabled on this vehicle.");
            return;

        }

        if (!currentPlayer.Customizer.UpgradeManager) {

            Debug.LogError("You are trying to customize the player vehicle, customization is enabled but upgrade manager couldn't found on this vehicle.");
            levelText.text = "-";
            return;

        }

        switch (upgradeClass) {

            case UpgradeClass.Engine:
                if (currentPlayer.Customizer.UpgradeManager.EngineLevel < 5)
                    currentPlayer.Customizer.UpgradeManager.UpgradeEngine();
                break;
            case UpgradeClass.Handling:
                if (currentPlayer.Customizer.UpgradeManager.HandlingLevel < 5)
                    currentPlayer.Customizer.UpgradeManager.UpgradeHandling();
                break;
            case UpgradeClass.Brake:
                if (currentPlayer.Customizer.UpgradeManager.BrakeLevel < 5)
                    currentPlayer.Customizer.UpgradeManager.UpgradeBrake();
                break;
            case UpgradeClass.Speed:
                if (currentPlayer.Customizer.UpgradeManager.SpeedLevel < 5)
                    currentPlayer.Customizer.UpgradeManager.UpgradeSpeed();
                break;

        }

        CheckLevel();

    }

}
