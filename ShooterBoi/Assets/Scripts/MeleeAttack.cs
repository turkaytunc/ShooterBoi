using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IAttack
{
    private float attackRate = 0.1f;

    float nextAttackTime;
    bool animateAttack;

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
            anim.SetTrigger("Attack");
            animateAttack = false;
        }
    }
}
