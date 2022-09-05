using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 _posOffset;
    [SerializeField] GameObject _followOb;

    void Start()
    {
        _posOffset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _followOb.transform.position + _posOffset;
    }
}
