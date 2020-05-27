using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
	Rigidbody2D rb;
	public SlimeCollision coll;
	SpriteRenderer spriteRenderer;

	public float moveSpeed;
    public bool moving;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            //rb.velocity = Vector3.right * moveSpeed * Time.deltaTime;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

		if ((coll.onLeftWall && spriteRenderer.flipX == false) || (!coll.onGroundL && spriteRenderer.flipX == false))
		{
			moveSpeed = moveSpeed* -1;
			StartCoroutine(MoveWait(0.2f));
			spriteRenderer.flipX = true;
		}
		else if((coll.onRightWall && spriteRenderer.flipX == true) || (!coll.onGroundR && spriteRenderer.flipX == true))
		{
			moveSpeed = moveSpeed * -1;
			StartCoroutine(MoveWait(0.2f));
			spriteRenderer.flipX = false;
		}
		

	}
	IEnumerator MoveWait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
}
