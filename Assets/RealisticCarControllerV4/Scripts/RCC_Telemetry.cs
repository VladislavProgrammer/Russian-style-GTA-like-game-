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
/// UI telemetry for info.
/// </summary>
public class RCC_Telemetry : MonoBehaviour {

    private RCC_CarControllerV4 carController;
    public GameObject mainPanel;

    public TextMeshProUGUI RPM_WheelFL;
    public TextMeshProUGUI RPM_WheelFR;
    public TextMeshProUGUI RPM_WheelRL;
    public TextMeshProUGUI RPM_WheelRR;

    public TextMeshProUGUI Torque_WheelFL;
    public TextMeshProUGUI Torque_WheelFR;
    public TextMeshProUGUI Torque_WheelRL;
    public TextMeshProUGUI Torque_WheelRR;

    public TextMeshProUGUI Brake_WheelFL;
    public TextMeshProUGUI Brake_WheelFR;
    public TextMeshProUGUI Brake_WheelRL;
    public TextMeshProUGUI Brake_WheelRR;

    public TextMeshProUGUI Force_WheelFL;
    public TextMeshProUGUI Force_WheelFR;
    public TextMeshProUGUI Force_WheelRL;
    public TextMeshProUGUI Force_WheelRR;

    public TextMeshProUGUI Angle_WheelFL;
    public TextMeshProUGUI Angle_WheelFR;
    public TextMeshProUGUI Angle_WheelRL;
    public TextMeshProUGUI Angle_WheelRR;

    public TextMeshProUGUI Sideways_WheelFL;
    public TextMeshProUGUI Sideways_WheelFR;
    public TextMeshProUGUI Sideways_WheelRL;
    public TextMeshProUGUI Sideways_WheelRR;

    public TextMeshProUGUI Forward_WheelFL;
    public TextMeshProUGUI Forward_WheelFR;
    public TextMeshProUGUI Forward_WheelRL;
    public TextMeshProUGUI Forward_WheelRR;

    public TextMeshProUGUI ABS;
    public TextMeshProUGUI ESP;
    public TextMeshProUGUI TCS;

    public TextMeshProUGUI GroundHit_WheelFL;
    public TextMeshProUGUI GroundHit_WheelFR;
    public TextMeshProUGUI GroundHit_WheelRL;
    public TextMeshProUGUI GroundHit_WheelRR;

    public TextMeshProUGUI speed;
    public TextMeshProUGUI engineRPM;
    public TextMeshProUGUI gear;
    public TextMeshProUGUI finalTorque;
    public TextMeshProUGUI drivetrain;
    public TextMeshProUGUI angularVelocity;
    public TextMeshProUGUI controllable;

    public TextMeshProUGUI throttle;
    public TextMeshProUGUI steer;
    public TextMeshProUGUI brake;
    public TextMeshProUGUI handbrake;
    public TextMeshProUGUI clutch;

