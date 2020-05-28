using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
*  GLOSARY:
*		A) INSTANCE VALUES
*			1) COMPONETS
*			2) MOVEMENT VALUES
*			3) JUMP COUNTERS
*			4) BOOLEANS
*			5) GAME OBJECTS, ANIMATOR AND SCRIPTS
*	
*		B) START METHOD
*		
*		C) UPDATE METHOD
*			1) MOVEMENT GET AXIS VALUES
*			2) KEY CODES
*			3) VELOCITIY SCENARIOS
*			4) ANIMATIONS
*		
*		D) MOVEMENT METHODS
*		
*		E) IENUMERATORS
*		
*		F) AUXILIARY METHODS
* 
*/

public class PlayerController : MonoBehaviour
{
    //Components used for the main player
    SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Collision coll;

    //Public values for all the movement used in the code
    public float moveSpeed = 5.0f;
    public float distance = 1f;
    public float jumpVelocity = 10f;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 3f;
    public float slideDown = -5f;
    public float jumpPushForce = 10f;
    public float wallJumpLerp = 10f;
    public float wallJumpForce = 30f;
    public float dashSpeed = 20;
    //counter for amount of jumps available for the player
    public int numJumps = 2;
    //Booleans for to determine different available movements
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;
    public bool hasDashed;
    public bool groundTouch;
    private bool attackPos,spellPos;

    //Game objects for the left position and right position of the hitbox
    public GameObject attackPosMeleeLeft, attackPosMeleeRight,spellPosLeft,spellPosRight,fireball;
    //Game object determining the side his facing
    private GameObject CurrentAttackPos,CurrentSpellPos;
    //Animators used for the movements
    private Animator an;
    //Attack Script
    private PlayerAttack plA;
    public Vector2 dir;

    public CheckpointCheck cc;


    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        plA = GetComponent<PlayerAttack>();
        CurrentAttackPos = attackPosMeleeRight;
        cc = GameObject.FindGameObjectWithTag("CC").GetComponent<CheckpointCheck>();
        transform.position = cc.lastCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

        //Movement values for getting the Axis pressed
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        /*
		 * KEY CODES FOR MOVEMENTS, JUMPS AND GRABS
		 *		1) WALK METHOD
		 *		2) JUMPING
		 *		3) WALL JUMPING
		 *		4) WALL GRAB
		 *		5) DASH
		 */

