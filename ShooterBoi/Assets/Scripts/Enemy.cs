using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    enum State { Idle, Moving, Attacking }
    State state = State.Idle;


    public GameObject bar;
    HealthBar healthBar;
    HealthSystem healthSystem;
    

    Transform target;
    Rigidbody2D rb2d;
    Animator anim;

    Vector2 velocity;

    float speed = 30f;

    bool lookRight = true;

    float timeBetweenShots = 2f;
    float nextAttackTime;

    float rangeAttackDistance = 1.5f;
    void Start()
    {
        healthBar = new HealthBar(bar);
        healthSystem = new HealthSystem(20f, null, healthBar);
        healthSystem.OnDeath += Die;

        target = GameObject.FindObjectOfType<PlayerMovement>().transform;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        state = State.Moving;
        while(target.position != null)
        {
            if (state != State.Attacking)
            {
                Vector2 targetDir = (target.position - transform.position).normalized;

                velocity = new Vector2(targetDir.x * speed * Time.deltaTime, targetDir.y * speed * Time.deltaTime);

                rb2d.velocity = velocity;


                float moveMagnitude = new Vector2(rb2d.velocity.x, rb2d.velocity.y).magnitude;

                anim.SetFloat("HorizontalMovement", rb2d.velocity.x);
                anim.SetFloat("VerticalMovement", rb2d.velocity.y);
                anim.SetFloat("MovementMagnitude", moveMagnitude);


                if (rb2d.velocity.x < 0)
                {
                    //transform.rotation = Quaternion.Euler(transform.rotation.x, -180, transform.rotation.z);
                    if (lookRight)
                    {
                        FlipFace();
                    }
                }
                else if (rb2d.velocity.x > 0)
                {
                    //transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                    if (!lookRight)
                    {
                        FlipFace();
                    }
                }
            }


            yield return new WaitForSeconds(0.25f);
        }

        yield return null;
    }

    private void Update()
    {
        if(Time.time > nextAttackTime)
        {
            float sqrDisToTarget = (target.position - transform.position).sqrMagnitude;
            if(sqrDisToTarget < Mathf.Pow(rangeAttackDistance, 2))
            {
                nextAttackTime += Time.time;
                StartCoroutine(RangeAttack());
            }
        }
    }

    IEnumerator RangeAttack()
    {
        state = State.Attacking;

        //Vector2 targetDir = (target.transform - )
        yield return null;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void FlipFace()
    {
        lookRight = !lookRight;
        transform.Rotate(Vector3.up * 180);
        bar.transform.parent.transform.Rotate(Vector3.up * 180);
    }
}
