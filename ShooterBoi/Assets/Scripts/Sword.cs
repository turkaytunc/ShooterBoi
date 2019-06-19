using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponController
{
    Animator anim;

    Camera cam;
    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update(cam);
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
