using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float offset = 0;

    public Transform top;
    public Transform bottom;

    // Update is called once per frame

    void Update()
    {
        Vector3 position = transform.position;
        position.y = (player.transform.position).y;
        transform.position = position;

        if (transform.position.y > top.position.y)
            transform.position = new Vector3(transform.position.x, top.position.y, transform.position.z);

        if (transform.position.y < bottom.position.y)
            transform.position = new Vector3(top.position.x, bottom.position.y, transform.position.z);
    }

}
