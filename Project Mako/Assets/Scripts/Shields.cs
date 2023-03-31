using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    [SerializeField] private float maxShieldCapacity;
    [SerializeField] private float shieldCapacity;
    [SerializeField] private float timeToStartRegenerating;
    [SerializeField] private float regenerationSpeed;
    [SerializeField] private float timeSinceLastHit;
    private Health health;
    public event Action OnShieldCapacityChanged;
    private void Awake()
    {
        health = GetComponent<Health>();
        shieldCapacity = maxShieldCapacity;
        timeSinceLastHit = timeToStartRegenerating;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastHit < timeToStartRegenerating)
        {
            timeSinceLastHit += Time.deltaTime;
            if (timeSinceLastHit > timeToStartRegenerating)
                timeSinceLastHit = timeToStartRegenerating;
        }
        else
        {
            shieldCapacity += Time.deltaTime * regenerationSpeed;
            OnShieldCapacityChanged?.Invoke();
            if (shieldCapacity < 0)
                shieldCapacity = 0;
            if (shieldCapacity > maxShieldCapacity)
                shieldCapacity = maxShieldCapacity;
        }
    }

    public void OnHitReceived(float damageAmount)
    {
        float shieldCapacityBeforeHit = shieldCapacity;
        shieldCapacity -= damageAmount;
        OnShieldCapacityChanged?.Invoke();
        timeSinceLastHit = 0;
        if (damageAmount > shieldCapacity)
            health.GetHealthSystem().Damage((int)damageAmount - (int)shieldCapacityBeforeHit);
    }
    public float GetPercent()
    {
        return (float)shieldCapacity / maxShieldCapacity;
    }
}
