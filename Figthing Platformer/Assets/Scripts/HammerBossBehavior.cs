using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBossBehavior : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    public float damage, runspeed;
    private float speed;
    public GameObject player;
    private GameObject[] gm, gm1;
    public float maxDist, dashDistance;
    Transform playerTrans, BaseTrans;
    float currentTime,currentTimeDash;
    public float waitTime, secondsDash;
    private float dazedTime,attackNum,originalSpeed;
    public float startDazedTime;
    bool ataque;

    public float attackRange;
    public LayerMask whatIsEnemies;
    public Transform attackPosLeft, attackPosRight;
    private Transform currentAttackPos;
    private Animator animator;
    bool dead;
    CurrenHealth health;
    //MaxHealth maxHealth;
    public AnimationClip deadAnimation;
    public float mana;
    float deadAnimationTime;

    void Start()
    {
        gm = GameObject.FindGameObjectsWithTag("Player");
        player = gm[0];
        Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<CurrenHealth>();
        //maxHealth = GetComponent<MaxHealth>();
        currentAttackPos = attackPosLeft;
        playerTrans = player.GetComponent<Transform>();

        deadAnimationTime = deadAnimation.length;
        attackNum = 0;

        


    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            CheckDazedTime();
            playerTrans = player.GetComponent<Transform>();

            if (currentTime < waitTime)
            {
                currentTime += 1 * Time.deltaTime;
            }
            if (currentTime >= waitTime)
            {
                currentTime = 0;
            }

            if (currentTimeDash < 3.5f)
            {
                currentTimeDash += 1 * Time.deltaTime;
            }
            if (currentTimeDash >= 3.5f)
            {
                currentTimeDash = 0;
            }



            if (Vector3.Distance(transform.position, playerTrans.position) <= maxDist)
            {
                if (currentTime == 0)
                {
                    //Debug.Log(attackNum);
                    if (attackNum == 2)
                    {
                        GetComponent<SlimeController>().moving = false;
                        SpinningAttack();
                    }
                    else if(attackNum<2)
                    {
                        Atacar();
                    }

                }
                else if (currentTime <= 0)
                {
                    //GetComponent<SlimeController>().moving = false;
                }
                else
                {
                    GetComponent<SlimeController>().moving = true;
                }

            }
            if(Vector3.Distance(transform.position, playerTrans.position) <= dashDistance && 
                Vector3.Distance(transform.position, playerTrans.position) > maxDist)
            {
                if (currentTimeDash == 0)
                {
                    Dash();
                }
            }
        }
        catch (MissingReferenceException e)
        {

        }
    }
    public void CheckHealth()
    {
        if (GetComponent<CurrenHealth>().CurrentHealthValue <= 0)
        {
            if (!dead)
            {
                GetComponent<SlimeController>().moving = false;
                Destroy(this.gameObject, deadAnimationTime);
                animator.SetTrigger("Dead");
                animator.SetBool("Dead 0", true);
                dead = true;
            }
        }
    }
    public void Atacar()
    {
        if (!dead)
        {
            if (tag == "Normal")
            {
                if (GetComponent<SpriteRenderer>().flipX)
                {
                    currentAttackPos = attackPosLeft;
                }
                else
                {
                    currentAttackPos = attackPosRight;
                }
            }
            

            Rigidbody.velocity = new Vector2(0f, 0f);
            
            attackNum += 1;
            //if we are on the FIRST attack
            if (attackNum == 1)
            {
                animator.SetTrigger("Attack1");
            }
            else if (attackNum == 2)
            {
                animator.SetTrigger("Attack2");
                
            }

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<TakeDamagePlayer>().TakeDamage(damage, this.gameObject);
            }
            //ataque = true;
            //StartCoroutine(Wait(secondsDash));
            GetComponent<SlimeController>().moving = true;
            FunStartDazedTime();

        }
    }
    public void SpinningAttack()
    {
        if (!dead)
        {
            if (tag == "Normal")
            {
                if (GetComponent<SpriteRenderer>().flipX)
                {
                    currentAttackPos = attackPosLeft;
                }
                else
                {
                    currentAttackPos = attackPosRight;
                }
            }
            attackNum += 1;
            
            if (attackNum == 3)
            {
                animator.SetTrigger("Spinning");
                attackNum = 0;
            }
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosLeft.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<TakeDamagePlayer>().TakeDamage(damage, this.gameObject);
            }
            
            GetComponent<SlimeController>().moving = true;

        }
    }
    public void Dash()
    {
        originalSpeed = GetComponent<SlimeController>().moveSpeed;
        
        //transform.Translate(Vector3.right * originalSpeed * Time.deltaTime);
        GetComponent<SlimeController>().moveSpeed = originalSpeed * 3f;
        animator.SetTrigger("Dash");
        //StartCoroutine(Wait(secondsDash));
        GetComponent<SlimeController>().moveSpeed = originalSpeed;
    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentAttackPos.position, attackRange);
    }
	*/
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    public void CheckDazedTime()
    {
        if (dazedTime <= 0)
        {
            speed = runspeed;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
    }
    public void FunStartDazedTime()
    {
        dazedTime = startDazedTime;
    }
    private void FixedUpdate()
    {
        CheckHealth();
    }

}
