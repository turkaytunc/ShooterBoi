using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponController
{
    [SerializeField]
    private GameObject bullet;

    //float radius = 0.3f;

    //bool attacking = false;

    //private float fireRate = 2f;
    //private float timeToFire = 0f;
    //private float offsetValue = 1f;
    public Transform shootPosition;
    Animator anim;

    Camera cam;

    void Start()
    {
        //timeToFire = 1 / fireRate;
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        base.Update(cam);
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
        anim.SetTrigger("Attack");
        attacking = true;
    }

    public void BulletTime()
    {
        Instantiate(bullet, shootPosition.transform.position, shootPosition.transform.rotation);
        attacking = false;
    }
}
