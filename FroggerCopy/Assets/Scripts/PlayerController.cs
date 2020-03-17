using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 8.0f;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && transform.position == pos)
        {        // Left
            pos += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {        // Right
            pos += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {        // Up
            pos += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {        // Down
            pos += Vector3.down;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        this.transform.position = new Vector3(0f, -4.5f, 0f);
        pos = new Vector3(0f, -4.5f, 0f);
    }


}
