using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private CheckpointCheck cc;

    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("CC").GetComponent<CheckpointCheck>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cc.lastCheckpoint = transform.position;
        }
    }
}
