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
using UnityEngine.SceneManagement;

/// <summary>
/// Loads target scene.
/// </summary>
public class RCC_LevelLoader : RCC_Core {

    /// <summary>
    /// Loads target scene with string.
    /// </summary>
    /// <param name="levelName"></param>
    public void LoadLevel(string levelName) {

        SceneManager.LoadScene(levelName);

    }

}
