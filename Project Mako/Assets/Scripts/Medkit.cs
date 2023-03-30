using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, ICollectable
{
    [SerializeField] private int amountToHeal;
    private MeshRenderer meshRenderer;
    private Collider hitBox;
    private AudioSource audioSource;
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        hitBox = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Collect(Health health)
    {
        StartCoroutine(OnCollect(health));
    }

    private IEnumerator OnCollect(Health health)
    {
        health.GetHealthSystem().Heal(amountToHeal);
        audioSource.Play();
        meshRenderer.enabled = false;
        hitBox.enabled = false;
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        var encounteredHealthEntity = other.GetComponent<Health>();
        if(encounteredHealthEntity)
        {
            Collect(encounteredHealthEntity);
        }
    }
}
