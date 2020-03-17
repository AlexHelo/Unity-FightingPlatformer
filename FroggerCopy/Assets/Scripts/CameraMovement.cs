using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float offset = 0;

    // Update is called once per frame

    void Update()
    {
        Vector3 position = transform.position;
        position.y = (player.transform.position).y;
        transform.position = position;
    }

}
