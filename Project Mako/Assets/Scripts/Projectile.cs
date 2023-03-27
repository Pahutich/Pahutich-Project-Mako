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
            Debug.Log(health.GetHealthSystem().GetHealth());
        }
        StartCoroutine(SelfDestroy());
    }
    public void OnShot(Vector3 direction)
    {
        projectileRigidbody.AddForce(direction * 50f, ForceMode.Impulse);
    }

    private IEnumerator SelfDestroy()
    {
        meshRenderer.enabled = false;
        hitBox.enabled = false;
        projectileRigidbody.angularDrag = 0;
        projectileRigidbody.drag = 0;
        yield return new WaitForSeconds(1);
        if(transform.parent != null)
        Destroy(transform.parent.gameObject);
        else
        {
            Destroy(gameObject);
        }
    }
}
