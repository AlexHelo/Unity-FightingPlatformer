
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    DistanceJoint2D rope;
    private Rigidbody2D rb;

    private LineRenderer lr;
    bool checker;
    public LayerMask whatIsGrappleable;

    public GameObject[] blocks;

    public PlayerController PlayerController;
    void Start()
    {


        blocks = GameObject.FindGameObjectsWithTag("Grapple");


        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Detect mouse position


        // Shot rope on mouse position
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();


        }

        // Destroy rope
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }

    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        Debug.Log("Chambing");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rb.position, mousePos, 10, whatIsGrappleable);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hito = Physics2D.GetRayIntersection(ray, Mathf.Infinity, whatIsGrappleable);

        GameObject closest = GetClosestObject(mousePos);


        if (closest == null) return;
        //if (!hito.collider)
        //{
        //    return;
        // }





        rope = gameObject.AddComponent<DistanceJoint2D>();
        rope.enableCollision = true;
        rope.connectedAnchor = closest.transform.position;
        // rope.distance = hit.distance;



        // rope.frequency = 1;
        //  rope.dampingRatio = 0.3f;

        lr.positionCount = 2;

    }

    void StopGrapple()
    {
        if (rope == null) return;
        lr.positionCount = 0;
        Destroy(rope);
    }

    void DrawRope()
    {
        if (!rope) return;
        PlayerController.rb.velocity = new Vector2(PlayerController.dir.x * 15, PlayerController.rb.velocity.y);
        lr.SetPosition(0, rope.connectedAnchor);
        lr.SetPosition(1, rb.position);
    }

    public GameObject GetClosestObject(Vector2 mousePos)
    {


        float closest = 5; //add your max range here
        GameObject closestObject = null;

        Debug.Log(blocks[0].transform.position);

        for (int i = 0; i < blocks.Length; i++)  //list of gameObjects to search through
        {

            float dist = Vector3.Distance(blocks[i].transform.position, mousePos);
            if (dist < closest)
            {
                closest = dist;
                closestObject = blocks[i];


            }
        }
        return closestObject;
    }
}
