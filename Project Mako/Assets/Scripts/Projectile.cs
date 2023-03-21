using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 target;
    private Rigidbody projectileRigidbody;
    private Collider hitBox;
    private MeshRenderer meshRenderer;
    [SerializeField] private float speed;
    private void Awake()
    {
        projectileRigidbody = GetComponentInChildren<Rigidbody>();
        hitBox = GetComponentInChildren<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other) {
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        meshRenderer.enabled = false;
        hitBox.enabled = false;
        projectileRigidbody.angularDrag = 0;
        projectileRigidbody.drag = 0;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
