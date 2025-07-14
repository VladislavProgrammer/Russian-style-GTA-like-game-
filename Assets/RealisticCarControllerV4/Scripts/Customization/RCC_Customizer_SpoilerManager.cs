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
/// Manager for upgradable spoilers.
/// </summary>
public class RCC_Customizer_SpoilerManager : RCC_Core {

    //  Mod applier.
    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (modApplier == null)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    /// <summary>
    /// All upgradable spoilers.
    /// </summary>
    public RCC_Customizer_Spoiler[] spoilers;

    /// <summary>
    /// Last selected spoiler index.
    /// </summary>
    [Min(-1)] public int spoilerIndex = -1;

    /// <summary>
    /// Painting the spoilers?
    /// </summary>
    public bool paintSpoilers = true;

    public void Initialize() {

        //  If spoilers is null, return.
        if (spoilers == null)
            return;

        //  If spoilers is null, return
        if (spoilers.Length < 1)
            return;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++) {

            if (spoilers[i] != null)
                spoilers[i].gameObject.SetActive(false);

        }

        //  Getting index of the loadouts spoiler.
        spoilerIndex = ModApplier.loadout.spoiler;

        //  If spoiler index is -1, return.
        if (spoilerIndex == -1)
            return;

        //  If index is not -1, enable the corresponding spoiler.
        if (spoilers[spoilerIndex] != null)
            spoilers[spoilerIndex].gameObject.SetActive(true);

        //  Getting saved color of the spoiler.
        if (ModApplier.loadout.paint != new Color(1f, 1f, 1f, 0f))
            Paint(ModApplier.loadout.paint);

    }

    public void GetAllSpoilers() {

        spoilers = GetComponentsInChildren<RCC_Customizer_Spoiler>(true);

    }

    public void DisableAll() {

        //  If spoilers is null, return.
        if (spoilers == null)
            return;

        //  If spoilers is null, return
        if (spoilers.Length < 1)
            return;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++) {

            if (spoilers[i] != null)
                spoilers[i].gameObject.SetActive(false);

        }

    }

    public void EnableAll() {

        //  If spoilers is null, return.
        if (spoilers == null)
            return;

        //  If spoilers is null, return
        if (spoilers.Length < 1)
            return;

        //  Enabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++) {

            if (spoilers[i] != null)
                spoilers[i].gameObject.SetActive(true);

        }

    }

    /// <summary>
    /// Unlocks target spoiler index and saves it.
    /// </summary>
    /// <param name="index"></param>
    public void Upgrade(int index) {

        //  If sirens is null, return.
        if (spoilers == null)
            return;

        if (spoilers.Length < 1)
            return;

        //  Index of the spoiler.
        spoilerIndex = index;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++) {

            if (spoilers[i] != null)
                spoilers[i].gameObject.SetActive(false);

        }

        //  If spoiler index is -1, return.
        if (spoilerIndex == -1)
            return;

        //  If index is not -1, enable the corresponding spoiler.
        if (spoilerIndex != -1 && spoilers[spoilerIndex] != null)
            spoilers[spoilerIndex].gameObject.SetActive(true);

        if (spoilerIndex != -1 && ModApplier.loadout.paint != new Color(1f, 1f, 1f, 0f) && spoilers[spoilerIndex].bodyRenderer != null)
            Paint(ModApplier.loadout.paint);

        //  Refreshing the loadout.
        ModApplier.Refresh(this);

        //  Saving the loadout.
        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Unlocks target spoiler index and saves it.
    /// </summary>
    /// <param name="index"></param>
    public void UpgradeWithoutSave(int index) {

        //  If sirens is null, return.
        if (spoilers == null)
            return;

        if (spoilers.Length < 1)
            return;

        //  Index of the spoiler.
        spoilerIndex = index;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++) {

            if (spoilers[i] != null)
                spoilers[i].gameObject.SetActive(false);

        }

        //  If spoiler index is -1, return.
        if (spoilerIndex == -1)
            return;

        //  If index is not -1, enable the corresponding spoiler.
        if (spoilers[spoilerIndex] != null)
            spoilers[spoilerIndex].gameObject.SetActive(true);

        if (ModApplier.loadout.paint != new Color(1f, 1f, 1f, 0f) && spoilers[spoilerIndex].bodyRenderer != null)
            Paint(ModApplier.loadout.paint);

    }

    /// <summary>
    /// Painting.
    /// </summary>
    /// <param name="newColor"></param>
    public void Paint(Color newColor) {

        //  If spoilers is null, return.
        if (spoilers == null)
            return;

        //  If spoilers is null, return.
        if (spoilers.Length < 1)
            return;

        //  If spoiler index is -1, return.
        if (spoilerIndex == -1)
            return;

        //  Painting all spoilers.
        for (int i = 0; i < spoilers.Length; i++) {

            if (spoilers[i] != null)
                spoilers[i].UpdatePaint(newColor);

        }

    }

    /// <summary>
    /// Restores the settings to default.
    /// </summary>
    public void Restore() {

        spoilerIndex = -1;

        //  If sirens is null, return.
        if (spoilers == null)
            return;

        if (spoilers.Length < 1)
            return;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++) {

            if (spoilers[i] != null)
                spoilers[i].gameObject.SetActive(false);

        }

    }

}
