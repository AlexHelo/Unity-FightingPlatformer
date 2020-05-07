using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float distanceToMove = 1f;
    [SerializeField] private float moveSpeed = 10f;
    private bool moveToPoint = false;
    private Vector3 endPosition;
     Vector3 originalPos;
  void Start () {
        endPosition = transform.position;
        originalPos = new Vector3(-0.44f, -3.53f, 0f);
    }
  
  void FixedUpdate () {
        if (moveToPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
        }
  }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) //Left
        {
            endPosition = new Vector3(endPosition.x - distanceToMove, endPosition.y, endPosition.z);
            moveToPoint = true;
        }
        if (Input.GetKeyDown(KeyCode.D)) //Right
        {
            endPosition = new Vector3(endPosition.x + distanceToMove, endPosition.y, endPosition.z);
            moveToPoint = true;
        }
        if (Input.GetKeyDown(KeyCode.W)) //Up
        {
            endPosition = new Vector3(endPosition.x, endPosition.y + distanceToMove, endPosition.z);
            moveToPoint = true;
        }
        if (Input.GetKeyDown(KeyCode.S)) //Down
        {
            endPosition = new Vector3(endPosition.x, endPosition.y - distanceToMove, endPosition.z);
            moveToPoint = true;
        }
    }

     void OnCollisionEnter2D(Collision2D other)
  {
      if (other.gameObject.CompareTag("Tank")) {
            gameObject.transform.position = originalPos;
            endPosition = new Vector3(-0.44f, -3.53f, 0f);
    	}
    }
         
          
    
  }

