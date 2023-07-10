using System.Collections;
using UnityEngine;

public class WeaponCooler : MonoBehaviour, ICollectable
{
    [SerializeField] private int amountToCool;
    private MeshRenderer meshRenderer;
    private Collider hitBox;
    private AudioSource audioSource;
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        hitBox = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Collect<T>(T shooter)
    {
        if(!typeof(T).Equals(typeof(Shooter)))
        return;

        var shooterConverted = (Shooter)(object)shooter;
        shooterConverted.currentOverheat -= amountToCool;
        audioSource.Play();
        meshRenderer.enabled = false;
        hitBox.enabled = false;

        StartCoroutine(SelfDestroy());
    }
    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        var encounteredGunOwner = other.GetComponentInChildren<Shooter>();
        if(encounteredGunOwner)
        {
            Collect<Shooter>(encounteredGunOwner);
        }
    }
}
