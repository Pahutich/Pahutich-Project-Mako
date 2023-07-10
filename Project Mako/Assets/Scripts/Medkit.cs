using System.Collections;
using UnityEngine;

public class Medkit : MonoBehaviour, ICollectable
{
    [SerializeField] private int amountToHeal;
    private MeshRenderer meshRenderer;
    private Collider hitBox;
    private AudioSource audioSource;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        hitBox = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Collect<T>(T health)
    {
        if(!typeof(T).Equals(typeof(Health)))
        return;

        var healthConverted = (Health)(object)health;
        healthConverted.GetHealthSystem().Heal(amountToHeal);
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
    private void OnTriggerEnter(Collider other)
    {
        var encounteredHealthEntity = other.GetComponent<Health>();
        if (encounteredHealthEntity)
        {
            Collect<Health>(encounteredHealthEntity);
        }
    }
}
