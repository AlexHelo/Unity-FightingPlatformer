using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    public SlimeCollision coll;
    SpriteRenderer spriteRenderer;

    public float moveSpeed;
    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = true;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (GetComponent<Transform>().position.y < 18)
            {
                //Debug.Log("Hola");
                //rb.velocity = Vector3.up * moveSpeed  * Time.deltaTime;
                transform.Translate(Vector3.up * moveSpeed/ 90 * Time.deltaTime);
            }
            else if (GetComponent<Transform>().position.y > 18)
            {
                //rb.velocity = Vector3.down * moveSpeed / 3 * Time.deltaTime;
                transform.Translate(Vector3.down * moveSpeed/ 90 * Time.deltaTime);
            }
            rb.velocity = Vector3.right * moveSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = false;
            //transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        


    }
    
}
