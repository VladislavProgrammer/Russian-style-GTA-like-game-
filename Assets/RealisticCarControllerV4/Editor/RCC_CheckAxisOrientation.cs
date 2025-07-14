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

public class RCC_CheckAxisOrientation {

    public static bool IsAxisOrientationCorrect(GameObject vehicle) {

        Bounds combinedBounds = new Bounds(Vector3.zero, Vector3.one);
        MeshFilter[] meshFilters = vehicle.GetComponentsInChildren<MeshFilter>();

        if (meshFilters.Length == 0) {

            Debug.LogWarning("No MeshFilters found in the car model.");
            return false;

        }

        // Aggregate the bounds of all MeshFilters
        foreach (MeshFilter meshFilter in meshFilters) {

            if (meshFilter.sharedMesh != null)
                combinedBounds.Encapsulate(meshFilter.sharedMesh.bounds);

        }

        // Compare the dimensions
        float width = combinedBounds.size.x;
        float height = combinedBounds.size.y;
        float length = combinedBounds.size.z;

        if (width <= length && length >= height) {

            return true;

        } else {

            return false;

        }

    }

}
