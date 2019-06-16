using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float fireRate = 2f;
    private float timeToFire = 0f;
    private float offsetValue = 1f;
    private GameObject go;


    void Start()
    {
        timeToFire = 1 / fireRate;
        go = transform.Find("BulletPoint").gameObject;
    }

    //void Update()
    //{
    //    
    //
    //    timeToFire -= Time.deltaTime;
    //
    //    if (timeToFire <= 0 && Input.GetMouseButtonDown(0))
    //    {
    //        Shoot();
    //    }
    //}


    private void Shoot()
    {
        Instantiate(bullet, go.transform.position , Quaternion.identity);
    }
}
