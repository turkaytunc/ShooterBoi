using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{

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
        xMov = Input.GetAxisRaw("Horizontal");
        yMov = Input.GetAxisRaw("Vertical");

        

        if(xMov < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, -180, transform.rotation.z);
            anim2d.SetFloat("HorizontalMovement", xMov);
        }
        else if(xMov >=0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            anim2d.SetFloat("HorizontalMovement", xMov);
        }

        anim2d.SetFloat("VerticalMovement", yMov);  


    }

    private void FixedUpdate()
    {
        xMovVector = new Vector2(xMov * speed * Time.fixedDeltaTime, yMov * speed * Time.fixedDeltaTime);
        rb2d.velocity = xMovVector;
    }
}
