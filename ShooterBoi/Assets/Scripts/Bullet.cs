using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 vec;
    private Vector3 dir;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = vec - transform.position;
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(dir * speed * Time.deltaTime);   //Mouse Click Position   
        transform.Translate(Vector2.right * speed * Time.deltaTime);      
    }
}
