using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem
{
    HealthBar healthBar;
    GameObject regenEffect;

    float healthMax;
    float currentHealth;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value > healthMax)
            {
                value = healthMax;
            }

            currentHealth = value;
            healthBar.Update(CurrentHealth, HealthMax);
        }
    }

    public float HealthMax { get => healthMax; }
        
    public event Action OnDeath;
    public event Action Regeneration;
    public float regen = 10 / 100f;

    public bool regenStarted = false;

    public HealthSystem(float healthMax, GameObject regenEffect, HealthBar healthBar)
    {
        this.healthMax = healthMax;
        currentHealth = this.healthMax;

        this.regenEffect = regenEffect;

        this.healthBar = healthBar;
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealthMax();
    }
    
    public void Die()
    {
        if(OnDeath != null)
        {
            OnDeath();
        }
    }

    public void RegenCheck()
    {
        if(!regenStarted && CurrentHealth < HealthMax)
        {
            if(Regeneration != null)
            {
                Regeneration();
            }
        }
    }

    void CurrentHealthMax()
    {
        if(CurrentHealth > HealthMax)
        {
            CurrentHealth = HealthMax;
        }
    }

    public void HealthInfo()
    {
        Debug.Log("Current Health: " + CurrentHealth);
    }

    public IEnumerator Regen()
    {
        regenStarted = true;

        if (regenEffect != null)
        {
            regenEffect.SetActive(true);
        }

        while(CurrentHealth < HealthMax)
        {
            CurrentHealth += regen;
            yield return new WaitForSeconds(1);
        }

        if (regenEffect != null)
        {
            regenEffect.SetActive(false);
        }

        regenStarted = false;
    }
    
}
