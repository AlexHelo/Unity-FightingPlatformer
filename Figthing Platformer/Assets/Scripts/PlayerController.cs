using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;


    [Space]
    [Header("Stats")]
    public float moveSpeed = 5.0f;
    public float jumpVelocity = 10f;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 3f;
    public int numJumps = 2;

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
        Vector2 dir = new Vector2(x, y);
        Walk(dir);

        //Better Jumping
        if (Input.GetButtonDown("Jump") && numJumps > 0)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            numJumps = 2; 
        }
    }
}
