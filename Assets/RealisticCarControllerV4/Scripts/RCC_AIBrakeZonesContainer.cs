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
/// Used for holding a list for brake zones, and drawing gizmos for all of them.
/// </summary>
public class RCC_AIBrakeZonesContainer : RCC_Core {

    /// <summary>
    /// Brake Zones list.
    /// </summary>
    public List<Transform> brakeZones = new List<Transform>();

    private void Awake() {

        // Changing all layers to ignore raycasts to prevent lens flare occlusion.
        foreach (var item in brakeZones)
            item.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

    }

    /// <summary>
    /// Used for drawing gizmos on Editor.
    /// </summary>
    private void OnDrawGizmos() {

        for (int i = 0; i < brakeZones.Count; i++) {

            Gizmos.matrix = brakeZones[i].transform.localToWorldMatrix;
            Gizmos.color = new Color(1f, 0f, 0f, .25f);
            Vector3 colliderBounds = brakeZones[i].GetComponent<BoxCollider>().size;

            Gizmos.DrawCube(Vector3.zero, colliderBounds);

        }

    }

}
