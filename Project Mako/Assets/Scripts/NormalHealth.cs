using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHealth : Health
{
    [SerializeField] private int health;
    HealthSystem healthSystem;
    // Start is called before the first frame update
    void Awake()
    {
        healthSystem = new HealthSystem(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
