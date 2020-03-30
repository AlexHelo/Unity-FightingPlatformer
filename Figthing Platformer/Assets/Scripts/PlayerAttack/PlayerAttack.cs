using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * GLOSARY:
 *		1) VALUES
 *		2) START METHOD
 *		3) UPDATE METHOD
 *		4) FIXED UPDATE
 *		5) ATTACK METHOD
 *		6) IENUMERATOR
 *		7) AUXILIARY METHODS
 *		8) GRIZMOS
 * 
 */ 
public class PlayerAttack : MonoBehaviour
{
	public Transform attackPos;
	public LayerMask whatIsEnemies;

	private Rigidbody2D rigidbody;

	private float timeBtWAttack,timeResetAttack;
	private float force;

	public float startTimeBtwAttack,startTimeResetAttack;
	public float attackRange, attackRange2, attackRange3;
	public float damage;

	float totalDuration;

	private bool Pressed = false;
	private bool startTimer = false;

	private int attackNum;
    
	private Animator an;
	private PlayerController pC;
	
	void Start()
    {
        an = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        pC = GetComponent<PlayerController>();
    }
	/*
	 *	UPDATE METHOD
	 *		1) TIME BETWEEN ATTACKS
	 */ 
	
    void Update()
    {
		// Check to substract time between attacks
        if (timeBtWAttack >= 0)
        {
            timeBtWAttack -= Time.deltaTime;
        }
        if (timeResetAttack >= 0  && startTimer)
        {
            timeResetAttack -= Time.deltaTime;
        }
       
    }
	/*
	 * FIXED UPDATE METHDOD
	 *		1) RESET COMBO LOSS
	 *		2) DETERMINE ATTACK POSITION
	 */ 
    private void FixedUpdate()
    {
		//check to reset cooldown attack if combo is loss 
        if (timeResetAttack <= 0)
        {
            ResetCooldown();
        }
		//Establish the attacking point
        attackPos = pC.GetCurrentAttackPos().GetComponent<Transform>();
    }

	/*
	 * ATTACK METHOD 
	 *		1) VALUES
	 *		2) CAN ATTACK CONDITION
	 *		3) PRESS KEY CODE CONDITION
	 *		4) CONTINUE COMBO CONDITION
	 *			A) FIRST ATTACK
	 *			B) SECOND ATTACK
	 *			C) THIRD ATTACK
	 *		5)HITTING ENEMY
	 *			A) FIRST COLLIDER
	 *			B) SECOND COLLIDER
	 *			C) THIRD COLLIDER
	 *			
	 */ 
	
    public void Attack()
    {
		//Values for the attack method
        Pressed = true;
        bool attacked = true;
        Collider2D[] enemiesToDamage= null;
        Collider2D[] enemiesToDamage1=null;
        Collider2D[] enemiesToDamage2 = null;
		//  if you can attack
        if (timeBtWAttack <= 0)
        {
			//if you pressed the key code to attack 
            if (Pressed)
            {
                Pressed = false;
                an.SetBool("Attacking", true);
                //if you can continue the combo
                if (timeResetAttack <= startTimeResetAttack)
                {
                    attackNum += 1;
					//if we are on the FIRST attack
                    if (attackNum == 1)
                    {
						enemiesToDamage = AttackNumber(enemiesToDamage, enemiesToDamage1, -5f, -1.2f,0f,attackRange);
						Debug.Log("attack 1");
					}
					//if we are on the SECOND attack
                    if (attackNum == 2)
                    {
						Debug.Log("attack 2");
						enemiesToDamage1 = AttackNumber(enemiesToDamage, enemiesToDamage1, 15f, -1.2f, 0f,attackRange2);
					}
					//if we are on the THIRD attack
                    if (attackNum == 3)
                    {
						Debug.Log("attack 3");
						enemiesToDamage2 = AttackNumber(enemiesToDamage, enemiesToDamage1, 22f, -1.2f, 0f,attackRange3);

					}
                    
                    timeResetAttack = startTimeResetAttack;
                }
				//if you are hitting an enemy
				if (enemiesToDamage != null)
				{
					Debug.Log("Hitbox 1");
					EnemyHitBox(enemiesToDamage, attacked);
				} else
				{
					Debug.Log("No hitbox 1");
				}
				//same as the above, with differet hitbox
				if (enemiesToDamage1 != null)
				{
					Debug.Log("Hitbox 2");
					EnemyHitBox(enemiesToDamage1, attacked);
				}
				//same as above with different hitbox
				if (enemiesToDamage2 != null)
                {
					Debug.Log("Hitbox 3");
					EnemyHitBox(enemiesToDamage2, attacked);
                }                
                attacked = true;
            }
            timeBtWAttack = startTimeBtwAttack;   
        }

    }
	/*
	 * IENUMERATOR 
	 *		1) TIME BETWEEN ATTACKS
	 */ 


	//IEnumerator to determine the time between attacks
    IEnumerator AttackWait(float seconds)
    {
        Debug.Log("running");
        yield return new WaitForSeconds(seconds);

    }

	/*
	 * AUXILIARY METHODS
	 *		1) ATTACK COOLDOWN
	 *		2) KNOCKBACK
	 *		3) CHOOSE COLLIDER
	 * 
	 */ 


	// Auxiliary attack cooldown reset
	private void ResetCooldown()
	{
		startTimer = true;
		attackNum = 0;
		//force = 0;
		an.SetInteger("AttackNo", attackNum);
	}

	//Auxiliary private method to apply knockback
	private void CheckKnockBack(int i, Collider2D[] enemiesToDamage)
    {
		Debug.Log("Enemy name "+ enemiesToDamage[i].gameObject.name);
        Debug.Log(force);
        if (pC.GetAttackPos())
        {
            Debug.Log("Derecha");
            enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 2)*  force);
        }
        else
        {
            Debug.Log("Izquierda");
            enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 2) * force);
        }
        
    }

	//Auxiliary method to determine the collider 
	private void EnemyHitBox(Collider2D[] enemiesToDamage, bool attacked)
	{
		// check every enemy hit
		for (int i = 0; i < enemiesToDamage.Length; i++)
		{
			//Apply knockback
			CheckKnockBack(i, enemiesToDamage);
			enemiesToDamage[i].GetComponent<TakeDamageEnemy>().TakeDamage(damage);
			// if you attack, dont apply again
			if (attacked)
			{
				attacked = false;
			}

		}
	}
	//Auxiliary method for number of attack
	private Collider2D[] AttackNumber(Collider2D[] enemiesToDamage, Collider2D[] enemiesToDamage1, float applyForce, float xDis, float rbVelocity, float att)
	{
		an.SetInteger("AttackNo", attackNum);
		rigidbody.velocity = new Vector2(rbVelocity, 0f);
		enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, att, whatIsEnemies);

		force = applyForce;

		Vector3 displacement = new Vector3(xDis,0, 0);
		enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
		return enemiesToDamage;
	}

	/*
	 * GRIZMOS DRAWING
	 */ 

	//Drawing private method hit box for the attack
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPos.position, attackRange);
	}

}
