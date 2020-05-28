using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageEnemy : MonoBehaviour
{
    private Animator an;
    private CurrenHealth cH;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        an=GetComponent<Animator>();
        cH = GetComponent<CurrenHealth>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage,GameObject enemy)
    {
        if (enemy.gameObject.name == "Fireball")
        {
            if (enemy.GetComponent<SpriteRenderer>().flipX && this.tag == "Normal")
            {
                //Debug.Log("Pop");
                rb.AddRelativeForce(new Vector2( 1/4*damage, damage));
            }
            else if (!enemy.GetComponent<SpriteRenderer>().flipX && this.tag == "Normal")
            {
                //Debug.Log("Kor");
                rb.AddRelativeForce(new Vector2(-1/4 * damage,  damage));
            }
            if (enemy.GetComponent<SpriteRenderer>().flipX && this.tag != "Normal")
            {
                //Debug.Log("Pop2");
                rb.AddRelativeForce(new Vector2(-1 / 4 * damage, damage));
            }
            else if (!enemy.GetComponent<SpriteRenderer>().flipX && this.tag != "Normal")
            {
                //Debug.Log("Kor2");
                rb.AddRelativeForce(new Vector2(1 / 4 * damage,  damage));
            }
        }
        else
        {

            if (enemy.GetComponent<SpriteRenderer>().flipX && this.tag == "Normal")
            {
                //Debug.Log("Pop");
                rb.AddRelativeForce(new Vector2(2f * damage, 1 * damage));
            }
            else if (!enemy.GetComponent<SpriteRenderer>().flipX && this.tag == "Normal")
            {
                //Debug.Log("Kor");
                rb.AddRelativeForce(new Vector2(-2f * damage, 1 * damage));
            }
            if (enemy.GetComponent<SpriteRenderer>().flipX && this.tag != "Normal")
            {
                //Debug.Log("Pop2");
                rb.AddRelativeForce(new Vector2(-2f * damage, 1 * damage));
            }
            else if (!enemy.GetComponent<SpriteRenderer>().flipX && this.tag != "Normal")
            {
                //Debug.Log("Kor2");
                rb.AddRelativeForce(new Vector2(2f * damage, 1 * damage));
            }
        }
        an.SetTrigger("Hurt");
        cH.CurrentHealthValue -= damage;
    }
}
