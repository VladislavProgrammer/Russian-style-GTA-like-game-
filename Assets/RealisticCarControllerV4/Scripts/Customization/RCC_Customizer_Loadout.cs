//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Customization loadout.
/// </summary>
[System.Serializable]
public class RCC_Customizer_Loadout {

    public Color paint = new Color(1f, 1f, 1f, 0f);
    public int spoiler = -1;
    public int siren = -1;
    public int wheel = -1;

    public int engineLevel = 0;
    public int handlingLevel = 0;
    public int brakeLevel = 0;
    public int speedLevel = 0;

    public int decalIndexFront = -1;
    public int decalIndexBack = -1;
    public int decalIndexLeft = -1;
    public int decalIndexRight = -1;

    public int neonIndex = -1;

    public RCC_CustomizationData customizationData = new RCC_CustomizationData();

    public void UpdateLoadout(MonoBehaviour component) {

        switch (component) {

            case RCC_Customizer_WheelManager:

                RCC_Customizer_WheelManager wheelComponent = (RCC_Customizer_WheelManager)component;
                wheel = wheelComponent.wheelIndex;
                break;

            case RCC_Customizer_UpgradeManager:

                RCC_Customizer_UpgradeManager upgradeComponent = (RCC_Customizer_UpgradeManager)component;
                engineLevel = upgradeComponent.EngineLevel;
                brakeLevel = upgradeComponent.BrakeLevel;
                handlingLevel = upgradeComponent.HandlingLevel;
                speedLevel = upgradeComponent.SpeedLevel;
                break;

            case RCC_Customizer_PaintManager:

                RCC_Customizer_PaintManager paintComponent = (RCC_Customizer_PaintManager)component;
                paint = paintComponent.color;
                break;

            case RCC_Customizer_SpoilerManager:

                RCC_Customizer_SpoilerManager spoilerComponent = (RCC_Customizer_SpoilerManager)component;
                spoiler = spoilerComponent.spoilerIndex;
                break;

            case RCC_Customizer_SirenManager:

                RCC_Customizer_SirenManager sirenComponent = (RCC_Customizer_SirenManager)component;
                siren = sirenComponent.sirenIndex;
                break;

            case RCC_Customizer_CustomizationManager:

                RCC_Customizer_CustomizationManager customizationComponent = (RCC_Customizer_CustomizationManager)component;
                customizationData = customizationComponent.customizationData;
                break;

            case RCC_Customizer_DecalManager:

                RCC_Customizer_DecalManager decalManager = (RCC_Customizer_DecalManager)component;
                decalIndexFront = decalManager.index_decalFront;
                decalIndexBack = decalManager.index_decalBack;
                decalIndexLeft = decalManager.index_decalLeft;
                decalIndexRight = decalManager.index_decalRight;
                break;

            case RCC_Customizer_NeonManager:

                RCC_Customizer_NeonManager neonManager = (RCC_Customizer_NeonManager)component;
                neonIndex = neonManager.index;
                break;

        }

    }

}
