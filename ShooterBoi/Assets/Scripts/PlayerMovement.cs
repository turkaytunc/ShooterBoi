using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    public enum State { Idle,Move, Attack}
    State state = State.Idle;

    IAttack attackType = new MeleeAttack();

    private float speed = 50f;
    private float xMov;
    private float yMov;
    private Rigidbody2D rb2d;
    private Vector2 xMovVector;
    private Animator anim2d;

    private Vector2 lastMoveDir;

    bool lookRight = true;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim2d = GetComponent<Animator>();
    }


    void Update()
    {
        if (state != State.Attack)
        {
            xMov = Input.GetAxisRaw("Horizontal");
            yMov = Input.GetAxisRaw("Vertical");
        }
        else
        {
            xMov = 0;
            yMov = 0;
        }


        if (xMov < 0)
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.x, -180, transform.rotation.z);
            if (lookRight)
            {
                FlipFace();
            }
        }
        else if (xMov > 0)
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            if (!lookRight)
            {
                FlipFace();
            }
        }

        anim2d.SetFloat("HorizontalMovement", xMov);
        anim2d.SetFloat("VerticalMovement", yMov);
        anim2d.SetFloat("MovementMagnitude", new Vector2(xMov, yMov).magnitude);

        bool isIdle = xMov == 0 && yMov == 0;
        if(isIdle)
        {
            anim2d.SetFloat("IdleRight", lastMoveDir.x);
            anim2d.SetFloat("IdleUP", lastMoveDir.y);
        }
        else
        {
            lastMoveDir = new Vector2(xMov, yMov);
        }

        if (Input.GetMouseButtonDown(0))
        {
            state = State.Attack;   
            attackType.Update();
            attackType.AnimateAttack(anim2d);
        }
    }
    private void FixedUpdate()
    {
        xMovVector = new Vector2(xMov * speed * Time.fixedDeltaTime, yMov * speed * Time.fixedDeltaTime);
        rb2d.velocity = xMovVector;
    }

    void FlipFace()
    {
        Debug.Log("Flipped Face");
        lookRight = !lookRight;
        transform.Rotate(Vector3.up * 180);
    }


    public void AttackStateChange()
    {
        state = State.Idle;
    }
}
