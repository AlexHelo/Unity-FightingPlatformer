using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyFollow : MonoBehaviour
{
    public float speed;

    private Transform target;
    private Animator an;
    public float maxDist,minDist;
    private Rigidbody2D rb;
    public float speedFollow;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        try
        {
            if (Vector2.Distance(transform.position, target.position) <= maxDist && Vector2.Distance(transform.position, target.position) >= minDist)
            {
                FollowPlayer();
                GetComponent<FlyingEnemyController>().moving = false;
            }
            else
            {
                GetComponent<FlyingEnemyController>().moving = true;
            }
        }
        catch (MissingReferenceException e) { }

    }
    private void FollowPlayer()
    {
        float slime = transform.position.x - target.transform.position.x;
        try
        {

            if (!GetComponent<SpriteRenderer>().flipX)
            {
                if (slime < 0)
                {
                    //Debug.Log("pp");
                    rb.position = Vector2.MoveTowards(transform.position, target.position, speedFollow * Time.deltaTime);
                    //transform.position = Vector2.MoveTowards(transform.position, target.position, GetComponent<FlyingEnemyController>().moveSpeed * Time.deltaTime);

                }
                else if (slime > 0)
                {
                    //Debug.Log("ii");
                    rb.position = Vector2.MoveTowards(transform.position, target.position, speedFollow * Time.deltaTime);
                    //transform.position = Vector2.MoveTowards(transform.position, target.position, GetComponent<FlyingEnemyController>().moveSpeed * Time.deltaTime);
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }

            

            if (GetComponent<SpriteRenderer>().flipX)
            {
                if (slime < 0)
                {
                    //Debug.Log("kk");
                    rb.position = Vector2.MoveTowards(transform.position, target.position, speedFollow * Time.deltaTime);
                    GetComponent<SpriteRenderer>().flipX = false;
                    //rb.velocity = Vector2.Lerp(transform.position, target.position, -GetComponent<FlyingEnemyController>().moveSpeed * Time.deltaTime);
                    //transform.position = Vector2.MoveTowards(transform.position, target.position, GetComponent<FlyingEnemyController>().moveSpeed * Time.deltaTime);
                }
                else if (slime > 0)
                {
                    //Debug.Log("ll");
                    rb.position = Vector2.MoveTowards(transform.position, target.position, speedFollow * Time.deltaTime);
                    //transform.position = Vector2.MoveTowards(transform.position, target.position, GetComponent<FlyingEnemyController>().moveSpeed * Time.deltaTime);
                    
                }

            }
        }
        catch (MissingReferenceException e)
        {

        }
    }
}
