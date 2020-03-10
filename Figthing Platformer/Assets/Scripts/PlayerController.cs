using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    public float jumpVelocity = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * -moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpVelocity;
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
}
