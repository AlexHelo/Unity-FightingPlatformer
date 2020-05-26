using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBehavior : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    public float damage, runspeed;
    private float speed;
    public GameObject player;
    private GameObject[] gm, gm1;
    public float maxDist;
    Transform playerTrans, BaseTrans;
    float currentTime;
    public float waitTime;
    private float dazedTime;
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
            if (Vector3.Distance(transform.position, playerTrans.position) <= maxDist)
            {
                if (currentTime == 0)
                {
                    Atacar();
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
                GetComponent<FlyingEnemyController>().moving = false;

                Destroy(this.gameObject, deadAnimationTime);
                animator.SetTrigger("Dead");
                dead = true;
            }
        }
    }
    public void Atacar()
    {
        if (!GetComponent<SpriteRenderer>().flipX)
        {
            currentAttackPos = attackPosRight;
        }
        else
        {
            currentAttackPos = attackPosLeft;
        }

        Rigidbody.velocity = new Vector2(0f, 0f);
        animator.SetTrigger("Attacking");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<TakeDamagePlayer>().TakeDamage(damage, this.gameObject);
        }
        //ataque = true;
        FunStartDazedTime();

    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentAttackPos.position, attackRange);
    }
	*/
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
