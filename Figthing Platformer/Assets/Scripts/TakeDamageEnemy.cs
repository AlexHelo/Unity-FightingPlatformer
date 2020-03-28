using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageEnemy : MonoBehaviour
{
    private Animator an;
    // Start is called before the first frame update
    void Start()
    {
        an=GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        an.SetTrigger("Hurt");
    }
}
