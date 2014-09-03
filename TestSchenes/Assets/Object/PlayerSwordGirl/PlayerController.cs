using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 2f;
    bool facingLeft = false;
    bool grounded = false;
    private Animator CharactorAnimator { get; set; }
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    float groundRadius = 0.2f;
    public Transform groundCheck;
    // Use this for initialization
    void Start()
    {
        CharactorAnimator = this.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(this.transform.position, groundRadius, whatIsGround);
        //CharactorAnimator.SetBool("Grounded", grounded);

        var move = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector3(move * maxSpeed, rigidbody2D.velocity.y);

        if (move != 0)
        {
            CharactorAnimator.SetBool("Moving", true);
        }
        else
        {
            CharactorAnimator.SetBool("Moving", false);
        }

        if (move < 0 && !facingLeft)
        {
            Flip();
        }
        else if (move > 0 && facingLeft)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JUMP!1");
            //rigidbody2D.AddForce(new Vector2(0, jumpForce));
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y + 200);
            //rigidbody2D.velocity.y = maxSpeed;

        }

        CharactorAnimator.SetBool("Grounded", grounded);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("ATTACK!1");
            CharactorAnimator.SetBool("AttackUpToBot", true);
        }
        else
        {
            CharactorAnimator.SetBool("AttackUpToBot", false);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Debug.Log("ATTACK!2");
            CharactorAnimator.SetBool("AttackFoward", true);
        }
        else
        {
            CharactorAnimator.SetBool("AttackFoward", false);

        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
