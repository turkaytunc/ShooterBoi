using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntities
{

    public Bow bow;

    enum State { Idle, Moving, Attacking }
    State state = State.Idle;
    

    Transform target;
    Rigidbody2D rb2d;
    Animator anim;

    Vector2 velocity;

    float speed = 30f;

    bool lookRight = true;

    float timeBetweenShots = 1.51f;
    float nextAttackTime;

    float rangeAttackDistance = 1.5f;
    protected override void Start()
    {
        base.Start();
        target = GameObject.FindObjectOfType<PlayerMovement>().transform;

        bow.usingBow = GetTargetPos;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("RangeAttack", true);

        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        while(target != null)
        {
            if (state != State.Attacking)
            {
                Vector2 targetDir = (target.position - transform.position).normalized;

                velocity = new Vector2(targetDir.x * speed * Time.deltaTime, targetDir.y * speed * Time.deltaTime);

                rb2d.velocity = velocity;


                SetWalkingAnimation(rb2d.velocity);


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
            else
            {
                rb2d.velocity = Vector2.zero;
                SetWalkingAnimation(rb2d.velocity);
            }

            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    protected override void Update()
    {
        base.Update();
        if(Time.time > nextAttackTime)
        {
            if(InAttackDistance())
            {
                nextAttackTime = Time.time + (timeBetweenShots / bow.AttackSpeed);
                StartCoroutine(RangeAttack());
            }
        }
    }

    IEnumerator RangeAttack()
    {
        rb2d.velocity = Vector2.zero;
        state = State.Attacking;
        anim.SetTrigger("Attack");
        bow.Shoot();

        while (bow.animationActive || InAttackDistance())
        {
            yield return null;
        }
        state = State.Idle;
    }


    void FlipFace()
    {
        lookRight = !lookRight;
        transform.Rotate(Vector3.up * 180);
        bar.transform.parent.transform.Rotate(Vector3.up * 180);
    }

    void SetWalkingAnimation(Vector2 velocity)
    {
        float moveMagnitude = new Vector2(velocity.x, velocity.y).magnitude;
        anim.SetFloat("HorizontalMovement", velocity.x);
        anim.SetFloat("VerticalMovement", velocity.y);
        anim.SetFloat("MovementMagnitude", moveMagnitude);
    }

     Vector3 GetTargetPos()
    {
        Vector3 pos = target.position;
        return pos;
    }

    void IncreaseBowAttackSpeed()
    {
        bow.AttackSpeed += 0.1f;
    }

    bool InAttackDistance()
    {
        float sqrDisToTarget = (target.position - transform.position).sqrMagnitude;
        return sqrDisToTarget < Mathf.Pow(rangeAttackDistance, 2);
    }
    public void AttackStateChange()
    {
    }
}
