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
    [SerializeField] protected string healthHolderName;
    private void Awake()
    {
        SetupHealthObject();
    }

    protected void SetupHealthObject()
    {
        hitBox = GetComponentInChildren<Collider>();
        audioSource = GetComponentInChildren<AudioSource>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        healthSystem = new HealthSystem(health, healthHolderName);
        healthSystem.OnHealthChanged += StartDestructionProcess;
        healthSystem.OnDead += () => StartCoroutine(SelfDestroy());
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
    protected void StartDestructionProcess(HealthSystem hs)
    {
        if (hs.GetHealth() <= 0)
            StartCoroutine(SelfDestroy());
    }
    protected virtual IEnumerator SelfDestroy()
    {
        audioSource.Play();
        meshRenderer.enabled = false;
        hitBox.enabled = false;
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
            //item.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
