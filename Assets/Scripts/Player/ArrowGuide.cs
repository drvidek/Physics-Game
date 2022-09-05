using UnityEngine;

public class ArrowGuide : MonoBehaviour
{
    [SerializeField] Transform _pointingAt;
    [SerializeField] Transform _followObj;
    [SerializeField] Vector3 _followOffset;
    Renderer[] _myMesh;

    private void Start()
    {
        if (_pointingAt == null)
            _pointingAt = GameObject.Find("Goal Zone").transform;

        _myMesh = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        transform.position = _followObj.position + _followOffset;
        transform.forward = ( _pointingAt.position - transform.position).normalized;

        foreach (Renderer mesh in _myMesh)
        {
            mesh.enabled = Vector3.Distance(transform.position, _pointingAt.position) > 5f;
        }
    }
}
