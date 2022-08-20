using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private HingeJoint _hinge;
    [SerializeField] private Rigidbody _stem;
    [SerializeField] private float _targetVelocity;
    [SerializeField] private float _force;
    [SerializeField] private float _dir = 1;
    bool _dirSwitch = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(_stem.velocity.x) < 0.2f && !_dirSwitch)
        {
            _dir *= -1;
            _dirSwitch = true;
        }
        else
        if ((_stem.transform.position.x > _hinge.transform.position.x && _dir == 1) || (_stem.transform.position.x < _hinge.transform.position.x && _dir == -1))
            _dirSwitch = false;

        JointMotor motor = _hinge.motor;
        motor.targetVelocity = _dir * _targetVelocity;
        motor.force = _force;
        _hinge.motor = motor;
    }
}
