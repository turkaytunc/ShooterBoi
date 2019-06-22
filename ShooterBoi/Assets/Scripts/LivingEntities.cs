using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntities : MonoBehaviour, IDamageAble
{

    protected User user;

    public User User { get => user; }

    //HealthSystem
    public GameObject regenEffect;
    public GameObject bar;
    public float startingHealth;
    HealthBar healthBar;
    HealthSystem healthSystem;
    public bool RegenIsOpen = false;
    protected virtual void Start()
    {
        //Health System Ayarlamalari
        healthBar = new HealthBar(bar);
        healthSystem = new HealthSystem(startingHealth, regenEffect, healthBar);
        healthSystem.OnDeath += Die;
        healthSystem.Regeneration += Regeneration;

    }

    protected virtual void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            healthSystem.TakeDamage(1f);
        }

        if (RegenIsOpen)
        {
            healthSystem.RegenCheck();
        }

    }

    public void TakeDamage(float damage)
    {
        healthSystem.TakeDamage(damage);
    }

    public void Regeneration()
    {
        StartCoroutine(healthSystem.Regen());
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void Heal(float healAmount)
    {
        healthSystem.Heal(healAmount);
    }
}
