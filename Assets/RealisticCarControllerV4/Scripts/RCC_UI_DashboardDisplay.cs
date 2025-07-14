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
using UnityEngine.UI;
using TMPro;

#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif

/// <summary>
/// Handles RCC Canvas dashboard elements.
/// </summary>
[RequireComponent(typeof(RCC_DashboardInputs))]
public class RCC_UI_DashboardDisplay : RCC_Core {

    //  Inputs of the dashboard elements.
    private RCC_DashboardInputs inputs;
    private RCC_DashboardInputs Inputs {

        get {

            if (inputs == null)
                inputs = GetComponent<RCC_DashboardInputs>();

            return inputs;

        }

    }

    public DisplayType displayType = DisplayType.Full;     //  Current display type.
    public enum DisplayType { Full, Customization, TopButtonsOnly, Off }

    public RCC_CarControllerV4 vehicle;
    public bool findPlayerVehicleAuto = true;

#if PHOTON_UNITY_NETWORKING && RCC_PHOTON
    public bool usePhotonWithThisCanvas = false;
#endif

    //  Buttons, texts, images, and dropdown menus.
    [Header("Panels")]
    public GameObject controllerButtons;
    public GameObject gauges;
    public GameObject customizationMenu;
    public GameObject optionsMenu;

    [Header("Buttons")]
    public GameObject spawn;
    public GameObject spawn_Photon;

    [Header("Texts")]
    public TextMeshProUGUI RPMLabel;
    public TextMeshProUGUI KMHLabel;
    public TextMeshProUGUI GearLabel;
    public TextMeshProUGUI recordingLabel;

    [Header("Images")]
    public Image ABS;
    public Image ESP;
    public Image Park;
    public Image Headlights;
    public Image leftIndicator;
    public Image rightIndicator;
    public Image heatIndicator;
    public Image fuelIndicator;
    public Image rpmIndicator;

    [Header("Colors")]
    public Color color_On = Color.yellow;
    public Color color_Off = Color.white;

    [Header("Dropdowns")]
    public Dropdown mobileControllersDropdown;
    public Dropdown carSelectionDropdown;
    public Dropdown carSelectionDropdown_Photon;

    private void Awake() {

#if PHOTON_UNITY_NETWORKING && RCC_PHOTON

        if (usePhotonWithThisCanvas) {

            if (carSelectionDropdown)
                carSelectionDropdown.gameObject.SetActive(false);

            if (carSelectionDropdown_Photon)
                carSelectionDropdown_Photon.gameObject.SetActive(true);

            if (spawn)
                spawn.SetActive(false);

            if (spawn_Photon)
                spawn_Photon.SetActive(true);

            StartCoroutine(EnableOptionsMenuOnPhotonConnect());

        } else {

            if (carSelectionDropdown)
                carSelectionDropdown.gameObject.SetActive(true);

            if (carSelectionDropdown_Photon)
                carSelectionDropdown_Photon.gameObject.SetActive(false);

            if (spawn)
                spawn.SetActive(true);

            if (spawn_Photon)
                spawn_Photon.SetActive(false);

        }

#else

        if (carSelectionDropdown)
            carSelectionDropdown.gameObject.SetActive(true);

#endif

    }
#if PHOTON_UNITY_NETWORKING && RCC_PHOTON

    private IEnumerator EnableOptionsMenuOnPhotonConnect() {

        if (optionsMenu)
            optionsMenu.SetActive(false);

        yield return new WaitUntil(() => PhotonNetwork.InRoom);

        if (optionsMenu)
            optionsMenu.SetActive(true);

    }

#endif

    private void Update() {

        if (mobileControllersDropdown)
            mobileControllersDropdown.interactable = Settings.mobileControllerEnabled;

        //  Enabling / disabling corresponding elements related to choosen display type.
        switch (displayType) {

            case DisplayType.Full:

                if (controllerButtons && !controllerButtons.activeSelf)
                    controllerButtons.SetActive(true);

                if (gauges && !gauges.activeSelf)
                    gauges.SetActive(true);

                if (customizationMenu && customizationMenu.activeSelf)
                    customizationMenu.SetActive(false);

                break;

            case DisplayType.Customization:

                if (controllerButtons && controllerButtons.activeSelf)
                    controllerButtons.SetActive(false);

                if (gauges && gauges.activeSelf)
                    gauges.SetActive(false);

                if (customizationMenu && !customizationMenu.activeSelf)
                    customizationMenu.SetActive(true);

                break;

            case DisplayType.TopButtonsOnly:

                if (controllerButtons.activeSelf)
                    controllerButtons.SetActive(false);

                if (gauges.activeSelf)
                    gauges.SetActive(false);

                if (customizationMenu.activeSelf)
                    customizationMenu.SetActive(false);

                break;

            case DisplayType.Off:

                if (controllerButtons && controllerButtons.activeSelf)
                    controllerButtons.SetActive(false);

                if (gauges && gauges.activeSelf)
                    gauges.SetActive(false);

                if (customizationMenu && customizationMenu.activeSelf)
                    customizationMenu.SetActive(false);

                break;

        }

    }

