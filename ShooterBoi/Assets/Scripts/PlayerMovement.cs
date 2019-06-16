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
                transform.rotation = Quaternion.Euler(transform.rotation.x, -180, transform.rotation.z);
                anim2d.SetFloat("HorizontalMovement", xMov);
            }
            else if (xMov >= 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                anim2d.SetFloat("HorizontalMovement", xMov);
            }

            anim2d.SetFloat("VerticalMovement", yMov);

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

    public void AttackStateChange()
    {
        state = State.Idle;
    }
}
