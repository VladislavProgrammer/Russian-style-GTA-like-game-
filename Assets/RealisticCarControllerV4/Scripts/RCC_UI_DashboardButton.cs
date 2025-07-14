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
using UnityEngine.EventSystems;

/// <summary>
/// UI buttons used in options panel. It has an enum for all kind of buttons. 
/// </summary>
public class RCC_UI_DashboardButton : RCC_Core, IPointerClickHandler {

    private Button button;        //  Button.

    public ButtonType _buttonType = ButtonType.ABS;      //  Type of the button.
    public enum ButtonType { Start, ABS, ESP, TCS, Headlights, LeftIndicator, RightIndicator, Gear, Low, Med, High, SH, GearUp, GearDown, HazardLights, SlowMo, Record, Replay, Neutral, ChangeCamera, CustomizeOn, CustomizeOff, Restore };
    private Scrollbar gearSlider;

    public int gearDirection = 0;

    /// <summary>
    /// When clicked.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {

        OnClicked();

    }

    private void Awake() {

        button = GetComponent<Button>();

        //  If this button type is a gear selector, get scrollbar and add listener.
        if (_buttonType == ButtonType.Gear && GetComponentInChildren<Scrollbar>()) {

            gearSlider = GetComponentInChildren<Scrollbar>();
            gearSlider.onValueChanged.AddListener(delegate { ChangeGear(); });

        }

    }

    private void OnEnable() {

        //  Updating image of the button.
        UpdateImageOfButton();

    }

    /// <summary>
    /// When clicked.
    /// </summary>
    private void OnClicked() {

        switch (_buttonType) {

            case ButtonType.Low:

                QualitySettings.SetQualityLevel(1);

                break;

            case ButtonType.Med:

                QualitySettings.SetQualityLevel(3);

                break;

            case ButtonType.High:

                QualitySettings.SetQualityLevel(5);

                break;

            case ButtonType.SlowMo:

                if (Time.timeScale != .2f)
                    Time.timeScale = .2f;
                else
                    Time.timeScale = 1f;

                break;

            case ButtonType.Record:

                RCC.StartStopRecord();

                break;

            case ButtonType.Replay:

                RCC.StartStopReplay();

                break;

            case ButtonType.Neutral:

                RCC.StopRecordReplay();

                break;

            case ButtonType.ChangeCamera:

                RCC.ChangeCamera();

                break;

            case ButtonType.CustomizeOn:

                if (RCCSceneManager.activePlayerCanvas)
                    RCCSceneManager.activePlayerCanvas.SetDisplayType(RCC_UI_DashboardDisplay.DisplayType.Customization);

                break;

            case ButtonType.CustomizeOff:

                if (RCCSceneManager.activePlayerCanvas)
                    RCCSceneManager.activePlayerCanvas.SetDisplayType(RCC_UI_DashboardDisplay.DisplayType.Full);

                if (RCC_CustomizationDemo.Instance)
                    RCC_CustomizationDemo.Instance.DisableCustomization();

                break;

            case ButtonType.Restore:

                if (RCCSceneManager.activePlayerVehicle && RCCSceneManager.activePlayerVehicle.Customizer) {

                    RCCSceneManager.activePlayerVehicle.Customizer.Delete();
                    RCCSceneManager.activePlayerVehicle.Customizer.Initialize();

                    RCC_UI_Upgrade[] upgraderButtons = FindObjectsOfType<RCC_UI_Upgrade>();

                    foreach (var item in upgraderButtons)
                        item.CheckLevel();

                }

                break;


            case ButtonType.Start:

                if (RCCSceneManager.activePlayerVehicle)
                    RCCSceneManager.activePlayerVehicle.KillOrStartEngine();

                break;

            case ButtonType.ABS:

                if (RCCSceneManager.activePlayerVehicle)
                    RCCSceneManager.activePlayerVehicle.ABS = !RCCSceneManager.activePlayerVehicle.ABS;

                break;

            case ButtonType.ESP:

                if (RCCSceneManager.activePlayerVehicle)
                    RCCSceneManager.activePlayerVehicle.ESP = !RCCSceneManager.activePlayerVehicle.ESP;

                break;

            case ButtonType.TCS:

                if (RCCSceneManager.activePlayerVehicle)
                    RCCSceneManager.activePlayerVehicle.TCS = !RCCSceneManager.activePlayerVehicle.TCS;

                break;

            case ButtonType.SH:

                if (RCCSceneManager.activePlayerVehicle)
                    RCCSceneManager.activePlayerVehicle.steeringHelper = !RCCSceneManager.activePlayerVehicle.steeringHelper;

                break;

            case ButtonType.Headlights:

                if (RCCSceneManager.activePlayerVehicle) {

                    if (!RCCSceneManager.activePlayerVehicle.highBeamHeadLightsOn && RCCSceneManager.activePlayerVehicle.lowBeamHeadLightsOn) {

                        RCCSceneManager.activePlayerVehicle.highBeamHeadLightsOn = true;
                        RCCSceneManager.activePlayerVehicle.lowBeamHeadLightsOn = true;
                        break;

                    }

                    if (!RCCSceneManager.activePlayerVehicle.lowBeamHeadLightsOn)
                        RCCSceneManager.activePlayerVehicle.lowBeamHeadLightsOn = true;

                    if (RCCSceneManager.activePlayerVehicle.highBeamHeadLightsOn) {

                        RCCSceneManager.activePlayerVehicle.lowBeamHeadLightsOn = false;
                        RCCSceneManager.activePlayerVehicle.highBeamHeadLightsOn = false;

                    }

                }

                break;

            case ButtonType.LeftIndicator:

                if (RCCSceneManager.activePlayerVehicle) {

                    if (RCCSceneManager.activePlayerVehicle.indicatorsOn != RCC_CarControllerV4.IndicatorsOn.Left)
                        RCCSceneManager.activePlayerVehicle.indicatorsOn = RCC_CarControllerV4.IndicatorsOn.Left;
                    else
                        RCCSceneManager.activePlayerVehicle.indicatorsOn = RCC_CarControllerV4.IndicatorsOn.Off;

                }

                break;

            case ButtonType.RightIndicator:

                if (RCCSceneManager.activePlayerVehicle) {

                    if (RCCSceneManager.activePlayerVehicle.indicatorsOn != RCC_CarControllerV4.IndicatorsOn.Right)
                        RCCSceneManager.activePlayerVehicle.indicatorsOn = RCC_CarControllerV4.IndicatorsOn.Right;
                    else
                        RCCSceneManager.activePlayerVehicle.indicatorsOn = RCC_CarControllerV4.IndicatorsOn.Off;

                }

                break;

            case ButtonType.HazardLights:

                if (RCCSceneManager.activePlayerVehicle) {

                    if (RCCSceneManager.activePlayerVehicle.indicatorsOn != RCC_CarControllerV4.IndicatorsOn.All)
                        RCCSceneManager.activePlayerVehicle.indicatorsOn = RCC_CarControllerV4.IndicatorsOn.All;
                    else
                        RCCSceneManager.activePlayerVehicle.indicatorsOn = RCC_CarControllerV4.IndicatorsOn.Off;

                }

                break;

            case ButtonType.GearUp:

                if (RCCSceneManager.activePlayerVehicle)
                    RCCSceneManager.activePlayerVehicle.GearShiftUp();

                break;

            case ButtonType.GearDown:

                if (RCCSceneManager.activePlayerVehicle)
                    RCCSceneManager.activePlayerVehicle.GearShiftDown();

                break;

        }

        UpdateImageOfButton();

    }

    /// <summary>
    /// Checking ABS, ESP, TCS, SH, And Headlights button. This will illuminate the corresponding button.
    /// </summary>
    public void UpdateImageOfButton() {

        if (!button)
            return;

        //  If no image attached to the button, return.
        if (!button.image)
            return;

        //  If no player vehicle found, return.
        if (!RCCSceneManager.activePlayerVehicle)
            return;

        //  Illuminating the image of the button when it's on.
        switch (_buttonType) {

            case ButtonType.ABS:

                if (RCCSceneManager.activePlayerVehicle.ABS)
                    button.image.color = new Color(1, 1, 1, 1);
                else
                    button.image.color = new Color(.25f, .25f, .25f, 1);

                break;

            case ButtonType.ESP:

                if (RCCSceneManager.activePlayerVehicle.ESP)
                    button.image.color = new Color(1, 1, 1, 1);
                else
                    button.image.color = new Color(.25f, .25f, .25f, 1);

                break;

            case ButtonType.TCS:

                if (RCCSceneManager.activePlayerVehicle.TCS)
                    button.image.color = new Color(1, 1, 1, 1);
                else
                    button.image.color = new Color(.25f, .25f, .25f, 1);

                break;

            case ButtonType.SH:

                if (RCCSceneManager.activePlayerVehicle.steeringHelper)
                    button.image.color = new Color(1, 1, 1, 1);
                else
                    button.image.color = new Color(.25f, .25f, .25f, 1);

                break;

            case ButtonType.Headlights:

                if (RCCSceneManager.activePlayerVehicle.lowBeamHeadLightsOn || RCCSceneManager.activePlayerVehicle.highBeamHeadLightsOn)
                    button.image.color = new Color(1, 1, 1, 1);
                else
                    button.image.color = new Color(.25f, .25f, .25f, 1);

                break;

        }

    }

    /// <summary>
    /// Changes the gear.
    /// </summary>
    public void ChangeGear() {

        if (!RCCSceneManager.activePlayerVehicle)
            return;

        if (gearDirection == Mathf.CeilToInt(gearSlider.value * 2))
            return;

        gearDirection = Mathf.CeilToInt(gearSlider.value * 2);

        RCCSceneManager.activePlayerVehicle.semiAutomaticGear = true;

        switch (gearDirection) {

            case 0:
                RCCSceneManager.activePlayerVehicle.StartCoroutine("ChangeGear", 0);
                RCCSceneManager.activePlayerVehicle.NGear = false;
                break;

            case 1:
                RCCSceneManager.activePlayerVehicle.NGear = true;
                break;

            case 2:
                RCCSceneManager.activePlayerVehicle.StartCoroutine("ChangeGear", -1);
                RCCSceneManager.activePlayerVehicle.NGear = false;
                break;

        }

    }

    private void OnDisable() {

        //		if (!RCC_SceneManager.Instance.activePlayerVehicle)
        //			return;
        //
        //		if(_buttonType == ButtonType.Gear){
        //
        //			SceneManager.activePlayerVehicle.semiAutomaticGear = false;
        //
        //		}

    }

}
