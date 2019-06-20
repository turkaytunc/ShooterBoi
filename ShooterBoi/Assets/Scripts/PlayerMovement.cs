using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    //HealthSystem
    public GameObject regenEffect;
    public GameObject bar;
    HealthBar healthBar;
    HealthSystem healthSystem;

    //Weapons
    public Bow bow;
    public Sword sword;

    //State
    public enum State { Idle,Move, Attack}
    State state = State.Idle;

    //Current Weapon
    static IAttack[] attacks;
    AttackTypes currentAttackType = AttackTypes.Melee;
    IAttack attack;

    //Player movement
    private float speed = 50f;
    private float xMov;
    private float yMov;

    private Vector2 xMovVector;
    private Vector2 lastMoveDir = new Vector2(0f, -1f); //Starting look direction

    //Player Component References
    private Rigidbody2D rb2d;
    private Animator anim2d;

    
    bool lookRight = true;

    private void Awake()
    {
        //Degismez component referanslarinin atanmasi
        rb2d = GetComponent<Rigidbody2D>();
        anim2d = GetComponent<Animator>();

    }

    private void Start()
    {
        //Health System Ayarlamalari
        healthBar = new HealthBar(bar);
        healthSystem = new HealthSystem(10f, regenEffect, healthBar);
        healthSystem.OnDeath += Die;

        //Baslangicta kullanilacak silahlar ve baslangicta kullanilan silah (sword)
        attacks = new IAttack[]{ new MeleeAttack(sword), new RangeAttack(bow) };
        attack = attacks[(int)currentAttackType];

        //Silah baslangic ayarlari
        bow.gameObject.SetActive(false);
        //sword.gameObject.SetActive(true);

        //Animastyon icin baslangic moduna gore range veya melee arasindaki secim (Range false ayari silinebilir otomatik olarak false zaten)
        anim2d.SetBool("MeleeAttack", true);
        anim2d.SetBool("RangeAttack", false);
        healthSystem.Regeneration += Regeneration;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.K))
        {
            healthSystem.TakeDamage(1f);
        }

        healthSystem.RegenCheck();


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

        float moveMagnitude = new Vector2(xMov, yMov).magnitude;

        if(moveMagnitude != 0 && state != State.Attack)
        {
            state = State.Move;
        }
        else if( state != State.Attack)
        {
            state = State.Idle;
        }

        anim2d.SetFloat("HorizontalMovement", xMov);
        anim2d.SetFloat("VerticalMovement", yMov);
        anim2d.SetFloat("MovementMagnitude", moveMagnitude);

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
            if (state != State.Attack)
            {
                state = State.Attack;
                attack.Update();
                attack.AnimateAttack(anim2d);
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            ChangeAttackType();
        }
    }
    private void FixedUpdate()
    {
        Vector2 mov = new Vector2(xMov, yMov).normalized;

        xMovVector = new Vector2(mov.x * speed * Time.fixedDeltaTime, mov.y * speed * Time.fixedDeltaTime);
        rb2d.velocity = xMovVector;
    }

    void FlipFace()
    {
        lookRight = !lookRight;
        transform.Rotate(Vector3.up * 180);
        bar.transform.parent.transform.Rotate(Vector3.up * 180);
    }


    public void AttackStateChange()
    {
        state = State.Idle;
    }

    void ChangeAttackType()
    {

        if(currentAttackType == AttackTypes.Melee)
        {
            currentAttackType = AttackTypes.Range;
            anim2d.SetBool("RangeAttack", true);
            anim2d.SetBool("MeleeAttack", false);
            bow.gameObject.SetActive(true);
            //sword.gameObject.SetActive(false);
        }
        else
        {
            currentAttackType = AttackTypes.Melee;
            anim2d.SetBool("MeleeAttack", true);
            anim2d.SetBool("RangeAttack", false);
            bow.gameObject.SetActive(false);
            //sword.gameObject.SetActive(true);
        }

        attack = attacks[(int)currentAttackType];

    }

    public void Regeneration()
    {
        StartCoroutine(healthSystem.Regen());
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
