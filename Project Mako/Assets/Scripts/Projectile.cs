using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody projectileRigidbody;
    private Collider hitBox;
    private MeshRenderer meshRenderer;
    [SerializeField] private float speed;
    [SerializeField] private int damageToDeal;
    private void Awake()
    {
        projectileRigidbody = GetComponentInParent<Rigidbody>();
        hitBox = GetComponentInChildren<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other) {
        Health health = other.gameObject.GetComponent<Health>();
        
        if(health != null)
        {
            health.GetHealthSystem().Damage(damageToDeal);
        }
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        meshRenderer.enabled = false;
        hitBox.enabled = false;
        projectileRigidbody.angularDrag = 0;
        projectileRigidbody.drag = 0;
        yield return new WaitForSeconds(1);
        Destroy(transform.parent.gameObject);
    }
}
