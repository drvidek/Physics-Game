using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCheck : MonoBehaviour
{
    [SerializeField] float _winCheckTimerMax;
    private float _winCheckTimer;

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Hip"))
        {
            _winCheckTimer = Mathf.MoveTowards(_winCheckTimer, _winCheckTimerMax, Time.deltaTime);
            if (_winCheckTimer == _winCheckTimerMax)
            {
                Debug.Log("Win!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Hip"))
        {
            _winCheckTimer = 0;
        }
    }
}
