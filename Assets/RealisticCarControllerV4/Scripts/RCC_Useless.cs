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

public class RCC_Useless : RCC_Core {

    public enum Useless { MainController, MobileControllers, Behavior, Graphics }
    public Useless useless = Useless.MainController;

    private void Awake() {

        int type = 0;

        if (useless == Useless.Behavior)
            type = Settings.behaviorSelectedIndex;

        if (useless == Useless.MobileControllers) {

            switch (Settings.mobileController) {

                case RCC_Settings.MobileController.TouchScreen:

                    type = 0;

                    break;

                case RCC_Settings.MobileController.Gyro:

                    type = 1;

                    break;

                case RCC_Settings.MobileController.SteeringWheel:

                    type = 2;

                    break;

                case RCC_Settings.MobileController.Joystick:

                    type = 3;

                    break;

            }

        }

        if (useless == Useless.Graphics)
            type = QualitySettings.GetQualityLevel();

        GetComponent<Dropdown>().SetValueWithoutNotify(type);
        GetComponent<Dropdown>().RefreshShownValue();

    }

}
