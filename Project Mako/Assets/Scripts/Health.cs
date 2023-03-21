using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected Collider hitBox;

    private void Awake() {
        hitBox = GetComponentInChildren<Collider>();
    }
}