    private void LateUpdate() {

        //  If inputs are not enabled yet, disable it and return.
        if (!Inputs.enabled)
            return;

        if (findPlayerVehicleAuto && RCCSceneManager.activePlayerVehicle)
            vehicle = RCCSceneManager.activePlayerVehicle;

        if (!vehicle)
            return;

        if (RPMLabel)
            RPMLabel.text = Inputs.RPM.ToString("0");

        if (KMHLabel) {

            if (Settings.units == RCC_Settings.Units.KMH)
                KMHLabel.text = Inputs.KMH.ToString("0") + "\nKMH";
            else
                KMHLabel.text = (Inputs.KMH * 0.62f).ToString("0") + "\nMPH";

        }

        if (GearLabel) {

            if (!Inputs.NGear && !Inputs.changingGear)
                GearLabel.text = Inputs.direction == 1 ? (Inputs.Gear + 1).ToString("0") : "R";
            else
                GearLabel.text = "N";

        }

        if (recordingLabel) {

            switch (RCCSceneManager.recordMode) {

                case RCC_SceneManager.RecordMode.Neutral:

                    if (recordingLabel.gameObject.activeSelf)
                        recordingLabel.gameObject.SetActive(false);

                    recordingLabel.text = "";

                    break;

                case RCC_SceneManager.RecordMode.Play:

                    if (!recordingLabel.gameObject.activeSelf)
                        recordingLabel.gameObject.SetActive(true);

                    recordingLabel.text = "Playing";
                    recordingLabel.color = Color.green;

                    break;

                case RCC_SceneManager.RecordMode.Record:

                    if (!recordingLabel.gameObject.activeSelf)
                        recordingLabel.gameObject.SetActive(true);

                    recordingLabel.text = "Recording";
                    recordingLabel.color = Color.red;

                    break;

            }

        }

        if (ABS)
            ABS.color = Inputs.ABS == true ? color_On : color_Off;

        if (ESP)
            ESP.color = Inputs.ESP == true ? color_On : color_Off;

        if (Park)
            Park.color = Inputs.Park == true ? Color.red : color_Off;

        if (Headlights)
            Headlights.color = Inputs.Headlights == true ? Color.green : color_Off;

        if (heatIndicator)
            heatIndicator.color = vehicle.engineHeat >= 100f ? Color.red : new Color(.1f, 0f, 0f);

        if (fuelIndicator)
            fuelIndicator.color = vehicle.fuelTank < 10f ? Color.red : new Color(.1f, 0f, 0f);

        if (rpmIndicator)
            rpmIndicator.color = vehicle.engineRPM >= vehicle.maxEngineRPM - 500f ? Color.red : new Color(.1f, 0f, 0f);

        if (leftIndicator && rightIndicator) {

            switch (Inputs.indicators) {

                case RCC_CarControllerV4.IndicatorsOn.Left:
                    leftIndicator.color = new Color(1f, .5f, 0f);
                    rightIndicator.color = new Color(.5f, .25f, 0f);
                    break;
                case RCC_CarControllerV4.IndicatorsOn.Right:
                    leftIndicator.color = new Color(.5f, .25f, 0f);
                    rightIndicator.color = new Color(1f, .5f, 0f);
                    break;
                case RCC_CarControllerV4.IndicatorsOn.All:
                    leftIndicator.color = new Color(1f, .5f, 0f);
                    rightIndicator.color = new Color(1f, .5f, 0f);
                    break;
                case RCC_CarControllerV4.IndicatorsOn.Off:
                    leftIndicator.color = new Color(.5f, .25f, 0f);
                    rightIndicator.color = new Color(.5f, .25f, 0f);
                    break;

            }

        }

    }

    public void SetDisplayType(DisplayType _displayType) {

        displayType = _displayType;

    }

}
