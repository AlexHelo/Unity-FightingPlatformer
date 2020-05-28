﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
	public float speed;

	private Transform target;
	private Animator an;


	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		an = GetComponent<Animator>();
	}

	void Update()
	{
        try
        {
            if (Vector2.Distance(transform.position, target.position) < 5f)
            {
                FollowPlayer();
            }
            else
            {
                GetComponent<SlimeController>().moving = true;
            }
        }catch(MissingReferenceException e) { }
			
	}
    private void FollowPlayer()
    {
        float slime = transform.position.x - target.transform.position.x;
        try {
            //Debug.Log(slime);
            if (gameObject.name!="Golem" && gameObject.name!="HammerBoss") {

                if (GetComponent<SpriteRenderer>().flipX)
                {
                    if (slime < 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, GetComponent<SlimeController>().moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        //Debug.Log("flip true else");
                    }

                }

                if (!GetComponent<SpriteRenderer>().flipX)
                {
                    if (slime > 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, -GetComponent<SlimeController>().moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        //Debug.Log("flip false else");
                    }

                }
            }
            else if (gameObject.name == "HammerBoss" || gameObject.name == "Golem")
            {
                //Debug.Log("G");
                if (!GetComponent<SpriteRenderer>().flipX)
                {
                    if (slime < 0)
                    {
                        //GetComponent<SlimeController>().moving=false;
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    }
                    else
                    {
                        //Debug.Log("flip true else");
                    }

                }

                else if (GetComponent<SpriteRenderer>().flipX)
                {
                    if (slime > 0)
                    {
                        //GetComponent<SlimeController>().moving = false;
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    }
                    else
                    {
                        //Debug.Log("flip false else");
                    }

                }

            }
        }
        catch (MissingReferenceException e)
        {

        }
	}
}
