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
    private PlayerController pC;
    private float force;
    void Start()
    {
        an = GetComponent<Animator>();
        //ParticleSystem parts = particles.GetComponent<ParticleSystem>();
        //totalDuration = parts.duration + parts.startLifetime;
        rigidbody = GetComponent<Rigidbody2D>();
        pC = GetComponent<PlayerController>();
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
       

    }
    private void FixedUpdate()
    {
        if (timeResetAttack <= 0)
        {
            ResetCooldown();
        }
        attackPos = pC.GetCurrentAttackPos().GetComponent<Transform>();
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
        //force = 0;
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
                        
                        force = 5f;

                        Vector3 displacement = new Vector3(-1.2f, 0, 0);
                        enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
                    }
                    if (attackNum == 2)
                    {
                        an.SetInteger("AttackNo", attackNum);
                        rigidbody.velocity = new Vector2(0f, 0f);
                        enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange2, whatIsEnemies);

                        force = 15f;

                        Vector3 displacement = new Vector3(-1.2f, 0, 0);
                        enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
                    }
                    if (attackNum == 3)
                    {
                        
                        an.SetInteger("AttackNo", attackNum);

                        rigidbody.velocity = new Vector2(0f, 0f);
                        enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange3, whatIsEnemies);

                        force = 22f;

                        Vector3 displacement = new Vector3(-1.2f, 0, 0);
                        enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
                        
                    }
                    
                    timeResetAttack = startTimeResetAttack;
                }








                if (enemiesToDamage != null)
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                       
                        CheckKnockBack(i,enemiesToDamage);
                        
                        enemiesToDamage[i].GetComponent<TakeDamageEnemy>().TakeDamage(damage);
                        if (attacked)
                        {

                            attacked = false;
                        }
                        
                    }
                }
                
                if (enemiesToDamage1 != null)
                {
                    for (int i = 0; i < enemiesToDamage1.Length; i++)
                    {
                        CheckKnockBack(i, enemiesToDamage1);
                        enemiesToDamage1[i].GetComponent<TakeDamageEnemy>().TakeDamage(damage);
                        if (attacked)
                        {
                            
                            attacked = false;
                        }
                        
                    }
                }
                if (enemiesToDamage2 != null)
                {
                    for (int i = 0; i < enemiesToDamage2.Length; i++)
                    {
                        CheckKnockBack(i, enemiesToDamage2);
                        enemiesToDamage2[i].GetComponent<TakeDamageEnemy>().TakeDamage(damage);
                        if (attacked)
                        {
                            
                            attacked = false;
                        }
                        
                    }
                }
                
                attacked = true;

            }
            timeBtWAttack = startTimeBtwAttack;
            
        }

    }
    IEnumerator AttackWait(float seconds)
    {
        Debug.Log("running");
        yield return new WaitForSeconds(seconds);

    }
    private void CheckKnockBack(int i, Collider2D[] enemiesToDamage)
    {
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
            //enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 2) * force);
        }
        
    }
    
}
