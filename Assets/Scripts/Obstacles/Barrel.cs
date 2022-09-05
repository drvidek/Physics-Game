using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private GameObject _explodePrefab;
    [SerializeField] private Transform _explodePoint;
    [SerializeField] private float _explodeForce;
    [SerializeField] private float _explodeRadius, _explodeVelocity, _upForce;
    private Rigidbody _rigidbody;
    private bool _exploding;

    private void Start()
    {
        _rigidbody ??= GetComponentInChildren<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.ValidateCollisionWithPlayer(collision, out Ragdoll doll) || _rigidbody.velocity.magnitude > _explodeVelocity)
        {
            StartCoroutine("Explode");
            if (doll != null)
            doll.ApplyImpact(0.2f);
        }
    }

    private IEnumerator Explode()
    {
        _exploding = true;
        Instantiate(_explodePrefab, transform.position, Quaternion.identity, null);
        Collider[] hits = Physics.OverlapSphere(transform.position, _explodeRadius);
        foreach (Collider coll in hits)
        {
            if (coll.TryGetComponent<Barrel>(out Barrel barrel))
            {
                if (!barrel._exploding && barrel != this)
                    barrel.StartCoroutine("Explode");
            }
            else
            if (coll.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = Vector3.zero;
                Vector3 dir = MathExt.Direction(_explodePoint.position, rb.transform.position);
                dir *= _explodeForce;
                rb.AddForce(dir,ForceMode.Impulse);
            }
        }
        yield return null;
        Destroy(gameObject);
    }
}
