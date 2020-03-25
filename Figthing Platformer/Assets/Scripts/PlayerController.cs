using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public Collision coll;


    public float moveSpeed = 5.0f;
    public float jumpVelocity = 10f;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 3f;
    public int numJumps = 2;

    public float dashSpeed = 20;


    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    public bool hasDashed;

    public bool groundTouch;



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        Walk(dir);

        //Better Jumping
        if (Input.GetKeyDown(KeyCode.W) && numJumps > 0)
        {
            Jump(Vector2.up);
            numJumps--;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && hasDashed == false)
        {
            if (xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);

            numJumps = 0;
        }

        // jump reset
        if (coll.onGround)
        {
            numJumps = 2;
            rb.gravityScale = 1;
            hasDashed = true;
        }

        if (!coll.onGround)
        {
            if (numJumps == 2)
            {
                numJumps = 1;
            }
            hasDashed = false;
        }



    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
    }

    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpVelocity;

    }



    private void Dash(float x, float y)
    {
        hasDashed = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {

        StartCoroutine(GroundDash());
        rb.gravityScale = 0;

        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);


        rb.gravityScale = 3;

        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
            gameObject.transform.position = new Vector3(0f, -1.485f, 0f);
    }
}
