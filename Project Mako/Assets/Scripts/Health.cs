using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected Collider hitBox;
    protected AudioSource audioSource;
    protected MeshRenderer meshRenderer;
    protected HealthSystem healthSystem;
    [SerializeField] protected int health;
    private void Awake()
    {
        SetupHealthObject();
    }

    protected void SetupHealthObject()
    {
        hitBox = GetComponentInChildren<Collider>();
        audioSource = GetComponentInChildren<AudioSource>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        healthSystem = new HealthSystem(health);
        healthSystem.OnHealthChanged += StartDestructionProcess;
        healthSystem.OnDead += () => StartCoroutine(SelfDestroy());
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
    protected void StartDestructionProcess()
    {
        Debug.Log("heloooioo");
        if(healthSystem.GetHealth() <= 0)
        StartCoroutine(SelfDestroy());
    }
    protected virtual IEnumerator SelfDestroy()
    {
        Debug.Log("i am about to destroy myself");
        audioSource.Play();
        meshRenderer.enabled = false;
        hitBox.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
