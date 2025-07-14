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
/// Customization loadout.
/// </summary>
[System.Serializable]
public class RCC_CustomizationData {

    public bool initialized = false;

    public float suspensionDistanceFront = .2f;
    public float suspensionDistanceRear = .2f;

    public float suspensionSpringForceFront = 55000f;
    public float suspensionSpringForceRear = 55000f;

    public float suspensionDamperFront = 3500f;
    public float suspensionDamperRear = 3500f;

    public float suspensionTargetFront = .5f;
    public float suspensionTargetRear = .5f;

    public float cambersFront = 0f;
    public float cambersRear = 0f;

    public float gearShiftingThreshold = .8f;
    public float clutchThreshold = .1f;

    public bool counterSteering = true;
    public bool steeringLimiter = true;

    public bool ABS;
    public bool ESP;
    public bool TCS;
    public bool SH;
    public bool NOS;
    public bool revLimiter;
    public bool automaticTransmission;

    public Color headlightColor = Color.white;
    public Color wheelSmokeColor = Color.white;

    public RCC_CustomizationData() { }

    public RCC_CustomizationData(bool initialized, float suspensionDistanceFront, float suspensionDistanceRear, float suspensionSpringForceFront, float suspensionSpringForceRear, float suspensionDamperFront, float suspensionDamperRear, float suspensionTargetFront, float suspensionTargetRear, float cambersFront, float cambersRear, float gearShiftingThreshold, float clutchThreshold, bool counterSteering, bool steeringLimiter, bool ABS, bool ESP, bool TCS, bool SH, bool NOS, bool revLimiter, bool automaticTransmission, Color headlightColor, Color wheelSmokeColor) {

        this.initialized = initialized;
        this.suspensionDistanceFront = suspensionDistanceFront;
        this.suspensionDistanceRear = suspensionDistanceRear;
        this.suspensionSpringForceFront = suspensionSpringForceFront;
        this.suspensionSpringForceRear = suspensionSpringForceRear;
        this.suspensionDamperFront = suspensionDamperFront;
        this.suspensionDamperRear = suspensionDamperRear;
        this.suspensionTargetFront = suspensionTargetFront;
        this.suspensionTargetRear = suspensionTargetRear;
        this.cambersFront = cambersFront;
        this.cambersRear = cambersRear;
        this.gearShiftingThreshold = gearShiftingThreshold;
        this.clutchThreshold = clutchThreshold;
        this.counterSteering = counterSteering;
        this.steeringLimiter = steeringLimiter;
        this.ABS = ABS;
        this.ESP = ESP;
        this.TCS = TCS;
        this.SH = SH;
        this.NOS = NOS;
        this.revLimiter = revLimiter;
        this.automaticTransmission = automaticTransmission;
        this.headlightColor = headlightColor;
        this.wheelSmokeColor = wheelSmokeColor;

    }

}
