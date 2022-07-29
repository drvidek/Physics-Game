using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    bool _ragdollActive;
    Rigidbody[] _rigBodies;

    // Start is called before the first frame update
    void Start()
    {
        _rigBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in _rigBodies)
        {
            //body.freezeRotation = !_ragdollActive;
            body.mass *= 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _ragdollActive = !_ragdollActive;
        }
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody body in _rigBodies)
        {
            //body.freezeRotation = !_ragdollActive;
            body.velocity = _ragdollActive ? body.velocity : Vector3.zero;
            body.constraints = _ragdollActive ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.GetComponentInParent<Move>())
        {
            _ragdollActive = true;
        }
    }
}
