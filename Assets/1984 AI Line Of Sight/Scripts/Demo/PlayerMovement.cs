using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed = 4;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveTo(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
    }
    
    void MoveTo(Vector3 pos)
    {
        rb.MovePosition(Vector3.Lerp(rb.position, rb.position + pos, Time.fixedDeltaTime * movementSpeed));
    }
}