        //Get the vector for the movement pressed and call the walk method
        dir = new Vector2(x, y);
        Walk(dir);
        //key code Better Jumping
        if (Input.GetKeyDown(KeyCode.W) && numJumps > 0 && !coll.onWall)
        {
            an.SetInteger("JumpNo", numJumps);
            Jump(Vector2.up);
            numJumps--;
        }
        // key code for wall jumping
        if (Input.GetKeyDown(KeyCode.W) && coll.onWall)
        {
            WallJump();

        }
        // key code for wall grab
        if (Input.GetKey(KeyCode.E) && coll.onWall)
        {
            //Debug.Log("WALL GRAB");
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        // key code for dash
        if (Input.GetKeyDown(KeyCode.Space) && hasDashed == false && !coll.onGround)
        {
            if (xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
            numJumps = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("Spell");
            InSpell();
        }
        /*
		 * 
		 * Velocity control for player falling in different scenarios 
		 *		
		 */
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        /*
		 * ANIMATIONS FOR PLAYER MOVEMENTS
		 *		1) WALL GRAB ANIMATION
		 *		2) JUMPS RESETS ANIMATIONS AND BOOLEAN CONTROLS
		 *		3) JUMPS ANIMATIONS
		 *		4) FALLING ANIMATION
		 *		
		 */

        //Animation for wall grab
        if (coll.onWall)
        {
            an.SetBool("WallGrab", true);
        }
        else
        {
            an.SetBool("WallGrab", false);
        }

        // jump reset animations/booleans
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
        //jumping animations for double and single jump
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
                an.SetBool("DoubleJump", true);
            }
            if (numJumps == 0)
            {
                an.SetBool("DoubleJump", false);
            }

        }
        // falling animation 
        if (!coll.onGround && rb.velocity.y < 0)
        {
            an.SetBool("Falling", true);
        }
        else
        {
            an.SetBool("Falling", false);
        }
        AttackCheck();
    }
    /*
	 * METHODS FOR PLAYER MOVEMENTS (OUTSIDE UPDATE METHOD)
	 *		1) DASH
	 *		2) JUMPING
	 *		3) WALK
	 *		4) WALL JUMPING
	 * 
	 */

    //private method for dash 
    private void Dash(float x, float y)
    {

        hasDashed = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
        SoundScript.PlaySound("dash");
    }
    //private method for jumping
    private void Jump(Vector2 dir)
    {

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpVelocity;
        SoundScript.PlaySound("jump");

    }
    // private method for walk 
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
                flipSpell(false);
            }
            else if (dir.x > 0)
            {
                spriteRenderer.flipX = false;
                flipAttack(true);
                flipSpell(false);
            }
        }

    }

    //private method for wall jumping
    private void WallJump()
    {

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));
        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
        Jump(Vector2.up + wallDir);
        wallJumped = true;
        SoundScript.PlaySound("jump");
    }
    private void InSpell()
    {
        GameObject f;
        an.SetTrigger("Spell");
        if (GetComponent<SpriteRenderer>().flipX)
        {
            f=Instantiate(fireball, spellPosLeft.GetComponent<Transform>().position, spellPosLeft.GetComponent<Transform>().rotation);
            f.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(!GetComponent<SpriteRenderer>().flipX)
        {
            f=Instantiate(fireball, spellPosRight.GetComponent<Transform>().position, spellPosRight.GetComponent<Transform>().rotation);
            f.GetComponent<SpriteRenderer>().flipX = false;
        }
        
        
    }


    /*
	 * IENUMERATORS FOR COUNTING TIME 
	 *		1) DISABLE MOVEMENT
	 *		2) DASH WAIT 
	 *		3) GROUND DASH (USED INSIDE DASH AND WAIT)
	 * 
	 */

    // IEnumerator for disable movement on the wallJumpo method
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    //IEnumarator for the private dash method
    IEnumerator DashWait()
    {
        GameObject.Find("Ghost").GetComponent<GhostTrail>().ShowGhost();
        //FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());

        rb.gravityScale = 0;

        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);


        rb.gravityScale = 3;

        wallJumped = false;
        isDashing = false;
    }
    //IEnumerator for the method IEnumerator in DashAndWait
    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);

    }

    /*
	 * AUXILIARY METHODS FOR THE SCRIPTS
	 *		1) FLIP ATTACK 
     *		2) FLIP SPELL 
	 *		3) GET CURRENT ATTACK POSITION
	 *		4) GET ATTACK POSITION
	 *		5) KEY CODE FOR ATTACK CKECK
	 * 
	 */

    //Auxiliary private method to determine the position in which the main player is facing
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
    }
    private void flipSpell(bool t)
    {
        if (t)
        {
            //Debug.Log("Right");
            CurrentSpellPos = spellPosRight;
            spellPos = true;
        }
        else if(!t)
        {
            //Debug.Log("Left");
            CurrentSpellPos = spellPosLeft;
            spellPos = false;
        }
    }
    //Auxiliary public method to get the current attack position 
    public GameObject GetCurrentAttackPos()
    {
        return CurrentAttackPos;
    }
    //Auxiliary public method to get the an attack position
    public bool GetAttackPos()
    {
        return attackPos;
    }

    //Auxiliary key code private method for the atack check
    private void AttackCheck()
    {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("PC attack");
            plA.Attack();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeadZone")
        {
            //Debug.Log("collision");
            Reload();
        }
    }
    public void Reload()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
    }


}
