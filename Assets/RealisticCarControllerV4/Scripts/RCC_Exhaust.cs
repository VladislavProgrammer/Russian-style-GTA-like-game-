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
using UnityEngine.Rendering;

/// <summary>
/// Exhaust based on Particle System. Based on vehicle controller's throttle.
/// </summary>
public class RCC_Exhaust : RCC_Core {

    private ParticleSystem particle;        //  Smoke particles.
    private ParticleSystem.EmissionModule emission;     //  Smoke emission.
    public ParticleSystem flame;        //  Flame particles.
    private ParticleSystem.EmissionModule subEmission;      //  Flame emission.

    private Light flameLight;       //  Flame light.
    private LensFlare lensFlare;        // Lensflare of the flame light.
#if RCC_URP
    public LensFlareComponentSRP lensFlareURP;     //   URP lensflare if used.
#endif

    public float flareBrightness = 1f;      //  Flare brightness.
    private float finalFlareBrightness;     //  Calculated flare brigtness.

    public float flameTime = 0f;        //  Flame time.
    private AudioSource flameSource;        //  Flame audio source.

    public Color flameColor = Color.red;        //  Flame color.
    public Color boostFlameColor = Color.blue;      //  Boost / Nos flame color.

    public bool previewFlames = false;

    public float minEmission = 5f;      //  Emission limits
    public float maxEmission = 20f;

    public float minSize = 1f;      //  Size limits.
    public float maxSize = 4f;

    public float minSpeed = .1f;        //  Speed limits.
    public float maxSpeed = 1f;

    private void Start() {

        //  If don't use any particles enabled, destroy it.
        if (Settings.dontUseAnyParticleEffects) {

            Destroy(gameObject);
            return;

        }

        // Getting components.
        particle = GetComponent<ParticleSystem>();
        emission = particle.emission;

        //  If flame exists...
        if (flame) {

            //  Getting emission of the flame, light, and creating audio source.
            subEmission = flame.emission;
            flameLight = flame.GetComponentInChildren<Light>();
            flameSource = NewAudioSource(Settings.audioMixer, gameObject, "Exhaust Flame AudioSource", 10f, 25f, .5f, Settings.exhaustFlameClips[0], false, false, false);

            //  If flame light exists, set render mode of the light depending of the option in RCC Settings.
            if (flameLight)
                flameLight.renderMode = Settings.useOtherLightsAsVertexLights ? LightRenderMode.ForceVertex : LightRenderMode.ForcePixel;

        }

        //  Getting lensflare.
        lensFlare = GetComponentInChildren<LensFlare>();

#if RCC_URP
        lensFlareURP = GetComponentInChildren<LensFlareComponentSRP>();
#endif

        if (flameLight) {

            if (flameLight.flare != null)
                flameLight.flare = null;

        }

    }

    private void Update() {

        //  If no car controller found, or particle, return.
        if (!CarController || !particle)
            return;

        Smoke();
        Flame();

#if RCC_URP
        //  If lensflare found, use them.
        if (lensFlare || lensFlareURP)
            LensFlare();
#else
        //  If lensflare found, use them.
        if (lensFlare)
            LensFlare();
#endif

    }

    /// <summary>
    /// Smoke particles.
    /// </summary>
    private void Smoke() {

        //  If engine is running, set speed, size, and emission rates of the smoke particles.
        if (CarController.engineRunning) {

            ParticleSystem.MainModule main = particle.main;

            if (CarController.speed < 20) {

                if (!emission.enabled)
                    emission.enabled = true;

                if (CarController.throttleInput > .35f) {

                    emission.rateOverTime = maxEmission;
                    main.startSpeed = maxSpeed;
                    main.startSize = maxSize;

                } else {

                    emission.rateOverTime = minEmission;
                    main.startSpeed = minSpeed;
                    main.startSize = minSize;

                }

            } else {

                if (emission.enabled)
                    emission.enabled = false;

            }

        } else {

            if (emission.enabled)
                emission.enabled = false;

        }

    }

    /// <summary>
    /// Flame particles with light effects.
    /// </summary>
    private void Flame() {

        //  If engine is running, set color of the flame, create audio source.
        if (CarController.engineRunning) {

            ParticleSystem.MainModule main = flame.main;

            if (CarController.throttleInput >= .25f)
                flameTime = 0f;

            if (((CarController.useExhaustFlame && CarController.engineRPM >= 5000 && CarController.engineRPM <= 5500 && CarController.throttleInput <= .25f && flameTime <= .5f) || CarController.boostInput >= .75f) || previewFlames) {

                flameTime += Time.deltaTime;
                subEmission.enabled = true;

                if (flameLight)
                    flameLight.intensity = flameSource.pitch * 3f * Random.Range(.25f, 1f);

                if (CarController.boostInput >= .75f && flame) {

                    main.startColor = boostFlameColor;

                    if (flameLight)
                        flameLight.color = main.startColor.color;

                } else {

                    main.startColor = flameColor;

                    if (flameLight)
                        flameLight.color = main.startColor.color;

                }

                if (!flameSource.isPlaying) {

                    flameSource.clip = Settings.exhaustFlameClips[Random.Range(0, Settings.exhaustFlameClips.Length)];
                    flameSource.Play();

                }

            } else {

                subEmission.enabled = false;

                if (flameLight)
                    flameLight.intensity = 0f;
                if (flameSource.isPlaying)
                    flameSource.Stop();

            }

        } else {

            if (emission.enabled)
                emission.enabled = false;

            subEmission.enabled = false;

            if (flameLight)
                flameLight.intensity = 0f;
            if (flameSource.isPlaying)
                flameSource.Stop();

        }

    }

    /// <summary>
    /// Lensflare calculation.
    /// </summary>
    private void LensFlare() {

        //  If there is no camera, return.
        if (!Camera.main)
            return;

        float distanceTocam = Vector3.Distance(transform.position, Camera.main.transform.position);
        float angle = Vector3.Angle(transform.forward, Camera.main.transform.position - transform.position);

        if (angle != 0)
            finalFlareBrightness = flareBrightness * (4 / distanceTocam) * ((100f - (1.11f * angle)) / 100f) / 2f;

        if (flameLight) {

            if (lensFlare) {

                lensFlare.brightness = finalFlareBrightness * flameLight.intensity;
                lensFlare.color = flameLight.color;

            }

#if RCC_URP
            if (lensFlareURP)
                lensFlareURP.intensity = finalFlareBrightness * flameLight.intensity;
#endif

        }

    }

}
