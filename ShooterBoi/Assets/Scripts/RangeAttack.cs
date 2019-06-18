using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : IAttack
{

    private float attackRate = 0.1f;

    float nextAttackTime;
    bool animateAttack;


    public void AnimateAttack(Animator anim)
    {
        if (animateAttack)
        {
            anim.SetTrigger("Attack");
            animateAttack = false;
        }
    }

    public void Update()
    {
        if (Time.time > nextAttackTime)
        {
            animateAttack = true;
            nextAttackTime = Time.time + attackRate;
        }
    }
}
