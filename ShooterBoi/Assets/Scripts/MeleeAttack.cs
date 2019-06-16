using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttack
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
            Debug.Log("Attack");
        }
    }

    public void AnimateAttack(Animator anim)
    {
        if (animateAttack)
        {
            anim.SetTrigger("AttackLeft");
            animateAttack = false;
        }
    }
}
