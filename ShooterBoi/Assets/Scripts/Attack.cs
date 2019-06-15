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


    // Start is called before the first frame update
    void Start()
    {
        timeToFire = 1 / fireRate;
        go = transform.Find("BulletPoint").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        

        timeToFire -= Time.deltaTime;

        if (timeToFire <= 0 && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        Instantiate(bullet, go.transform.position , Quaternion.identity);
    }
}
