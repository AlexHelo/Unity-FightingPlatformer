using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public Collision coll;


    public float moveSpeed = 5.0f;
    public float distance = 1f;
    public float jumpVelocity = 10f;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 3f;
    public float slideDown = -5f;
    public int numJumps = 2;
    public float jumpPushForce = 10f;
    public int side = 1;
    public float wallJumpLerp = 10f;

    public float wallJumpForce = 30f;
    public float dashSpeed = 20;


    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;



    public bool hasDashed;

    public bool groundTouch;

    public GameObject attackPosMeleeLeft, attackPosMeleeRight;

    private GameObject CurrentAttackPos;
    private bool attackPos;
    private Animator an;
    private PlayerAttack plA;



    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        plA = GetComponent<PlayerAttack>();
        CurrentAttackPos = attackPosMeleeRight;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

        //Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        Walk(dir);


        //Better Jumping
        if (Input.GetKeyDown(KeyCode.W) && numJumps > 0 && !coll.onWall)
        {
            an.SetInteger("JumpNo", numJumps);

            Jump(Vector2.up);

            numJumps--;
        }




        if (Input.GetKeyDown(KeyCode.W) && coll.onWall)
        {
            WallJump();

        }
        if (coll.onWall)
        {
            an.SetBool("WallGrab", true);
        }
        else
        {
            an.SetBool("WallGrab", false);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.G) && !coll.onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }



        if (Input.GetKeyDown(KeyCode.Space) && hasDashed == false && !coll.onGround)
        {
            if (xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
            numJumps = 0;
        }

        //walljump tryouts 

        // jump reset
        if (coll.onGround)
        {
            an.SetBool("Jumping", false);
            an.SetBool("Falling", false);
            an.SetBool("DoubleJump", true);
            numJumps = 2;
            an.SetInteger("JumpNo", numJumps);
            rb.gravityScale = 1;
            hasDashed = false;
            wallJumped = true;
        }

        if (!coll.onGround && rb.velocity.y < 0)
        {
            an.SetBool("Falling", true);
        }
        else
        {
            an.SetBool("Falling", false);
        }

        if (!coll.onGround)
        {
            an.SetBool("Jumping", true);
            an.SetBool("DoubleJump", true);
            an.SetInteger("JumpNo", numJumps);

            if (numJumps == 2)
            {
                numJumps = 1;

            }

            if (numJumps == 1)
            {
                //an.SetBool("Falling", false);
                an.SetBool("DoubleJump", true);

            }

            if (numJumps == 0)
            {
                an.SetBool("DoubleJump", false);

            }

        }
        /*
                if (coll.onWall && !coll.onGround)
                {
                    rb.velocity = new Vector2(rb.velocity.x, slideDown);
                }
        */
        AttackCheck();
    }

    private void AttackCheck()
    {
        if (Input.GetMouseButton(0))
        {
            plA.Attack();
        }
    }
    private void Walk(Vector2 dir)
    {
        if (canMove == true)
        {
            if (!wallJumped)
            {
                rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * moveSpeed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
            }

            if (dir.x != 0)
            {
                an.SetBool("Running", true);
            }
            else
            {
                an.SetBool("Running", false);
            }
            if (dir.x < 0)
            {
                spriteRenderer.flipX = true;
                flipAttack(false);
            }
            else if (dir.x > 0)
            {
                spriteRenderer.flipX = false;
                flipAttack(true);
            }
        }

    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void Jump(Vector2 dir)
    {

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpVelocity;
        Debug.Log("DO " + rb.velocity);

    }

    private void WallJump()
    {

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));
        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump(Vector2.up + wallDir);

        //Debug.Log("send " + wallDir);

        wallJumped = true;

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
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        
        rb.gravityScale = 0;

        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);


        rb.gravityScale = 3;

        wallJumped = false;
        isDashing = false;
    }
    private void flipAttack(bool t)
    {
        if (t)
        {
            CurrentAttackPos = attackPosMeleeRight;
            attackPos = true;
        }
        else
        {
            CurrentAttackPos = attackPosMeleeLeft;
            attackPos = false;
        }

        //AttackPosMelee.GetComponent<Transform>().position = new Vector3(-AttackPosMelee.GetComponent<Transform>().position.x, -AttackPosMelee.GetComponent<Transform>().position.y, 0);
    }
    public GameObject GetCurrentAttackPos()
    {
        return CurrentAttackPos;
    }
    public bool GetAttackPos()
    {
        return attackPos;
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
