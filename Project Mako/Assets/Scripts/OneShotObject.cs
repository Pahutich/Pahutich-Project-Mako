using UnityEngine;

public class OneShotObject : Health
{
    private void Awake() {
        base.SetupHealthObject();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Projectile>())
        {
            base.SelfDestroy();
        }
    }
}
