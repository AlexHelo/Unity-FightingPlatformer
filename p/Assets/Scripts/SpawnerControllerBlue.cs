using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControllerBlue : MonoBehaviour
{
   
	public GameObject objectToSpawn;



    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("Respawn", 0.0f, 2.5f);

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
