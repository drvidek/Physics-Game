using UnityEngine;

public class ElectricFence : MonoBehaviour
{
    [SerializeField] private Material _electricityMat;
    [SerializeField] private float _animSpd;

    [SerializeField] private float _slowdownMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        if (_electricityMat == null)
        {
            _electricityMat = GetComponent<LineRenderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Material myMat = _electricityMat;
        myMat.mainTextureOffset += new Vector2(_animSpd * Time.deltaTime, 0);
        _electricityMat = myMat;
    }

    private void OnTriggerEnter(Collider other)
    {

        Ragdoll ragdoll = other.GetComponentInParent<Ragdoll>();
        if (ragdoll == null || !other.name.Contains("Hips")) return;
        ragdoll.ApplySlowdown(_slowdownMultiplier);
       
    }
}

