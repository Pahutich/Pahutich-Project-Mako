using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotObject : Health
{
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Projectile>())
        {
            StartCoroutine(base.SelfDestroy());
        }
    }
}
