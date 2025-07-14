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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// RCC Canvas for modification.
/// </summary>
public class RCC_UI_Canvas_Customizer : RCC_Core {

    private RCC_CarControllerV4 currentPlayer;

    //UI Panels.
    [Header("Modify Panels")]
    public GameObject colorClass;
    public GameObject wheelClass;
    public GameObject modificationClass;
    public GameObject upgradesClass;
    public GameObject spoilerClass;
    public GameObject sirenClass;
    public GameObject decalsClass;
    public GameObject neonsClass;

    //UI Buttons.
    [Header("Modify Buttons")]
    public Button bodyPaintButton;
    public Button rimButton;
    public Button customizationButton;
    public Button upgradeButton;
    public Button spoilersButton;
    public Button sirensButton;
    public Button decalsButton;
    public Button neonsButton;

    private Color orgButtonColor;

    private void Awake() {

        //Getting original color of the button.
        orgButtonColor = bodyPaintButton.image.color;

    }

    private void Update() {

        currentPlayer = RCCSceneManager.activePlayerVehicle;

        // If no any player vehicle, disable all buttons and return.
        if (!currentPlayer || (currentPlayer && !currentPlayer.Customizer)) {

            if (upgradeButton)
                upgradeButton.interactable = false;

            if (spoilersButton)
                spoilersButton.interactable = false;

            if (customizationButton)
                customizationButton.interactable = false;

            if (sirensButton)
                sirensButton.interactable = false;

            if (rimButton)
                rimButton.interactable = false;

            if (bodyPaintButton)
                bodyPaintButton.interactable = false;

            return;

        }

        // Setting interactable states of the buttons depending on upgrade managers. 
        //	Ex. If spoiler manager not found, spoiler button will be disabled.
        if (upgradeButton)
            upgradeButton.interactable = currentPlayer.Customizer.UpgradeManager;

        if (spoilersButton)
            spoilersButton.interactable = currentPlayer.Customizer.SpoilerManager;

        if (customizationButton)
            customizationButton.interactable = currentPlayer.Customizer.CustomizationManager;

        if (sirensButton)
            sirensButton.interactable = currentPlayer.Customizer.SirenManager;

        if (rimButton)
            rimButton.interactable = currentPlayer.Customizer.WheelManager;

        if (bodyPaintButton)
            bodyPaintButton.interactable = currentPlayer.Customizer.PaintManager;

        if (decalsButton)
            decalsButton.interactable = currentPlayer.Customizer.DecalManager;

        if (neonsButton)
            neonsButton.interactable = currentPlayer.Customizer.NeonManager;

    }

    /// <summary>
    /// Opens up the target class panel.
    /// </summary>
    /// <param name="activeClass"></param>
    public void ChooseClass(GameObject activeClass) {

        if (colorClass)
            colorClass.SetActive(false);

        if (wheelClass)
            wheelClass.SetActive(false);

        if (modificationClass)
            modificationClass.SetActive(false);

        if (upgradesClass)
            upgradesClass.SetActive(false);

        if (spoilerClass)
            spoilerClass.SetActive(false);

        if (sirenClass)
            sirenClass.SetActive(false);

        if (decalsButton)
            decalsClass.SetActive(false);

        if (neonsClass)
            neonsClass.SetActive(false);

        if (activeClass)
            activeClass.SetActive(true);

    }

    /// <summary>
    /// Checks colors of the UI buttons. Ex. If paint class is enabled, color of the button will be green. 
    /// </summary>
    /// <param name="activeButton"></param>
    public void CheckButtonColors(Button activeButton) {

        if (bodyPaintButton)
            bodyPaintButton.image.color = orgButtonColor;

        if (rimButton)
            rimButton.image.color = orgButtonColor;

        if (customizationButton)
            customizationButton.image.color = orgButtonColor;

        if (upgradeButton)
            upgradeButton.image.color = orgButtonColor;

        if (spoilersButton)
            spoilersButton.image.color = orgButtonColor;

        if (sirensButton)
            sirensButton.image.color = orgButtonColor;

        if (decalsButton)
            decalsButton.image.color = orgButtonColor;

        if (neonsButton)
            neonsButton.image.color = orgButtonColor;

        activeButton.image.color = new Color(0f, 1f, 0f);

    }

    /// <summary>
    /// Sets auto rotation of the showrooom camera.
    /// </summary>
    /// <param name="state"></param>
    public void ToggleAutoRotation(bool state) {

        RCC_ShowroomCamera showroomCamera = FindObjectOfType<RCC_ShowroomCamera>();

        // If no any showroom camera, return.
        if (!showroomCamera)
            return;

        showroomCamera.ToggleAutoRotation(state);

    }

    /// <summary>
    /// Sets horizontal angle of the showroom camera.
    /// </summary>
    /// <param name="hor"></param>
    public void SetHorizontal(float hor) {

        RCC_ShowroomCamera showroomCamera = FindObjectOfType<RCC_ShowroomCamera>();

        // If no any showroom camera, return.
        if (!showroomCamera)
            return;

        showroomCamera.orbitX = hor;

    }
    /// <summary>
    /// Sets vertical angle of the showroom camera.
    /// </summary>
    /// <param name="ver"></param>
    public void SetVertical(float ver) {

        RCC_ShowroomCamera showroomCamera = FindObjectOfType<RCC_ShowroomCamera>();

        // If no any showroom camera, return.
        if (!showroomCamera)
            return;

        showroomCamera.orbitY = ver;

    }

    public void DisableCustomization() {

        if (RCC_CustomizationDemo.Instance)
            RCC_CustomizationDemo.Instance.DisableCustomization();

    }

}
