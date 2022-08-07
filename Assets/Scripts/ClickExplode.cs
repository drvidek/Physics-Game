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

            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.attachedRigidbody == null)
                {
                    Instantiate(_explosionPrefab, hit.point, Quaternion.identity);
                    Collider[] hitRagdolls = Physics.OverlapSphere(hit.point, currentExplosion.radius);
                    foreach (Collider coll in hitRagdolls)
                    {
                        if (coll.TryGetComponent<Rigidbody>(out Rigidbody rb))
                        {
                            bool _hipsHit = false;

                            if (coll.name.Contains("Hips"))
                                _hipsHit = true;

                            if (_addExplosionForce)
                            {
                                rb.AddExplosionForce(_hipsHit ? currentExplosion.force : currentExplosion.force/2f, hit.point, currentExplosion.radius, currentExplosion.upForce, ForceMode.Impulse);
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
                    break;
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
