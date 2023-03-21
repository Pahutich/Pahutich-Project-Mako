using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Projectile>())
        {
            Destroy(gameObject);
        }
    }
}
