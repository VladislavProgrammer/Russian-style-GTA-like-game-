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
using System.Collections.Generic;
using TMPro;

/// <summary>
/// UI upgrade button.
/// </summary>
public class RCC_UI_CustomizationSlider : RCC_Core {

    /// <summary>
    /// Target customization class for this slider.
    /// </summary>
    public CustomizationClass customizationClass = CustomizationClass.Cambers_Front;
    public enum CustomizationClass { Cambers_Front, Cambers_Rear, SuspensionSprings_Front, SuspensionSprings_Rear, SuspensionDistances_Front, SuspensionDistances_Rear, SuspensionDampers_Front, SuspensionDampers_Rear, SuspensionTargets_Front, SuspensionTargets_Rear }

    /// <summary>
    /// Slider component.
    /// </summary>
    private Slider slider;

    /// <summary>
    /// Text value of the slider.
    /// </summary>
    public TextMeshProUGUI sliderValue;

    private void Awake() {

        slider = GetComponent<Slider>();

    }

    private void OnEnable() {

        //  Finding the player vehicle.
        RCC_CarControllerV4 playerVehicle = RCCSceneManager.activePlayerVehicle;

        //  If no player vehicle found, return.
        if (!playerVehicle)
            return;

        //  If player vehicle doesn't have the customizer component, return.
        if (!playerVehicle.Customizer)
            return;

        RCC_CustomizationData customizationData = playerVehicle.Customizer.GetLoadout().customizationData;

        switch (customizationClass) {

            case CustomizationClass.Cambers_Front:
                slider.SetValueWithoutNotify(customizationData.cambersFront);
                break;

            case CustomizationClass.Cambers_Rear:
                slider.SetValueWithoutNotify(customizationData.cambersRear);
                break;

            case CustomizationClass.SuspensionSprings_Front:
                slider.SetValueWithoutNotify(customizationData.suspensionSpringForceFront);
                break;

            case CustomizationClass.SuspensionSprings_Rear:
                slider.SetValueWithoutNotify(customizationData.suspensionSpringForceRear);
                break;

            case CustomizationClass.SuspensionDistances_Front:
                slider.SetValueWithoutNotify(customizationData.suspensionDistanceFront);
                break;

            case CustomizationClass.SuspensionDistances_Rear:
                slider.SetValueWithoutNotify(customizationData.suspensionDistanceRear);
                break;

            case CustomizationClass.SuspensionDampers_Front:
                slider.SetValueWithoutNotify(customizationData.suspensionDamperFront);
                break;

            case CustomizationClass.SuspensionDampers_Rear:
                slider.SetValueWithoutNotify(customizationData.suspensionDamperRear);
                break;

            case CustomizationClass.SuspensionTargets_Front:
                slider.SetValueWithoutNotify(customizationData.suspensionTargetFront);
                break;

            case CustomizationClass.SuspensionTargets_Rear:
                slider.SetValueWithoutNotify(customizationData.suspensionTargetRear);
                break;

        }

        if (sliderValue)
            sliderValue.text = slider.value.ToString("F0");

    }

    public void OnSlider() {

        //  Finding the player vehicle.
        RCC_CarControllerV4 playerVehicle = RCCSceneManager.activePlayerVehicle;

        //  If no player vehicle found, return.
        if (!playerVehicle)
            return;

        //  If player vehicle doesn't have the customizer component, return.
        if (!playerVehicle.Customizer)
            return;

        if (!playerVehicle.Customizer.CustomizationManager)
            return;

        switch (customizationClass) {

            case CustomizationClass.Cambers_Front:
                playerVehicle.Customizer.CustomizationManager.SetFrontCambers(slider.value);
                break;

            case CustomizationClass.Cambers_Rear:
                playerVehicle.Customizer.CustomizationManager.SetRearCambers(slider.value);
                break;

            case CustomizationClass.SuspensionSprings_Front:
                playerVehicle.Customizer.CustomizationManager.SetFrontSuspensionsSpringForce(slider.value);
                break;

            case CustomizationClass.SuspensionSprings_Rear:
                playerVehicle.Customizer.CustomizationManager.SetRearSuspensionsSpringForce(slider.value);
                break;

            case CustomizationClass.SuspensionDistances_Front:
                playerVehicle.Customizer.CustomizationManager.SetFrontSuspensionsDistances(slider.value);
                break;

            case CustomizationClass.SuspensionDistances_Rear:
                playerVehicle.Customizer.CustomizationManager.SetRearSuspensionsDistances(slider.value);
                break;

            case CustomizationClass.SuspensionDampers_Front:
                playerVehicle.Customizer.CustomizationManager.SetFrontSuspensionsSpringDamper(slider.value);
                break;

            case CustomizationClass.SuspensionDampers_Rear:
                playerVehicle.Customizer.CustomizationManager.SetRearSuspensionsSpringDamper(slider.value);
                break;

            case CustomizationClass.SuspensionTargets_Front:
                playerVehicle.Customizer.CustomizationManager.SetFrontSuspensionsTargetPos(slider.value);
                break;

            case CustomizationClass.SuspensionTargets_Rear:
                playerVehicle.Customizer.CustomizationManager.SetRearSuspensionsTargetPos(slider.value);
                break;

        }

        if (sliderValue)
            sliderValue.text = slider.value.ToString("F0");

    }

}
