using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponController
{
    Animator anim;
    EdgeCollider2D collider;

    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
   //void Update()
   //{
   //    base.Update(GameHandler.instance.GetMouseWorldPos());
   //}
    
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
