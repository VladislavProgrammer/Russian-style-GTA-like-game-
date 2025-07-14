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

public class RCC_Prop : RCC_Core {

    public float destroyAfterCollision = 3f;

    private void OnEnable() {

        if (Settings.setLayers && Settings.PropLayer != "")
            gameObject.layer = LayerMask.NameToLayer(Settings.PropLayer);

        Rigidbody rigid = GetComponent<Rigidbody>();

        if (rigid)
            rigid.Sleep();

    }

    private void Reset() {

        if (Settings.setLayers && Settings.PropLayer != "")
            gameObject.layer = LayerMask.NameToLayer(Settings.PropLayer);

#if UNITY_2022_2_OR_NEWER
        IgnoreLayers();
#endif

    }

#if UNITY_2022_2_OR_NEWER
    private void IgnoreLayers() {

        //  Getting collider.
        Collider[] partColliders = GetComponentsInChildren<Collider>(true);

        LayerMask curLayerMask = -1;

        foreach (Collider collider in partColliders) {

            curLayerMask = collider.excludeLayers;
            curLayerMask |= (1 << LayerMask.NameToLayer(Settings.WheelColliderLayer));
            collider.excludeLayers = curLayerMask;

        }

    }
#endif

    private void OnCollisionEnter(Collision collision) {

        if (destroyAfterCollision <= 0 || collision.impulse.magnitude < 100)
            return;

        Destroy(gameObject, destroyAfterCollision);

    }

}
