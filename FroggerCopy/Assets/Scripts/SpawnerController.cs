using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject objectToSpawn;


    public float secs = 2.0f;
    void Start()
    {
        InvokeRepeating("Respawn", 0.0f, secs);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Respawn()
    {
        Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1f);
        Instantiate(objectToSpawn, position, Quaternion.identity);
    }

}
