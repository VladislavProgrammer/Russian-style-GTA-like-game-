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
/// Trailer attacher point. Trailer will be attached when two trigger colliders triggers each other.
/// </summary>
public class RCC_TrailerAttachPoint : RCC_Core {

    private void OnTriggerEnter(Collider col) {

        //  Getting other attacher.
        RCC_TrailerAttachPoint otherAttacher = col.gameObject.GetComponent<RCC_TrailerAttachPoint>();

        //  If no attacher found, return.
        if (!otherAttacher)
            return;

        //  Other vehicle.
        RCC_CarControllerV4 otherVehicle = otherAttacher.gameObject.GetComponentInParent<RCC_CarControllerV4>();

        //  If no vehicle found, return.
        if (!otherVehicle)
            return;

        //  Attach the trailer.
        GetComponentInParent<ConfigurableJoint>().transform.SendMessage("AttachTrailer", otherVehicle, SendMessageOptions.DontRequireReceiver);

    }

    private void Reset() {

        if (GetComponent<BoxCollider>() == null)
            gameObject.AddComponent<BoxCollider>().isTrigger = true;

    }

}
