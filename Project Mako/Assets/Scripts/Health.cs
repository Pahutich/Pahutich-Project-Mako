using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected Collider hitBox;
    protected AudioSource audioSource;
    protected MeshRenderer meshRenderer;
    private void Awake() {
        hitBox = GetComponentInChildren<Collider>();
        audioSource = GetComponentInChildren<AudioSource>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected virtual IEnumerator SelfDestroy()
    {
        audioSource.Play();
        meshRenderer.enabled = false;
        hitBox.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
