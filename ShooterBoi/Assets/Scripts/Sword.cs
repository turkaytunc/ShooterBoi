using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponController
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update(GameHandler.instance.GetMouseWorldPos());
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        attacking = true;
    }

    public void Stop()
    {
        attacking = false;
    }

}
