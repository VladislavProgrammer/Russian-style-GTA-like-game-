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
using UnityEngine.Rendering;

/// <summary>
/// All demo materials.
/// </summary>
public class RCC_DemoMaterials : ScriptableObject {

    #region singleton
    private static RCC_DemoMaterials instance;
    public static RCC_DemoMaterials Instance { get { if (instance == null) instance = Resources.Load("RCC Assets/RCC_DemoMaterials") as RCC_DemoMaterials; return instance; } }
    #endregion

    public Material[] demoMaterials;
    public Material[] demoVehicleMaterials;

    // Shader names for Built-in and URP
    public string builtInShaderName = "RCC Car Body Shader";
    public string urpShaderName = "RCC Car Body Shader URP";

    /// <summary>
    /// Update shaders of materials in the ScriptableObject based on the current render pipeline.
    /// </summary>
    public void ConvertCarBodyShadersToURP() {

        if (demoVehicleMaterials == null)
            return;

        Shader shaderToAssign;

        if (!IsURP())
            shaderToAssign = Shader.Find(builtInShaderName); // Built-in shader
        else
            shaderToAssign = Shader.Find(urpShaderName); // URP shader


        if (shaderToAssign != null) {

            foreach (var material in demoVehicleMaterials) {

                if (material != null) {

                    material.shader = shaderToAssign;
                    Debug.Log($"Assigned {shaderToAssign.name} shader to {material.name}");

                }

            }

        } else {

            Debug.LogError("Failed to find the appropriate shader.");

        }

    }

    /// <summary>
    /// Update shaders of materials in the ScriptableObject based on the current render pipeline.
    /// </summary>
    public Material[] SelectEnvironmentShadersForURP() {

        if (demoMaterials == null)
            return null;

        return demoMaterials;

    }

    /// <summary>
    /// Cleans empty elements in the demo materials array.
    /// </summary>
    public void CleanEmptyMaterials() {

        if (demoMaterials != null) {

            List<Material> materialsList = new List<Material>();

            for (int i = 0; i < demoMaterials.Length; i++) {

                if (demoMaterials[i] != null)
                    materialsList.Add(demoMaterials[i]);

            }

            demoMaterials = materialsList.ToArray();

        }

        if (demoVehicleMaterials != null) {

            List<Material> materialsVehicleList = new List<Material>();

            for (int i = 0; i < demoVehicleMaterials.Length; i++) {

                if (demoVehicleMaterials[i] != null)
                    materialsVehicleList.Add(demoVehicleMaterials[i]);

            }

            demoVehicleMaterials = materialsVehicleList.ToArray();

        }

    }

    private bool IsURP() {

        RenderPipelineAsset currentPipeline = GraphicsSettings.currentRenderPipeline;

#if RCC_URP
        if (currentPipeline != null && currentPipeline is UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset)
            return true;
#endif

        return false;

    }

}
