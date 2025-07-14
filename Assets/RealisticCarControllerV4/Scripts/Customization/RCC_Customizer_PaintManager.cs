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
/// Manager for painters.
/// </summary>
public class RCC_Customizer_PaintManager : RCC_Core {

    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (!modApplier)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    /// <summary>
    /// All painters.
    /// </summary>
    public RCC_Customizer_Paint[] paints;

    /// <summary>
    /// Current color.
    /// </summary>
    public Color color = Color.white;

    /// <summary>
    /// Default colors.
    /// </summary>
    public List<Color> defaultColors = new List<Color>();

    /// <summary>
    /// Initializes all painters.
    /// </summary>
    public void Initialize() {

        //  Return if no painters found.
        if (paints == null)
            return;

        //  Return in no painters found.
        if (paints.Length < 1)
            return;

        //  Loadout color.
        color = ModApplier.loadout.paint;

        //  Getting last saved color for this vehicle.
        if (color != new Color(1f, 1f, 1f, 0f))
            Paint(color);
        else
            Restore();

        defaultColors.Clear();

        //  Getting default colors for restoring.
        for (int i = 0; i < paints.Length; i++) {

            if (paints[i] != null && paints[i].paintMaterial)
                defaultColors.Add(paints[i].paintMaterial.GetColor(paints[i].id));

        }

    }

    public void GetAllPainters() {

        paints = GetComponentsInChildren<RCC_Customizer_Paint>(true);

    }

    /// <summary>
    /// Runs all painters with the target color.
    /// </summary>
    /// <param name="newColor"></param>
    public void Paint(Color newColor) {

        //  Return if no painters found.
        if (paints == null)
            return;

        //  Return if no painters found.
        if (paints.Length < 1)
            return;

        //  Setting color.
        color = newColor;

        //  Painting.
        for (int i = 0; i < paints.Length; i++) {

            if (paints[i] != null)
                paints[i].UpdatePaint(color);

        }

        //  Painting spoilers.
        if (ModApplier.SpoilerManager != null && ModApplier.loadout.paint != new Color(1f, 1f, 1f, 0f))
            ModApplier.SpoilerManager.Paint(ModApplier.loadout.paint);

        //  Refreshing the loadout.
        ModApplier.Refresh(this);

        //  Saving the loadout.
        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Runs all painters with the target color.
    /// </summary>
    /// <param name="newColor"></param>
    public void PaintWithoutSave(Color newColor) {

        //  Return if no painters found.
        if (paints == null)
            return;

        //  Return if no painters found.
        if (paints.Length < 1)
            return;

        //  Setting color.
        color = newColor;

        //  Painting.
        for (int i = 0; i < paints.Length; i++) {

            if (paints[i] != null)
                paints[i].UpdatePaint(color);

        }

        //  Painting spoilers.
        if (ModApplier.SpoilerManager != null && ModApplier.loadout.paint != new Color(1f, 1f, 1f, 0f))
            ModApplier.SpoilerManager.Paint(ModApplier.loadout.paint);

    }

    private void Reset() {

        paints = GetComponentsInChildren<RCC_Customizer_Paint>(true);

        if (paints == null || (paints != null && paints.Length == 0)) {

            paints = new RCC_Customizer_Paint[1];
            GameObject newPaint = new GameObject("Paint_1");
            newPaint.transform.SetParent(transform);
            newPaint.transform.localPosition = Vector3.zero;
            newPaint.transform.localRotation = Quaternion.identity;
            paints[0] = newPaint.AddComponent<RCC_Customizer_Paint>();

        }

    }

    /// <summary>
    /// Restores the settings to default.
    /// </summary>
    public void Restore() {

        //  Loadout color.
        color = ModApplier.loadout.paint;

        if (defaultColors != null) {

            if (defaultColors.Count >= 1) {

                for (int i = 0; i < defaultColors.Count; i++) {

                    if (paints[i] != null)
                        paints[i].UpdatePaint(defaultColors[i]);

                }

            }

        }

    }

}
