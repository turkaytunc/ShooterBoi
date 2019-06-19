using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IAttack
{

    Sword sword;

    private float attackRate = 0.1f;

    float nextAttackTime;
    bool animateAttack;

    public MeleeAttack(Sword sword)
    {
        this.sword = sword;
    }

    public void Update()
    {
        if(Time.time > nextAttackTime)
        {
            animateAttack = true;
            nextAttackTime = Time.time + attackRate;
        }
    }

    public void AnimateAttack(Animator anim)
    {
        if (animateAttack)
        {
            sword.Attack();
            anim.SetTrigger("Attack");
            animateAttack = false;
        }
    }
}
