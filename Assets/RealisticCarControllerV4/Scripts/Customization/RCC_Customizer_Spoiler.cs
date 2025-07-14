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
using UnityEngine;

/// <summary>
/// Upgradable spoiler.
/// </summary>
public class RCC_Customizer_Spoiler : RCC_Core {

    //  Mod applier.
    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (modApplier == null)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    public MeshRenderer bodyRenderer;       //  Renderer of the spoiler.
    public int index = -1;       //  Material index of the renderer.
    private Color color = Color.gray;     //  Default color.

    private void OnEnable() {

        //  If index is set to -1, no need to paint it.
        if (index == -1)
            return;

        //  Getting saved color of the spoiler.
        if (ModApplier.loadout.paint != new Color(1f, 1f, 1f, 0f))
            color = ModApplier.loadout.paint;

        //  Painting target material.
        if (bodyRenderer)
            bodyRenderer.materials[index].color = color;
        else
            Debug.LogError("Body renderer of this spoiler is not selected!");

    }

    /// <summary>
    /// Painting.
    /// </summary>
    /// <param name="newColor"></param>
    public void UpdatePaint(Color newColor) {

        if (index == -1)
            return;

        if (bodyRenderer)
            bodyRenderer.materials[index].color = newColor;
        else
            Debug.LogError("Body renderer of this spoiler is not selected!");

    }

    private void Reset() {

        bodyRenderer = GetComponent<MeshRenderer>();

    }

}
