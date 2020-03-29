using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageEnemy : MonoBehaviour
{
    private Animator an;
    private CurrenHealth cH;
    // Start is called before the first frame update
    void Start()
    {
        an=GetComponent<Animator>();
        cH = GetComponent<CurrenHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        an.SetTrigger("Hurt");
        cH.CurrentHealthValue -= damage;
    }
}
