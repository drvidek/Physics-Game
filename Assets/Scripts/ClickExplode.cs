using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickExplode : MonoBehaviour
{
    [SerializeField] float _explodeForce;
    [SerializeField] float _explodeRadius;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Collider[] hits = Physics.OverlapSphere(hit.point, _explodeRadius);
                foreach (Collider coll in hits)
                {
                    if (coll.TryGetComponent<Rigidbody>(out Rigidbody rb))
                    {
                        rb.AddExplosionForce(_explodeForce, hit.point, _explodeRadius);
                    }

                    //Vector3 direction = coll.transform.position - hit.point;
                    //float sqMag = direction.sqrMagnitude;
                    //Vector3 finalForce = direction.normalized * (_explodeForce / sqMag);
                    //rb.AddForce(finalForce, ForceMode.Impulse);

                }
            }
        }
    }
}
