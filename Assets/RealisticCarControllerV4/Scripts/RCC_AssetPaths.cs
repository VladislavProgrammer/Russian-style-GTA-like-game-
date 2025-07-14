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

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Paths of the projects, links, and assets.
/// </summary>
public class RCC_AssetPaths : ScriptableObject {

    #region singleton
    private static RCC_AssetPaths instance;
    public static RCC_AssetPaths Instance { get { if (instance == null) instance = Resources.Load("RCC Assets/RCC_AssetPaths") as RCC_AssetPaths; return instance; } }
    #endregion

    public Object importDefaultResources;
    public Object importDemoResources;

    public const string assetStorePath = "https://assetstore.unity.com/packages/tools/physics/realistic-car-controller-16296#content";
    public const string photonPUN2 = "https://assetstore.unity.com/packages/tools/network/pun-2-free-119922";
    public const string logitech = "https://assetstore.unity.com/packages/tools/integration/logitech-gaming-sdk-6630";
    public const string proFlares = "https://assetstore.unity.com/packages/tools/particles-effects/proflares-ultimate-lens-flares-for-unity3d-12845";

    public const string documentations = "https://www.bonecrackergames.com/bonecrackergames.com/admin/AssetStoreDocs/RCC_Documentations.rar";
    public const string YTVideos = "https://www.youtube.com/playlist?list=PLRXTqAVrLDpoW58lKf8XA1AWD6kDkoKb1";
    public const string otherAssets = "https://assetstore.unity.com/publishers/5425";

    [Space()]
    public Object addon_BCGSharedAssets;
    public Object addon_PhotonPUN2;
    public Object addon_ProFlare;
    public Object addon_Logitech;
    public Object inputActionMap;

    [Space()]
    public Object demo_AIO;
    public Object demo_City;
    public Object demo_CarSelection;
    public Object demo_CarSelectionLoadNextScene;
    public Object demo_CarSelectionLoadedScene;
    public Object demo_OverrideInputs;
    public Object demo_Customization;
    public Object demo_APIBlank;
    public Object demo_BlankMobile;
    public Object demo_Damage;
    public Object demo_MultipleTerrain;
    public Object demo_CityFPS;
    public Object demo_CityTPS;
    public Object demo_PUN2Lobby;
    public Object demo_PUN2City;

    [Space()]
    public Object importBuiltinShaders;
    public Object importURPShaders;

    [Space()]
    public Object builtinShaders;
    public Object URPShaders;

    [Space()]
    public Object[] demoContentToDelete;

#if UNITY_EDITOR
    public string GetPath(Object objectField) {

        string objectPath = AssetDatabase.GetAssetPath(objectField);
        return objectPath;

    }
#endif

}
