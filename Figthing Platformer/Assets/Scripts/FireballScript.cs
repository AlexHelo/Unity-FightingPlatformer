using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private SpriteRenderer sp;
    private Rigidbody2D rb;
    public float speed,damage;
    public LayerMask enemies;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (sp.flipX)
        {
            rb.velocity = Vector2.left * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.right * speed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            //Debug.Log("Cum");
            collision.gameObject.GetComponent<TakeDamageEnemy>().TakeDamage(damage,this.gameObject);
            
        }
        Destroy(this.gameObject);
    }

}
