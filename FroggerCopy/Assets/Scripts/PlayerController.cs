using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 8.0f;
    Vector3 pos;
    Vector3 oldpos;
    void Start()
    {
        pos = transform.position;
        oldpos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && transform.position == pos)
        {        // Left
            oldpos = transform.position;
            pos += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {        // Right
            oldpos = transform.position;
            pos += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {        // Up
            oldpos = transform.position;
            pos += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {        // Down
            oldpos = transform.position;
            pos += Vector3.down;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            this.transform.position = new Vector3(0f, -4.5f, 0f);
            pos = new Vector3(0f, -4.5f, 0f);
        }

        if (col.gameObject.tag == "Wall")
        {
            pos = oldpos;
        }
    }


}
