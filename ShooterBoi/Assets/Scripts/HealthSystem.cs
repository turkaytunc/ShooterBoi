using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    float healthMax;
    float currentHealth;

    public float CurrentHealth { get => currentHealth; }

    public float HealthMax { get => healthMax; }

    public event Action OnDeath;
    public float regen = 0.1f;

    public HealthSystem(float healthMax)
    {
        this.healthMax = healthMax;
        currentHealth = this.healthMax;
    }

    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        CurrentHealthMax();
    }

    public void Regeneration()
    {
        currentHealth += regen * Time.deltaTime;
        CurrentHealthMax();
    }

    public void Die()
    {
        if(OnDeath != null)
        {
            OnDeath();
        }
    }

    void CurrentHealthMax()
    {
        if(currentHealth > healthMax)
        {
            currentHealth = healthMax;
        }
    }
}
