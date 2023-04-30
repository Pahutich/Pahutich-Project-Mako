
using System;

public class HealthSystem
{
    private int healthMax;
    private int health;
    public event Action<HealthSystem> OnHealthChanged;
    public event Action OnDead;
    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        OnHealthChanged?.Invoke(this);
        if (health <= 0)
        {
            health = 0;
            OnDead?.Invoke();
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        OnHealthChanged?.Invoke(this);
    }
}
