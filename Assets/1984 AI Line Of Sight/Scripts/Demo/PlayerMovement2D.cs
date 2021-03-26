using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed = 4;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*movementSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddForce(Vector2.up * (100 * movementSpeed));
        }
    }
    private bool canJump = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        canJump = true;
    }
}