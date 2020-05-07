using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTankController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
         gameObject.transform.Translate(new Vector3(0,0.03f,0));

    }

    void OnCollisionEnter2D(Collision2D col)
    {
       
       Destroy(gameObject);
    }
}
