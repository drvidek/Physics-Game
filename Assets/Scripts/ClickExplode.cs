using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickExplode : MonoBehaviour
{
    [SerializeField] float _explodeForce;
    [SerializeField] float _explodeRadius, _explodeUpForce;
    [SerializeField] GameObject _explosionPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.name.Contains("Plane"))
                {
                    Collider[] hitRagdolls = Physics.OverlapSphere(hit.point, _explodeRadius);
                    foreach (Collider coll in hitRagdolls)
                    {
                        if (coll.TryGetComponent<Rigidbody>(out Rigidbody rb))
                        {
                            rb.AddExplosionForce(_explodeForce, hit.point, _explodeRadius, _explodeUpForce, ForceMode.Impulse);
                            //Vector3 direction = coll.transform.position - hit.point;
                            //float sqMag = direction.sqrMagnitude;
                            //Vector3 finalForce = direction.normalized * (_explodeForce / sqMag);
                            //rb.AddForce(finalForce, ForceMode.Force);
                        }
                    }
                }

                Instantiate(_explosionPrefab, hit.point, new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
