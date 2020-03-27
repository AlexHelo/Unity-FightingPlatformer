using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBtWAttack,timeResetAttack;
    public float startTimeBtwAttack;
    private bool Pressed = false;

    public Transform attackPos;
    public float attackRange,attackRange2,attackRange3;
    public LayerMask whatIsEnemies;

    private Animator an;
    float totalDuration;
    public float damage;
    private Rigidbody2D rigidbody;
    private int attackNum;

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

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
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
                attackNum += 1;
                
                if (attackNum == 1)
                {
                    an.SetInteger("AttackNo",1);
                    rigidbody.velocity = new Vector2(0f, 0f);
                    enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                    Vector3 displacement = new Vector3(-1.2f,0,0);
                    enemiesToDamage1= Physics2D.OverlapCircleAll(attackPos.position-displacement, attackRange-.5f, whatIsEnemies);
                }
                if (attackNum == 2)
                {
                    an.SetInteger("AttackNo", 2);
                    rigidbody.velocity = new Vector2(0f, 0f);
                    enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                    Vector3 displacement = new Vector3(-1.2f, 0, 0);
                    enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
                }
                if (attackNum == 3)
                {
                    an.SetInteger("AttackNo", 3);
                    rigidbody.velocity = new Vector2(0f, 0f);
                    enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                    Vector3 displacement = new Vector3(-1.2f, 0, 0);
                    enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos.position - displacement, attackRange - .5f, whatIsEnemies);
                }


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
    private void ResetCooldown()
    {

    }
}
