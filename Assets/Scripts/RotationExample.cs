using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Quaternion example = Quaternion.identity;

        Vector3 direction = new Vector3();

        //multiply a Quat by a Vector to apply the rotation to the vector
        //you must place the Quaternion before the Vector3
        Vector3 newDirection = example * direction;
    }

}
