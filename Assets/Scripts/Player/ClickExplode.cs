using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickExplode : MonoBehaviour
{
    [System.Serializable]
    public struct Explosion
    {
        public float force;
        public float radius;
        public float upForce;
        public Button button;
    }
    [SerializeField] Explosion[] explosionOptions;
    [SerializeField] Explosion currentExplosion;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] bool _addExplosionForce;
    bool _explodePending;

    private void Start()
    {
        SetExplosion(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _explodePending = true;
        }
    }

    private void FixedUpdate()
    {
        if (_explodePending)
        {
            _explodePending = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] tempHits = Physics.RaycastAll(ray);
            List<RaycastHit> hits = new List<RaycastHit>();

            for (int i = 0; i < tempHits.Length; i++)
            {
                hits.Add(tempHits[i]);
            }

            hits.Sort(
                (x,y) => Vector3.Distance(x.point, ray.origin).CompareTo(Vector3.Distance(y.point, ray.origin))
                );

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.attachedRigidbody == null && !hit.collider.isTrigger)
                {
                    Instantiate(_explosionPrefab, hit.point, Quaternion.identity);
                    Collider[] hitRagdolls = Physics.OverlapSphere(hit.point, currentExplosion.radius);
                    foreach (Collider coll in hitRagdolls)
                    {
                        if (coll.TryGetComponent<Rigidbody>(out Rigidbody rb))
                        {
                            if (_addExplosionForce)
                            {
                                rb.AddExplosionForce(currentExplosion.force, hit.point, currentExplosion.radius, currentExplosion.upForce, ForceMode.Impulse);
                            }
                            else
                            {
                                Vector3 direction = (coll.transform.position - hit.point) + (Vector3.up * currentExplosion.upForce);
                                float sqMag = direction.sqrMagnitude;
                                Vector3 finalForce = direction.normalized * (currentExplosion.force / sqMag);
                                rb.AddForce(finalForce, ForceMode.Impulse);
                            }
                        }
                    }
                    return;
                }
            }
        }
    }

    public void SetExplosion(int i)
    {
        if (currentExplosion.button != null)
        {
            ColorBlock oldButtonCol = currentExplosion.button.colors;
            oldButtonCol.normalColor = Color.white;
            currentExplosion.button.colors = oldButtonCol;
        }

        currentExplosion = explosionOptions[i];
        ColorBlock newButtonCol = currentExplosion.button.colors;
        newButtonCol.normalColor = currentExplosion.button.colors.selectedColor;
        currentExplosion.button.colors = newButtonCol;
    }
}
