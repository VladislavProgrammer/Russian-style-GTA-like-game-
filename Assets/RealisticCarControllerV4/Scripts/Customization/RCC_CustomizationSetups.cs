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
/// All demo vehicles.
/// </summary>
public class RCC_CustomizationSetups : ScriptableObject {

    #region singleton
    private static RCC_CustomizationSetups instance;
    public static RCC_CustomizationSetups Instance { get { if (instance == null) instance = Resources.Load("RCC Assets/RCC_CustomizationSetups") as RCC_CustomizationSetups; return instance; } }
    #endregion

    public GameObject customization;
    public GameObject decals;
    public GameObject neons;
    public GameObject paints;
    public GameObject sirens;
    public GameObject spoilers;
    public GameObject upgrades;
    public GameObject wheels;

}
