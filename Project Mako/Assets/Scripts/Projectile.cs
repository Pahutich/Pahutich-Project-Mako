using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 target;
    private Rigidbody projectileRigidbody;
    [SerializeField] private float speed;
    private void Awake()
    {
        projectileRigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Start() {
        //projectileRigidbody.velocity = transform.forward * speed;
    }
    private void FixedUpdate() {
        //projectileRigidbody.AddForce(target * speed);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
