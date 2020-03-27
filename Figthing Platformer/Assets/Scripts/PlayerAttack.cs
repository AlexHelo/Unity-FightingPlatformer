using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBtWAttack,timeResetAttack;
    public float startTimeBtwAttack,startTimeResetAttack;
    private bool Pressed = false;

    public Transform attackPos;
    public float attackRange,attackRange2,attackRange3;
    public LayerMask whatIsEnemies;

    private Animator an;
    float totalDuration;
    public float damage;
    private Rigidbody2D rigidbody;
    private int attackNum;
    private bool startTimer=false;

    void Start()
    {
        an = GetComponent<Animator>();
        //ParticleSystem parts = particles.GetComponent<ParticleSystem>();
        //totalDuration = parts.duration + parts.startLifetime;
        rigidbody = GetComponent<Rigidbody2D>();
        
    }


    void Update()
    {

        if (timeBtWAttack >= 0)
        {
            timeBtWAttack -= Time.deltaTime;
        }
        if (timeResetAttack >= 0  && startTimer)
        {
            timeResetAttack -= Time.deltaTime;
        }
        Debug.Log(timeResetAttack);

    }
    private void FixedUpdate()
    {
        if (timeResetAttack <= 0)
        {
            ResetCooldown();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private void ResetCooldown()
    {
        startTimer = true;
        attackNum = 0;
        an.SetInteger("AttackNo", attackNum);
    }
    public void Attack()
    {
        Pressed = true;
        bool attacked = true;
        Collider2D[] enemiesToDamage= null;
        Collider2D[] enemiesToDamage1=null;
        Collider2D[] enemiesToDamage2 = null;

        if (timeBtWAttack <= 0)
        {
            if (Pressed)
            {
                Pressed = false;
                an.SetBool("Attacking", true);
                
                if (timeResetAttack <= startTimeResetAttack)
                {
                    attackNum += 1;

                    if (attackNum == 1)
                    {
                        an.SetInteger("AttackNo", attackNum);
                        rigidbody.velocity = new Vector2(0f, 0f);
                        enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                        Vector3 displacement = new Vector3(-1.2f, 0, 0);
                        enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
                    }
                    if (attackNum == 2)
                    {
                        an.SetInteger("AttackNo", attackNum);
                        rigidbody.velocity = new Vector2(0f, 0f);
                        enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                        Vector3 displacement = new Vector3(-1.2f, 0, 0);
                        enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
                    }
                    if (attackNum == 3)
                    {
                        an.SetInteger("AttackNo", attackNum);

                        rigidbody.velocity = new Vector2(0f, 0f);
                        enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                        Vector3 displacement = new Vector3(-1.2f, 0, 0);
                        enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
                        
                    }
                    
                    timeResetAttack = startTimeResetAttack;
                }
                /*else
                {
                    ResetCooldown();
                }*/

                if (enemiesToDamage != null)
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {

                        //enemiesToDamage[i].GetComponent<TakeDamageEnemy>().TakeDamage(damage);
                        if (attacked)
                        {
                            //StartCoroutine(shake.Start());
                            attacked = false;
                        }
                        //GameObject g = Instantiate(particles, particlePos.position, particlePos.rotation) as GameObject;
                        //Destroy(g, totalDuration);
                    }
                }
                
                if (enemiesToDamage1 != null)
                {
                    for (int i = 0; i < enemiesToDamage1.Length; i++)
                    {

                        //enemiesToDamage[i].GetComponent<TakeDamageEnemy>().TakeDamage(damage);
                        if (attacked)
                        {
                            //StartCoroutine(shake.Start());
                            attacked = false;
                        }
                        //GameObject g = Instantiate(particles, particlePos.position, particlePos.rotation) as GameObject;
                        //Destroy(g, totalDuration);
                    }
                }
                if (enemiesToDamage2 != null)
                {
                    for (int i = 0; i < enemiesToDamage2.Length; i++)
                    {

                        //enemiesToDamage[i].GetComponent<TakeDamageEnemy>().TakeDamage(damage);
                        if (attacked)
                        {
                            //StartCoroutine(shake.Start());
                            attacked = false;
                        }
                        //GameObject g = Instantiate(particles, particlePos.position, particlePos.rotation) as GameObject;
                        //Destroy(g, totalDuration);
                    }
                }
                
                attacked = true;

            }
            timeBtWAttack = startTimeBtwAttack;
            
        }

    }
    
}
