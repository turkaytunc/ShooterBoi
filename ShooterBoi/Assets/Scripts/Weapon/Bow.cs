using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bow : WeaponController
{
    [NonSerialized]
    public User user;

    public float damage = 1f;

    public delegate Vector3 AimPosition();
    public AimPosition usingBow;

    [SerializeField]
    private GameObject bullet;

    public bool animationActive = false;

    //float radius = 0.3f;
    //private float fireRate = 2f;
    //private float timeToFire = 0f;
    //private float offsetValue = 1f;
    public Transform shootPosition;
    Animator anim;

    public float attackSpeed = 1;

    public float AttackSpeed
    {
        get => attackSpeed;
        set
        {
            if (value < 1) value = 1;
            attackSpeed = value;
        SetAnimationAttackSpeed();
        }
    }

    void Start()
    {
        //timeToFire = 1 / fireRate;
        anim = GetComponent<Animator>();
        SetAnimationAttackSpeed();
    }

    void Update()
    {
        if (usingBow != null)
        {
            base.Update(usingBow());
        }
        //if (!attacking)
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    Plane plane = new Plane(Vector3.forward, 0f);
        //    float rayDistance;
        //    Vector3 position = Vector3.zero;
        //    Vector3 dir = Vector3.zero;
        //    if (plane.Raycast(ray, out rayDistance))
        //    {
        //        position = ray.GetPoint(rayDistance);
        //        dir = (position - origin.position).normalized;
        //    }
        //    Vector3 bowPosition = origin.position + dir * radius;
        //    this.transform.position = bowPosition;
        //    float angle = 90 - Mathf.Rad2Deg * Mathf.Atan2(dir.x, dir.y);
        //    transform.eulerAngles = Vector3.forward * angle;
        //}

        //timeToFire -= Time.deltaTime;
        //
        //if (timeToFire <= 0 && Input.GetMouseButtonDown(0))
        //{
        //    Shoot();
        //}
    }

    public void Shoot()
    {
        animationActive = true;
        SetAnimationAttackSpeed();
        anim.SetBool("IsActive", animationActive);

        anim.SetTrigger("Attack");
        attacking = true;
    }

    public void BulletTime()
    {
        Bullet bul = Instantiate(bullet.GetComponent<Bullet>(), shootPosition.transform.position, shootPosition.transform.rotation) as Bullet;
        if (usingBow != null)
        {
            bul.targetPosition = usingBow();
            bul.SetDamage(damage);
            bul.SetUser(user);
        }
        attacking = false;

        animationActive = false;
        anim.SetBool("IsActive", animationActive);
    }

    void SetAnimationAttackSpeed()
    {
        anim.SetFloat("AttackSpeed", attackSpeed * 1.51f);
    }
}
