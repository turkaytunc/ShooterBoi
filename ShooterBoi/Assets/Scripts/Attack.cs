using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float fireRate = 2f;
    private float timeToFire = 0f;


    // Start is called before the first frame update
    void Start()
    {
        timeToFire = 1 / fireRate;
        
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
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
