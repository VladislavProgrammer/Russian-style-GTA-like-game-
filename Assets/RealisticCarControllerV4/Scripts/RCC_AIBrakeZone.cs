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
/// Brake Zones are meant to be used for slowing AI vehicles. If you have a sharp turn on your scene, you can simply use one of these Brake Zones. It has a target speed. AI will adapt its speed to this target speed while in this Brake Zone. It's simple.
/// </summary>
public class RCC_AIBrakeZone : RCC_Core {

    /// <summary>
    /// Target maximum speed.
    /// </summary>
    public float targetSpeed = 50;

    /// <summary>
    /// Maximum distance.
    /// </summary>
    public float distance = 100f;

}
