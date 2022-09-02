using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    bool _ragdollActive;
    Rigidbody[] _rigBodies;
    public Rigidbody[] RigBodies
    {
        get => _rigBodies;
    }
    [SerializeField] private float _massMultiplier;
    private float _impactTime;
    public bool Impacted
    {
        get => _impactTime > 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in _rigBodies)
        {
            //body.freezeRotation = !_ragdollActive;
            body.mass *= _massMultiplier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _ragdollActive = !_ragdollActive;
        }

        if (Input.GetMouseButtonDown(1) && !_ragdollActive)
            _ragdollActive = true;

        _impactTime = Mathf.MoveTowards(_impactTime, 0, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody body in _rigBodies)
        {
            //body.freezeRotation = !_ragdollActive;
            body.velocity = _ragdollActive || body.name.Contains("Arm") ? body.velocity : Vector3.zero;
            body.constraints = _ragdollActive || body.name.Contains("Arm") ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Move>())
        {
            _ragdollActive = true;
        }
    }

    public void ApplySlowdown(float rate)
    {
        foreach (Rigidbody body in _rigBodies)
        {
            body.velocity *= rate;
        }
    }

    public void ApplyImpact(float time)
    {
        _impactTime = time;
    }
}
