//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using System.IO;

[InitializeOnLoad]
public class RCC_PipelineChecker {

    // Static constructor is called as soon as the editor is loaded or scripts are recompiled
    static RCC_PipelineChecker() {

#if !BCG_RCC
        return;
#endif

        // Subscribe to the update event for delayed execution
        EditorApplication.delayCall += DelayedInitLoad;

    }

    public static bool IsURP() {

        RenderPipelineAsset currentPipeline = GraphicsSettings.currentRenderPipeline;

#if RCC_URP
        if (currentPipeline != null && currentPipeline is UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset)
            return true;
#endif

        return false;

    }

    public static void DelayedInitLoad() {

        ManageShaders();
        ManageMaterials();

        // Subscribe to the play mode state changed event
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        if (!SessionState.GetBool("SelectMaterialsAfterCompile", false))
            return;

        SessionState.EraseBool("SelectMaterialsAfterCompile");

        Selection.objects = RCC_DemoMaterials.Instance.SelectEnvironmentShadersForURP();
        EditorUtility.DisplayDialog("Realistic Car Controller | Materials", "All demo materials have been selected now. You can now convert them from Edit --> Rendering --> Convert. After converting materials, you must convert RCC Car Body Shader from Tools --> BCG --> RCC --> URP.\n\nYou may want to reload the scene after converting the materials.", "Ok");

    }

    public static void ManageMaterials() {

        bool URP = GraphicsSettings.currentRenderPipeline != null;

#if !RCC_URP

        if (URP) {

            if (EditorUtility.DisplayDialog("Realistic Car Controller | Materials", "Seems like you're using URP, but materials of the demo content still have builtin shaders.", "Select And Convert Demo Materials", "Keep Demo Materials")) {

                SessionState.SetBool("SelectMaterialsAfterCompile", true);

            } else {

                EditorUtility.DisplayDialog("Realistic Car Controller | Materials", "All demo materials will remain the same. If you wish to convert them later, Tools --> BoneCracker Games --> RCC --> URP is the right way.", "Ok");

            }

            RCC_SetScriptingSymbol.SetEnabled("RCC_URP", true);

        } else {

            RCC_SetScriptingSymbol.SetEnabled("RCC_URP", false);

        }

#endif

    }

    public static void ManageShaders() {

        string builtInShaderPackagePath = RCC_AssetPaths.Instance.importBuiltinShaders != null ? RCC_AssetPaths.Instance.GetPath(RCC_AssetPaths.Instance.importBuiltinShaders) : "";
        string urpShaderPackagePath = RCC_AssetPaths.Instance.importURPShaders != null ? RCC_AssetPaths.Instance.GetPath(RCC_AssetPaths.Instance.importURPShaders) : "";

        string builtInShaderPath = RCC_AssetPaths.Instance.builtinShaders != null ? RCC_AssetPaths.Instance.GetPath(RCC_AssetPaths.Instance.builtinShaders) : "";
        string urpShaderPath = RCC_AssetPaths.Instance.URPShaders != null ? RCC_AssetPaths.Instance.GetPath(RCC_AssetPaths.Instance.URPShaders) : "";

        bool usingURP = IsURP();

        if (!usingURP) {

            // Built-in Render Pipeline
            if (!ShaderExists(builtInShaderPath)) {

                Debug.Log("Built-in Render Pipeline detected. Importing Built-in Shader and deleting URP Shader.");

                EditorUtility.DisplayDialog("Realistic Car Controller | Built-in Render Pipeline detected", "Built-in Render Pipeline detected. Importing Built-in Shader and deleting URP Shader. Please import the package.", "Ok");

                EditorApplication.isPlaying = false;

                if (builtInShaderPackagePath != "")
                    ImportShaderPackage(builtInShaderPackagePath);
                else
                    Debug.Log("Builtin shader package couldn't found.");

                if (ShaderExists(urpShaderPath))
                    DeleteActualShader(urpShaderPath);

            } else {

                if (ShaderExists(urpShaderPath))
                    DeleteActualShader(urpShaderPath);

            }

        } else {

            // URP or another Scriptable Render Pipeline
            if (!ShaderExists(urpShaderPath)) {

                Debug.Log("URP detected. Importing URP Shader and deleting Built-in Shader.");

                EditorUtility.DisplayDialog("Realistic Car Controller | URP detected", "Importing URP Shader and deleting Built-in Shader. Please import the package.", "Ok");

                EditorApplication.isPlaying = false;

                if (urpShaderPackagePath != "")
                    ImportShaderPackage(urpShaderPackagePath);
                else
                    Debug.Log("URP shader package couldn't found.");

                if (ShaderExists(builtInShaderPath))
                    DeleteActualShader(builtInShaderPath);

            } else {

                if (ShaderExists(builtInShaderPath))
                    DeleteActualShader(builtInShaderPath);

            }

        }

    }

    public static void OnPlayModeStateChanged(PlayModeStateChange state) {

        if (state == PlayModeStateChange.ExitingEditMode)
            ManageShaders();

    }

    // Check if the shader already exists in the project
    public static bool ShaderExists(string shaderPath) {

        return File.Exists(shaderPath);

    }

    public static void ImportShaderPackage(string packagePath) {

        if (File.Exists(packagePath)) {

            AssetDatabase.ImportPackage(packagePath, true);
            Debug.Log($"Imported shader package: {packagePath}");

        } else {

            Debug.LogError($"Shader package not found at: {packagePath}");

        }

    }

    public static void DeleteActualShader(string shaderPath) {

        string shaderDir = Path.GetDirectoryName(shaderPath);

        if (Directory.Exists(shaderDir)) {

            FileUtil.DeleteFileOrDirectory(shaderDir);
            AssetDatabase.Refresh();
            Debug.Log($"Deleted shader: {shaderDir}");

        } else {

            Debug.LogError($"Shader directory not found at: {shaderDir}");

        }

    }

}
