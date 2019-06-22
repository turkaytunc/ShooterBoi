using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : IAttack
{
    Bow bow;

    private float attackRate = 0.1f;

    float nextAttackTime;
    bool animateAttack;

    public RangeAttack(Bow bow)
    {
        this.bow = bow;
    }

    public void Update()
    {
        if (Time.time > nextAttackTime)
        {
            animateAttack = true;
            nextAttackTime = Time.time + attackRate;
        }
    }

    public void AnimateAttack(Animator anim)
    {
        if (animateAttack)
        {
            bow.Shoot();
            anim.SetTrigger("Attack");
            anim.SetFloat("Attack Speed", 1.35f * bow.AttackSpeed);
            animateAttack = false;
        }
    }

}
