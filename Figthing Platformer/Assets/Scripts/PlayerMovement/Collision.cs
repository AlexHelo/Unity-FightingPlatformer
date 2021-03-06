﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public LayerMask groundLayer;
    Renderer rend;

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    public Vector2 sizeX;
    public Vector2 sizeY;
    public Vector2 sizeOffsetY;
    public Vector2 sizeOffsetX;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        sizeOffsetY = new Vector2(0.1f, rend.bounds.size.y);
        sizeOffsetY.y -= 0.3f;
        sizeOffsetX = new Vector2(rend.bounds.size.x, 0.1f);
        sizeOffsetX.x -= 0.3f;
        sizeX = new Vector2(sizeOffsetX.x, 0.1f);
        sizeY = new Vector2(0.1f, sizeOffsetY.y);
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, sizeX, 0, groundLayer.value);
        onWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, sizeY, 0, groundLayer.value)
            || Physics2D.OverlapBox((Vector2)transform.position + leftOffset, sizeY, 0, groundLayer.value);

        onRightWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, sizeY, 0, groundLayer.value);
        onLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, sizeY, 0, groundLayer.value);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, sizeX);
        Gizmos.DrawWireCube((Vector2)transform.position + rightOffset, sizeY);
        Gizmos.DrawWireCube((Vector2)transform.position + leftOffset, sizeY);
    }
}
