using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isGrounded;

    // Update is called once per frame


    private void OnTriggerStay2D(Collider2D collider)
    {
        isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        isGrounded = false;
    }

}