    private void Update() {

        if (mainPanel.activeSelf != RCC_Settings.Instance.useTelemetry)
            mainPanel.SetActive(RCC_Settings.Instance.useTelemetry);

        carController = RCC_SceneManager.Instance.activePlayerVehicle;

        if (!carController)
            return;

        RPM_WheelFL.text = carController.FrontLeftWheelCollider.WheelCollider.rpm.ToString("F0");
        RPM_WheelFR.text = carController.FrontRightWheelCollider.WheelCollider.rpm.ToString("F0");
        RPM_WheelRL.text = carController.RearLeftWheelCollider.WheelCollider.rpm.ToString("F0");
        RPM_WheelRR.text = carController.RearRightWheelCollider.WheelCollider.rpm.ToString("F0");

        Torque_WheelFL.text = carController.FrontLeftWheelCollider.WheelCollider.motorTorque.ToString("F0");
        Torque_WheelFR.text = carController.FrontRightWheelCollider.WheelCollider.motorTorque.ToString("F0");
        Torque_WheelRL.text = carController.RearLeftWheelCollider.WheelCollider.motorTorque.ToString("F0");
        Torque_WheelRR.text = carController.RearRightWheelCollider.WheelCollider.motorTorque.ToString("F0");

        Brake_WheelFL.text = carController.FrontLeftWheelCollider.WheelCollider.brakeTorque.ToString("F0");
        Brake_WheelFR.text = carController.FrontRightWheelCollider.WheelCollider.brakeTorque.ToString("F0");
        Brake_WheelRL.text = carController.RearLeftWheelCollider.WheelCollider.brakeTorque.ToString("F0");
        Brake_WheelRR.text = carController.RearRightWheelCollider.WheelCollider.brakeTorque.ToString("F0");

        Force_WheelFL.text = carController.FrontLeftWheelCollider.bumpForce.ToString("F0");
        Force_WheelFR.text = carController.FrontRightWheelCollider.bumpForce.ToString("F0");
        Force_WheelRL.text = carController.RearLeftWheelCollider.bumpForce.ToString("F0");
        Force_WheelRR.text = carController.RearRightWheelCollider.bumpForce.ToString("F0");

        Angle_WheelFL.text = carController.FrontLeftWheelCollider.WheelCollider.steerAngle.ToString("F0");
        Angle_WheelFR.text = carController.FrontRightWheelCollider.WheelCollider.steerAngle.ToString("F0");
        Angle_WheelRL.text = carController.RearLeftWheelCollider.WheelCollider.steerAngle.ToString("F0");
        Angle_WheelRR.text = carController.RearRightWheelCollider.WheelCollider.steerAngle.ToString("F0");

        Sideways_WheelFL.text = carController.FrontLeftWheelCollider.wheelSlipAmountSideways.ToString("F");
        Sideways_WheelFR.text = carController.FrontRightWheelCollider.wheelSlipAmountSideways.ToString("F");
        Sideways_WheelRL.text = carController.RearLeftWheelCollider.wheelSlipAmountSideways.ToString("F");
        Sideways_WheelRR.text = carController.RearRightWheelCollider.wheelSlipAmountSideways.ToString("F");

        Forward_WheelFL.text = carController.FrontLeftWheelCollider.wheelSlipAmountForward.ToString("F");
        Forward_WheelFR.text = carController.FrontRightWheelCollider.wheelSlipAmountForward.ToString("F");
        Forward_WheelRL.text = carController.RearLeftWheelCollider.wheelSlipAmountForward.ToString("F");
        Forward_WheelRR.text = carController.RearRightWheelCollider.wheelSlipAmountForward.ToString("F");

        ABS.text = carController.ABSAct ? "Engaged" : "Not Engaged";
        ESP.text = carController.ESPAct ? "Engaged" : "Not Engaged";
        TCS.text = carController.TCSAct ? "Engaged" : "Not Engaged";

        GroundHit_WheelFL.text = carController.FrontLeftWheelCollider.isGrounded ? carController.FrontLeftWheelCollider.wheelHit.collider.name : "";
        GroundHit_WheelFR.text = carController.FrontRightWheelCollider.isGrounded ? carController.FrontRightWheelCollider.wheelHit.collider.name : "";
        GroundHit_WheelRL.text = carController.RearLeftWheelCollider.isGrounded ? carController.RearLeftWheelCollider.wheelHit.collider.name : "";
        GroundHit_WheelRR.text = carController.RearRightWheelCollider.isGrounded ? carController.RearRightWheelCollider.wheelHit.collider.name : "";

        speed.text = carController.speed.ToString("F0");
        engineRPM.text = carController.engineRPM.ToString("F0");
        gear.text = carController.currentGear.ToString("F0");

        switch (carController.wheelTypeChoise) {

            case RCC_CarControllerV4.WheelType.FWD:

                drivetrain.text = "FWD";
                break;

            case RCC_CarControllerV4.WheelType.RWD:

                drivetrain.text = "RWD";
                break;

            case RCC_CarControllerV4.WheelType.AWD:

                drivetrain.text = "AWD";
                break;

        }

        angularVelocity.text = carController.Rigid.angularVelocity.ToString();
        controllable.text = carController.canControl ? "True" : "False";

        throttle.text = carController.throttleInput.ToString("F");
        steer.text = carController.steerInput.ToString("F");
        brake.text = carController.brakeInput.ToString("F");
        handbrake.text = carController.handbrakeInput.ToString("F");
        clutch.text = carController.clutchInput.ToString("F");

    }

}
