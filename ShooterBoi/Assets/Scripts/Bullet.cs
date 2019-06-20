using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform rayPosition;
    public Vector3 targetPosition { get; set; }
    private Vector3 vec;
    private Vector3 dir;
    private float speed = 10f;
    float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(targetPosition);
        dir = (targetPosition - transform.position).normalized;
        //vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //dir = vec - transform.position;
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(dir * speed * Time.deltaTime);   //Mouse Click Position   
        // transform.Translate(Vector2.right * speed * Time.deltaTime); //Duz atis
        RaycastHit2D hit = Physics2D.Raycast(rayPosition.position, dir, speed * Time.deltaTime);
        if(hit.transform != null)
        {
            Destroy(gameObject);
            hit.transform.GetComponent<IDamageAble>().TakeDamage(damage);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
    }


}
