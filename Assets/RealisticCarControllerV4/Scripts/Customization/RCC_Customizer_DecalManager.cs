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
/// Manager for upgradable decals.
/// </summary>
public class RCC_Customizer_DecalManager : RCC_Core {

    private RCC_Customizer modApplier;
    public RCC_Customizer ModApplier {

        get {

            if (!modApplier)
                modApplier = GetComponentInParent<RCC_Customizer>(true);

            return modApplier;

        }

    }

    /// <summary>
    /// Decal painters.
    /// </summary>
    public RCC_Customizer_Decal decalFront;

    /// <summary>
    /// Decal painters.
    /// </summary>
    public RCC_Customizer_Decal decalBack;

    /// <summary>
    /// Decal painters.
    /// </summary>
    public RCC_Customizer_Decal decalLeft;

    /// <summary>
    /// Decal painters.
    /// </summary>
    public RCC_Customizer_Decal decalRight;

    /// <summary>
    /// Indexes of decals.
    /// </summary>
    [Min(-1)] public int index_decalFront = -1;

    /// <summary>
    /// Indexes of decals.
    /// </summary>
    [Min(-1)] public int index_decalBack = -1;

    /// <summary>
    /// Indexes of decals.
    /// </summary>
    [Min(-1)] public int index_decalLeft = -1;

    /// <summary>
    /// Indexes of decals.
    /// </summary>
    [Min(-1)] public int index_decalRight = -1;

    /// <summary>
    /// Decal materials.
    /// </summary>
    public Material[] decals;

    /// <summary>
    /// Empty decal material.
    /// </summary>
    public Material decal_Null;

    public void Initialize() {

        //  If empty decal is null, return.
        if (decal_Null != null) {

            decalFront.SetDecal(decal_Null);
            decalBack.SetDecal(decal_Null);
            decalLeft.SetDecal(decal_Null);
            decalRight.SetDecal(decal_Null);

        }

        //  And then getting index values from the loadout. -1 means it's empty.
        index_decalFront = ModApplier.loadout.decalIndexFront;
        index_decalBack = ModApplier.loadout.decalIndexBack;
        index_decalLeft = ModApplier.loadout.decalIndexLeft;
        index_decalRight = ModApplier.loadout.decalIndexRight;

        decalFront.gameObject.SetActive(false);
        decalBack.gameObject.SetActive(false);
        decalLeft.gameObject.SetActive(false);
        decalRight.gameObject.SetActive(false);

        //  If decals is null, return.
        if (decals == null)
            return;

        //  If decals is null, return
        if (decals.Length < 1)
            return;

        //  If index is not -1, set material of the decal by the loadout.
        if (index_decalFront != -1) {

            decalFront.gameObject.SetActive(true);
            decalFront.SetDecal(decals[index_decalFront]);

        }

        if (index_decalBack != -1) {

            decalBack.gameObject.SetActive(true);
            decalBack.SetDecal(decals[index_decalBack]);

        }

        if (index_decalLeft != -1) {

            decalLeft.gameObject.SetActive(true);
            decalLeft.SetDecal(decals[index_decalLeft]);

        }

        if (index_decalRight != -1) {

            decalRight.gameObject.SetActive(true);
            decalRight.SetDecal(decals[index_decalRight]);

        }

    }

    public void DisableAll() {

        //  If empty decal is null, return.
        if (decal_Null != null) {

            decalFront.SetDecal(decal_Null);
            decalBack.SetDecal(decal_Null);
            decalLeft.SetDecal(decal_Null);
            decalRight.SetDecal(decal_Null);

        }

        decalFront.gameObject.SetActive(false);
        decalBack.gameObject.SetActive(false);
        decalLeft.gameObject.SetActive(false);
        decalRight.gameObject.SetActive(false);

    }

    public void EnableAll() {

        //  If empty decal is null, return.
        if (decal_Null != null) {

            decalFront.SetDecal(decal_Null);
            decalBack.SetDecal(decal_Null);
            decalLeft.SetDecal(decal_Null);
            decalRight.SetDecal(decal_Null);

        }

        decalFront.gameObject.SetActive(true);
        decalBack.gameObject.SetActive(true);
        decalLeft.gameObject.SetActive(true);
        decalRight.gameObject.SetActive(true);

    }

    /// <summary>
    /// Upgrades target decal index and saves it.
    /// </summary>
    /// <param name="index"></param>
    public void Upgrade(int location, Material material) {

        //  Setting material depending on the location. 0 is front, 1 is back, 2 is left, and 3 is right.
        switch (location) {

            case 0:
                decalFront.gameObject.SetActive(true);
                decalFront.SetDecal(material);
                index_decalFront = FindMaterialIndex(material);
                break;

            case 1:
                decalBack.gameObject.SetActive(true);
                decalBack.SetDecal(material);
                index_decalBack = FindMaterialIndex(material);
                break;

            case 2:
                decalLeft.gameObject.SetActive(true);
                decalLeft.SetDecal(material);
                index_decalLeft = FindMaterialIndex(material);
                break;

            case 3:
                decalRight.gameObject.SetActive(true);
                decalRight.SetDecal(material);
                index_decalRight = FindMaterialIndex(material);
                break;

        }

        //  Refreshing the loadout.
        ModApplier.Refresh(this);

        //  Saving the loadout.
        if (ModApplier.autoSave)
            ModApplier.Save();

    }

    /// <summary>
    /// Upgrades target decal index and saves it.
    /// </summary>
    /// <param name="index"></param>
    public void UpgradeWithoutSave(int location, Material material) {

        //  Setting material depending on the location. 0 is front, 1 is back, 2 is left, and 3 is right.
        switch (location) {

            case 0:
                decalFront.gameObject.SetActive(true);
                decalFront.SetDecal(material);
                index_decalFront = FindMaterialIndex(material);
                break;

            case 1:
                decalBack.gameObject.SetActive(true);
                decalBack.SetDecal(material);
                index_decalBack = FindMaterialIndex(material);
                break;

            case 2:
                decalLeft.gameObject.SetActive(true);
                decalLeft.SetDecal(material);
                index_decalLeft = FindMaterialIndex(material);
                break;

            case 3:
                decalRight.gameObject.SetActive(true);
                decalRight.SetDecal(material);
                index_decalRight = FindMaterialIndex(material);
                break;

        }

    }

    /// <summary>
    /// Restores the settings to default.
    /// </summary>
    public void Restore() {

        //  If empty decal is null, return.
        if (decal_Null != null) {

            decalFront.SetDecal(decal_Null);
            decalBack.SetDecal(decal_Null);
            decalLeft.SetDecal(decal_Null);
            decalRight.SetDecal(decal_Null);

        }

        decalFront.gameObject.SetActive(false);
        decalBack.gameObject.SetActive(false);
        decalLeft.gameObject.SetActive(false);
        decalRight.gameObject.SetActive(false);

    }

    /// <summary>
    /// Finds index of the material.
    /// </summary>
    /// <param name="_material"></param>
    /// <returns></returns>
    private int FindMaterialIndex(Material _material) {

        int index = -1;

        if (decals != null) {

            for (int i = 0; i < decals.Length; i++) {

                if (decals[i] != null && Equals(decals[i], _material))
                    index = i;

            }

        }

        return index;

    }

}
