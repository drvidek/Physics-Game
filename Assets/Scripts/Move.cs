using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float _leftSpd;
    [SerializeField] private float _rightSpd, _currentSpd, _turnDir;
    [SerializeField] private float _turnSpd = 30f;
    [SerializeField] private float _spdMax = 10f, _accel = 5f, _fric = 3f;
    [SerializeField] HingeJoint[] _leftWheels;
    [SerializeField] HingeJoint[] _rightWheels;

    private void Update()
    {
        float _horDir = Input.GetAxis("Horizontal");
        float _verDir = Input.GetAxis("Vertical");

        if (_verDir != 0)
            _currentSpd = Mathf.Clamp(_currentSpd + (_accel * Time.deltaTime * _verDir), -_spdMax, _spdMax);
        else
            _currentSpd = Mathf.Clamp(_currentSpd - _fric * Time.deltaTime, 0f, _spdMax);

        foreach (HingeJoint joint in _leftWheels)
        {
            JointMotor _motor = joint.motor;
            _motor.force = _currentSpd * (_horDir >= 0 ? 1 : 0);
            joint.motor = _motor;
        }

        foreach (HingeJoint joint in _rightWheels)
        {
            JointMotor _motor = joint.motor;
            _motor.force = _currentSpd * (_horDir <= 0 ? 1 : 0);
            joint.motor = _motor;
        }
    }

}
