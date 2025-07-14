using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollLogic : MonoBehaviour
{
    [SerializeField]
    private List<Rigidbody> _rigidbodies;

    private Animator anim;


    private void OnEnable()
    {
        Initialize();

    }

    void Initialize()
    {
        _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        anim = GetComponent<Animator>();
        OffRagDoll();
    }

    

    public void OffRagDoll()
    {
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }


    public void OnRagDoll()
    {
        anim.enabled = false;
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }
}
