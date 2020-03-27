using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
	Rigidbody2D rb;
	public SlimeCollision coll;
	SpriteRenderer spriteRenderer;

	public float moveSpeed;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

		if (coll.onLeftWall && spriteRenderer.flipX == true)
		{
			Debug.Log("cum1");
			moveSpeed = moveSpeed* -1;
			StartCoroutine(MoveWait(0.2f));
			spriteRenderer.flipX = false;
		}
		if(coll.onRightWall && spriteRenderer.flipX == false)
		{
			Debug.Log("cum2");
			
			moveSpeed = moveSpeed * -1;
			StartCoroutine(MoveWait(0.2f));
			spriteRenderer.flipX = true;
		}
		

	}
	IEnumerator MoveWait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
}
