using UnityEngine;
using UnityEngine.UI;


public class VictoryCheck : MonoBehaviour
{
    [SerializeField] float _winCheckTimerMax;
    private float _winCheckTimer;
    [SerializeField] Image _winCheckImage;
    bool _won;

    private void Update()
    {
        _winCheckImage.fillAmount = _winCheckTimer / _winCheckTimerMax;
        _winCheckImage.color = _winCheckTimer == _winCheckTimerMax ? Color.green : Color.yellow;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Hip") && !_won)
        {
            _winCheckTimer = Mathf.MoveTowards(_winCheckTimer, _winCheckTimerMax, Time.deltaTime);
            if (_winCheckTimer == _winCheckTimerMax)
            {
                _won = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().EnableNextScene();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Hip") && !_won)
        {
            _winCheckTimer = 0;
        }
    }
}
