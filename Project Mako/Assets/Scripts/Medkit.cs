using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, ICollectable
{
    [SerializeField] private int amountToHeal;
    public void Collect(Health health)
    {
        health.GetHealthSystem().Heal(amountToHeal);
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
