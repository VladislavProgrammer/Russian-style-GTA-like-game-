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
public class RCC_Customizer_CustomizationManager : RCC_Core {

    private RCC_Customizer modApplier;
    private RCC_Customizer ModApplier {

        get {

            if (!modApplier)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    /// <summary>
    /// Current customization data.
    /// </summary>
    public RCC_CustomizationData customizationData = new RCC_CustomizationData();

    /// <summary>
    /// Default customization data.
    /// </summary>
    public RCC_CustomizationData customizationDataDefault = new RCC_CustomizationData();

    public void Initialize() {

        StartCoroutine(InitializeDelayed());

    }

    private IEnumerator InitializeDelayed() {

        yield return new WaitForEndOfFrame();

        if (customizationData == null)
            customizationData = new RCC_CustomizationData();

        if (customizationDataDefault == null)
            customizationDataDefault = new RCC_CustomizationData();

        //  Getting the default values for restoring the vehicle.
        if (!customizationDataDefault.initialized) {

            if (CarController.FrontLeftWheelCollider && CarController.FrontRightWheelCollider) {

                customizationDataDefault.cambersFront = (CarController.FrontLeftWheelCollider.camber + CarController.FrontRightWheelCollider.camber) / 2f;
                customizationDataDefault.suspensionTargetFront = (CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.targetPosition + CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.targetPosition) / 2f;
                customizationDataDefault.suspensionDistanceFront = (CarController.FrontLeftWheelCollider.WheelCollider.suspensionDistance + CarController.FrontRightWheelCollider.WheelCollider.suspensionDistance) / 2f;
                customizationDataDefault.suspensionSpringForceFront = (CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.spring + CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.spring) / 2f;
                customizationDataDefault.suspensionDamperFront = (CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.damper + CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.damper) / 2f;

            }

            if (CarController.RearLeftWheelCollider && CarController.RearRightWheelCollider) {

                customizationDataDefault.cambersRear = (CarController.RearLeftWheelCollider.camber + CarController.RearRightWheelCollider.camber) / 2f;
                customizationDataDefault.suspensionTargetRear = (CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.targetPosition + CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.targetPosition) / 2f;
                customizationDataDefault.suspensionDistanceRear = (CarController.RearLeftWheelCollider.WheelCollider.suspensionDistance + CarController.RearRightWheelCollider.WheelCollider.suspensionDistance) / 2f;
                customizationDataDefault.suspensionSpringForceRear = (CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.spring + CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.spring) / 2f;
                customizationDataDefault.suspensionDamperRear = (CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.damper + CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.damper) / 2f;

            }

            if (CarController.AllLights != null && CarController.AllLights.Length >= 1) {

                List<RCC_Light> headlights = new List<RCC_Light>();

                for (int i = 0; i < CarController.AllLights.Length; i++) {

                    if (CarController.AllLights[i] != null && CarController.AllLights[i].lightType == RCC_Light.LightType.HeadLight)
                        headlights.Add(CarController.AllLights[i]);

                }

                if (headlights.Count >= 1)
                    customizationDataDefault.headlightColor = headlights[0].LightSource.color;

            }

            for (int i = 0; i < CarController.AllWheelColliders.Length; i++) {

                if (CarController.AllWheelColliders[i] != null) {

                    //  And setting color of the particles.
                    foreach (ParticleSystem wheelParticle in CarController.AllWheelColliders[i].GetComponentsInChildren<ParticleSystem>(true)) {

                        ParticleSystem.MainModule psmain = wheelParticle.main;
                        customizationDataDefault.wheelSmokeColor = psmain.startColor.color;

                    }

                }

            }

            customizationDataDefault.initialized = true;

        }

        customizationData = ModApplier.loadout.customizationData;      //  Getting the customization data from the loadout.

        //  If data is null, return.
        if (customizationData == null)
            yield break;

        //  Getting the default values for restoring the vehicle.
        if (!customizationData.initialized) {

            if (CarController.FrontLeftWheelCollider && CarController.FrontRightWheelCollider) {

                customizationData.cambersFront = (CarController.FrontLeftWheelCollider.camber + CarController.FrontRightWheelCollider.camber) / 2f;
                customizationData.suspensionTargetFront = (CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.targetPosition + CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.targetPosition) / 2f;
                customizationData.suspensionDistanceFront = (CarController.FrontLeftWheelCollider.WheelCollider.suspensionDistance + CarController.FrontRightWheelCollider.WheelCollider.suspensionDistance) / 2f;
                customizationData.suspensionSpringForceFront = (CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.spring + CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.spring) / 2f;
                customizationData.suspensionDamperFront = (CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.damper + CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.damper) / 2f;

            }

            if (CarController.RearLeftWheelCollider && CarController.RearRightWheelCollider) {

                customizationData.cambersRear = (CarController.RearLeftWheelCollider.camber + CarController.RearRightWheelCollider.camber) / 2f;
                customizationData.suspensionTargetRear = (CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.targetPosition + CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.targetPosition) / 2f;
                customizationData.suspensionDistanceRear = (CarController.RearLeftWheelCollider.WheelCollider.suspensionDistance + CarController.RearRightWheelCollider.WheelCollider.suspensionDistance) / 2f;
                customizationData.suspensionSpringForceRear = (CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.spring + CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.spring) / 2f;
                customizationData.suspensionDamperRear = (CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.damper + CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.damper) / 2f;

            }

            if (CarController.AllLights != null && CarController.AllLights.Length >= 1) {

                List<RCC_Light> headlights = new List<RCC_Light>();

                for (int i = 0; i < CarController.AllLights.Length; i++) {

                    if (CarController.AllLights[i] != null && CarController.AllLights[i].lightType == RCC_Light.LightType.HeadLight)
                        headlights.Add(CarController.AllLights[i]);

                }

                if (headlights.Count >= 1)
                    customizationData.headlightColor = headlights[0].LightSource.color;

            }

            for (int i = 0; i < CarController.AllWheelColliders.Length; i++) {

                if (CarController.AllWheelColliders[i] != null) {

                    //  And setting color of the particles.
                    foreach (ParticleSystem wheelParticle in CarController.AllWheelColliders[i].GetComponentsInChildren<ParticleSystem>(true)) {

                        ParticleSystem.MainModule psmain = wheelParticle.main;
                        customizationData.wheelSmokeColor = psmain.startColor.color;

                    }

                }

            }

            customizationData.initialized = true;

        }

        //  Apply customization data to the vehicle.
        SetHeadlightsColor(customizationData.headlightColor);
        SetSmokeColor(customizationData.wheelSmokeColor);
        SetFrontCambers(customizationData.cambersFront);
        SetRearCambers(customizationData.cambersRear);
        SetFrontSuspensionsTargetPos(customizationData.suspensionTargetFront);
        SetRearSuspensionsTargetPos(customizationData.suspensionTargetRear);
        SetFrontSuspensionsDistances(customizationData.suspensionDistanceFront);
        SetRearSuspensionsDistances(customizationData.suspensionDistanceRear);
        SetFrontSuspensionsSpringForce(customizationData.suspensionSpringForceFront);
        SetRearSuspensionsSpringForce(customizationData.suspensionSpringForceRear);
        SetFrontSuspensionsSpringDamper(customizationData.suspensionDamperFront);
        SetRearSuspensionsSpringDamper(customizationData.suspensionDamperRear);

    }

    /// <summary>
    /// Set Smoke Color.
    /// </summary>
    public void SetSmokeColor(Color color) {

        if (CarController.AllWheelColliders == null)
            return;

        if (CarController.AllWheelColliders.Length < 1)
            return;

        for (int i = 0; i < CarController.AllWheelColliders.Length; i++) {

            if (CarController.AllWheelColliders[i] != null) {

                //  And setting color of the particles.
                foreach (ParticleSystem wheelParticle in CarController.AllWheelColliders[i].GetComponentsInChildren<ParticleSystem>(true)) {

                    ParticleSystem.MainModule psmain = wheelParticle.main;
                    color.a = .35f;
                    psmain.startColor = color;

                }

            }

        }

        customizationData.wheelSmokeColor = color;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Headlights Color.
    /// </summary>
    public void SetHeadlightsColor(Color color) {

        if (CarController.AllLights != null && CarController.AllLights.Length >= 1) {

            List<RCC_Light> headlights = new List<RCC_Light>();

            for (int i = 0; i < CarController.AllLights.Length; i++) {

                if (CarController.AllLights[i] != null && CarController.AllLights[i].lightType == RCC_Light.LightType.HeadLight)
                    headlights.Add(CarController.AllLights[i]);

            }

            if (headlights.Count >= 1) {

                for (int i = 0; i < headlights.Count; i++) {

                    if (headlights[i] != null)
                        headlights[i].LightSource.color = color;

                }

            }

        }

        customizationData.headlightColor = color;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Front Wheel Cambers.
    /// </summary>
    public void SetFrontCambers(float camberAngle) {

        if (CarController.FrontLeftWheelCollider)
            CarController.FrontLeftWheelCollider.camber = camberAngle;

        if (CarController.FrontRightWheelCollider)
            CarController.FrontRightWheelCollider.camber = camberAngle;

        customizationData.cambersFront = camberAngle;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Rear Wheel Cambers.
    /// </summary>
    public void SetRearCambers(float camberAngle) {

        if (CarController.RearLeftWheelCollider)
            CarController.RearLeftWheelCollider.camber = camberAngle;

        if (CarController.RearRightWheelCollider)
            CarController.RearRightWheelCollider.camber = camberAngle;

        customizationData.cambersRear = camberAngle;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Front Suspension targetPositions. It changes targetPosition of the front WheelColliders.
    /// </summary>
    public void SetFrontSuspensionsTargetPos(float targetPosition) {

        //  Sets target position.
        targetPosition = Mathf.Clamp01(targetPosition);

        if (CarController.FrontLeftWheelCollider) {

            JointSpring spring = CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring;
            spring.targetPosition = 1f - targetPosition;

            CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring = spring;

        }

        if (CarController.FrontRightWheelCollider) {

            JointSpring spring = CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring;
            spring.targetPosition = 1f - targetPosition;

            CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring = spring;

        }

        customizationData.suspensionTargetFront = targetPosition;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Rear Suspension targetPositions. It changes targetPosition of the rear WheelColliders.
    /// </summary>
    public void SetRearSuspensionsTargetPos(float targetPosition) {

        //  Sets target position.
        targetPosition = Mathf.Clamp01(targetPosition);

        if (CarController.RearLeftWheelCollider) {

            JointSpring spring = CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring;
            spring.targetPosition = 1f - targetPosition;

            CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring = spring;

        }

        if (CarController.RearRightWheelCollider) {

            JointSpring spring = CarController.RearRightWheelCollider.WheelCollider.suspensionSpring;
            spring.targetPosition = 1f - targetPosition;

            CarController.RearRightWheelCollider.WheelCollider.suspensionSpring = spring;

        }

        customizationData.suspensionTargetRear = targetPosition;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set All Suspension targetPositions. It changes targetPosition of the all WheelColliders.
    /// </summary>
    public void SetAllSuspensionsTargetPos(float targetPosition) {

        SetFrontSuspensionsTargetPos(targetPosition);
        SetRearSuspensionsTargetPos(targetPosition);

    }

    /// <summary>
    /// Set Front Suspension Distances.
    /// </summary>
    public void SetFrontSuspensionsDistances(float distance) {

        //  Make sure new distance is not close to 0.
        if (distance <= .01)
            distance = .05f;

        //  Setting suspension distance of front wheelcolliders.
        if (CarController.FrontLeftWheelCollider)
            CarController.FrontLeftWheelCollider.WheelCollider.suspensionDistance = distance;

        if (CarController.FrontRightWheelCollider)
            CarController.FrontRightWheelCollider.WheelCollider.suspensionDistance = distance;

        customizationData.suspensionDistanceFront = distance;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Rear Suspension Distances.
    /// </summary>
    public void SetRearSuspensionsDistances(float distance) {

        //  Make sure new distance is not close to 0.
        if (distance <= .01)
            distance = .05f;

        //  Setting suspension distance of front wheelcolliders.
        if (CarController.RearLeftWheelCollider)
            CarController.RearLeftWheelCollider.WheelCollider.suspensionDistance = distance;

        if (CarController.RearRightWheelCollider)
            CarController.RearRightWheelCollider.WheelCollider.suspensionDistance = distance;

        customizationData.suspensionDistanceRear = distance;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Front Suspension Spring Force.
    /// </summary>
    public void SetFrontSuspensionsSpringForce(float targetValue) {

        if (!CarController.FrontLeftWheelCollider || !CarController.FrontRightWheelCollider)
            return;

        JointSpring spring = CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring;
        spring.spring = targetValue;
        CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring = spring;
        CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring = spring;

        customizationData.suspensionSpringForceFront = targetValue;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Rear Suspension Spring Force.
    /// </summary>
    public void SetRearSuspensionsSpringForce(float targetValue) {

        if (!CarController.RearLeftWheelCollider || !CarController.RearRightWheelCollider)
            return;

        JointSpring spring = CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring;
        spring.spring = targetValue;
        CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring = spring;
        CarController.RearRightWheelCollider.WheelCollider.suspensionSpring = spring;

        customizationData.suspensionSpringForceRear = targetValue;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Front Suspension Spring Damper.
    /// </summary>
    public void SetFrontSuspensionsSpringDamper(float targetValue) {

        if (!CarController.FrontLeftWheelCollider || !CarController.FrontRightWheelCollider)
            return;

        JointSpring spring = CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring;
        spring.damper = targetValue;
        CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring = spring;
        CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring = spring;

        customizationData.suspensionDamperFront = targetValue;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Set Rear Suspension Spring Damper.
    /// </summary>
    public void SetRearSuspensionsSpringDamper(float targetValue) {

        if (!CarController.RearLeftWheelCollider || !CarController.RearRightWheelCollider)
            return;

        JointSpring spring = CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring;
        spring.damper = targetValue;
        CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring = spring;
        CarController.RearRightWheelCollider.WheelCollider.suspensionSpring = spring;

        customizationData.suspensionDamperRear = targetValue;

        ModApplier.Refresh(this);

        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Repair CarController.
    /// </summary>
    public void Repair() {

        if (!CarController.useDamage)
            return;

        CarController.damage.repairNow = true;

    }

    /// <summary>
    /// Restores the settings to default.
    /// </summary>
    public void Restore() {

        if (customizationDataDefault == null)
            return;

        //  Apply customization data to the vehicle.
        SetHeadlightsColor(customizationDataDefault.headlightColor);
        SetSmokeColor(customizationDataDefault.wheelSmokeColor);
        SetFrontCambers(customizationDataDefault.cambersFront);
        SetRearCambers(customizationDataDefault.cambersRear);
        SetFrontSuspensionsTargetPos(customizationDataDefault.suspensionTargetFront);
        SetRearSuspensionsTargetPos(customizationDataDefault.suspensionTargetRear);
        SetFrontSuspensionsDistances(customizationDataDefault.suspensionDistanceFront);
        SetRearSuspensionsDistances(customizationDataDefault.suspensionDistanceRear);
        SetFrontSuspensionsSpringForce(customizationDataDefault.suspensionSpringForceFront);
        SetRearSuspensionsSpringForce(customizationDataDefault.suspensionSpringForceRear);
        SetFrontSuspensionsSpringDamper(customizationDataDefault.suspensionDamperFront);
        SetRearSuspensionsSpringDamper(customizationDataDefault.suspensionDamperRear);

        customizationData = new RCC_CustomizationData();
        customizationData = ModApplier.loadout.customizationData;

        //  If data is new, get customization data from the vehicle.
        if (!customizationData.initialized) {

            if (CarController.FrontLeftWheelCollider) {

                customizationData.cambersFront = CarController.FrontLeftWheelCollider.camber;
                customizationData.suspensionTargetFront = CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.targetPosition;
                customizationData.suspensionDistanceFront = CarController.FrontLeftWheelCollider.WheelCollider.suspensionDistance;
                customizationData.suspensionSpringForceFront = CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.spring;
                customizationData.suspensionDamperFront = CarController.FrontLeftWheelCollider.WheelCollider.suspensionSpring.damper;

            }

            if (CarController.FrontRightWheelCollider) {

                customizationData.cambersFront = CarController.FrontRightWheelCollider.camber;
                customizationData.suspensionTargetFront = CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.targetPosition;
                customizationData.suspensionDistanceFront = CarController.FrontRightWheelCollider.WheelCollider.suspensionDistance;
                customizationData.suspensionSpringForceFront = CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.spring;
                customizationData.suspensionDamperFront = CarController.FrontRightWheelCollider.WheelCollider.suspensionSpring.damper;

            }

            if (CarController.RearLeftWheelCollider) {

                customizationData.cambersRear = CarController.RearLeftWheelCollider.camber;
                customizationData.suspensionTargetRear = CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.targetPosition;
                customizationData.suspensionDistanceRear = CarController.RearLeftWheelCollider.WheelCollider.suspensionDistance;
                customizationData.suspensionSpringForceRear = CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.spring;
                customizationData.suspensionDamperRear = CarController.RearLeftWheelCollider.WheelCollider.suspensionSpring.damper;

            }

            if (CarController.RearRightWheelCollider) {

                customizationData.cambersRear = CarController.RearRightWheelCollider.camber;
                customizationData.suspensionTargetRear = CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.targetPosition;
                customizationData.suspensionDistanceRear = CarController.RearRightWheelCollider.WheelCollider.suspensionDistance;
                customizationData.suspensionSpringForceRear = CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.spring;
                customizationData.suspensionDamperRear = CarController.RearRightWheelCollider.WheelCollider.suspensionSpring.damper;

            }

            if (CarController.AllLights != null && CarController.AllLights.Length >= 1) {

                List<RCC_Light> headlights = new List<RCC_Light>();

                for (int i = 0; i < CarController.AllLights.Length; i++) {

                    if (CarController.AllLights[i] != null && CarController.AllLights[i].lightType == RCC_Light.LightType.HeadLight)
                        headlights.Add(CarController.AllLights[i]);

                }

                if (headlights.Count >= 1)
                    customizationData.headlightColor = headlights[0].LightSource.color;

            }

            for (int i = 0; i < CarController.AllWheelColliders.Length; i++) {

                if (CarController.AllWheelColliders[i] != null) {

                    //  And setting color of the particles.
                    foreach (ParticleSystem wheelParticle in CarController.AllWheelColliders[i].GetComponentsInChildren<ParticleSystem>(true)) {

                        ParticleSystem.MainModule psmain = wheelParticle.main;
                        customizationData.wheelSmokeColor = psmain.startColor.color;

                    }

                }

            }

            customizationData.initialized = true;

        }

    }

}
