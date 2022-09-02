using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] private float _bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (GameManager.ValidateCollisionWithPlayer(collision, out Ragdoll doll))
            {
                foreach (Rigidbody rigidbody in doll.RigBodies)
                {
                    rigidbody.AddForce(Vector3.up * _bounceForce, ForceMode.Impulse);
                }
                doll.ApplyImpact(0.2f);
                return;
            }

            rb.AddForce(Vector3.up * _bounceForce, ForceMode.Impulse);
        }
    }
}
