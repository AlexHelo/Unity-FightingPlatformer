using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(10f, 0, 0) * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Wall")
        {
            Destroy(this.gameObject);
        }

    }
}
