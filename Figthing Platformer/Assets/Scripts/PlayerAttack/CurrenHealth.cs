using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrenHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject healthBarSprite;
    [SerializeField]
    private float currenTHealth;
    [SerializeField]
    private Animator an;
    [SerializeField]
    private float deadTime;
    private Vector3 scale;
    private bool dead;
    void Start()
    {
        an = GetComponent<Animator>();
        dead = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        scale = new Vector3(currenTHealth / 100, 1, 1);
        healthBar.GetComponent<Transform>().localScale = scale;
        if (currenTHealth < 30)
        {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.red;
            
        }
        else if(currenTHealth<75)
        {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            healthBarSprite.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (currenTHealth <=0)
        {
            currenTHealth = 0;
        }
        CheckHealth();
    }
    private void CheckHealth()
    {
        if (!dead)
        {
            if (currenTHealth <= 0)
            {
                dead=true;
                an.SetTrigger("Dead");
                
                Destroy(this.gameObject,deadTime);
            }
        }
    }
    IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        
    }
    public float CurrentHealthValue
    {
        get => currenTHealth;
        set
        {
            currenTHealth = value;
            
        }
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "DeadZone")
		{
			currenTHealth = 0;
			CheckHealth();
		}
	}
}
