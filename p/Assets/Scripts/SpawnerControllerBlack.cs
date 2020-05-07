using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControllerBlack : MonoBehaviour
{
     
	public GameObject objectToSpawn;



    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("Respawn", 0.0f, 3f);

    }

    // Update is called once per frame
    void Update()
    {


    }

    void Respawn(){

    	Vector3 position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y ,  gameObject.transform.position.z);
    	Instantiate(objectToSpawn, position,  Quaternion.Euler(0, 0, -90));

    }
}
