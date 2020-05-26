using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamagePlayer : MonoBehaviour
{
	private Animator an;
	private CurrenHealth cH;
	private Rigidbody2D rb;
	// Start is called before the first frame update
	void Start()
    {
		an = GetComponent<Animator>();
		cH = GetComponent<CurrenHealth>();
		rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	public void TakeDamage(float damage)
	{
		//an.SetTrigger("Hurt");
		rb.AddForce(new Vector2(200f*damage,100.5f*damage));
		cH.CurrentHealthValue -= damage;
	}
	public void TakeDamage(float damage,GameObject enemy)
	{
		if (enemy.GetComponent<SpriteRenderer>().flipX && enemy.tag!="Normal")
		{
            //Debug.Log("Pop");
            rb.AddRelativeForce(new Vector2(150f * damage, 80.5f * damage));
		}
		else if(!enemy.GetComponent<SpriteRenderer>().flipX && enemy.tag != "Normal") 
        { 
            //Debug.Log("Kor");
            rb.AddRelativeForce(new Vector2(-150f * damage, 80.5f * damage));
		}


        if (!enemy.GetComponent<SpriteRenderer>().flipX && enemy.tag == "Normal")
        {
            //Debug.Log("Hey");
            rb.AddRelativeForce(new Vector2(60 * damage, damage*20));
        }
        else if(enemy.GetComponent<SpriteRenderer>().flipX && enemy.tag == "Normal")
        {
            //Debug.Log("Heya");
            rb.AddRelativeForce(new Vector2(60 * -damage, damage * 20));
        }
        cH.CurrentHealthValue -= damage;
	}
}
