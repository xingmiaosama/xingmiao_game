using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isTouchingGround;
    private PlayerData playerData;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerData = GetComponent<PlayerData>();
        rb.gravityScale = playerData.gravityScale;
    }

    // Update is called once per frame

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, playerData.jumpForce);
        }
        
    }

    private void FixedUpdate()
    {
        if (isTouchingGround)
        {
            rb.velocity = new Vector2(horizontalInput * playerData.moveSpeed * playerData.momentum, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * playerData.moveSpeed, rb.velocity.y);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //other.collider.enabled = true;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        ContactPoint2D contact = other.contacts[0];
        if (contact.normal == new Vector2(1f, 0f) || contact.normal == new Vector2(-1f, 0f))
        {
            isTouchingGround = true;
        }
        else
        {
            isTouchingGround = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //other.collider.enabled = true;
    }
}
