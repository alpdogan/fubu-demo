using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RedBox2DAI : EnemyAI
{
    private Rigidbody2D rb;
    public float movementSpeed = 3f;
    public Material seenMaterial, unseenMaterial;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector3 lastMove;

    internal override void DoPatrol()
    {
        if (Vector3.Distance(transform.position, lastKnownPos) > 0.5)
        {
            MoveTo(lastKnownPos);
            // Debug.Log((transform.position - lastMove).magnitude);
            // Debug.Log(Vector3.right*transform.localScale.x);
            if ((transform.position - lastMove).magnitude < 0.0001) // We hit a wall
            {
                lastKnownPos = transform.position - Vector3.right*transform.localScale.x*movementSpeed - Vector3.up; // turn back
                transform.localScale = new Vector3(transform.localScale.x*-1,1,1);
            }
        }
        else
        {
            // Reached last known position, trying random walk.
            lineOfSight.SetMaterial(unseenMaterial);
            lastKnownPos = transform.position + new Vector3(Random.Range(-5, 5), 0,0);
        }

        lastMove = rb.position;
    }

    internal override void DoIdle()
    {
        lineOfSight.SetMaterial(unseenMaterial);
        // You can play animations if you want.
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(lastKnownPos, 0.4f);
    }

    internal override void DoFollow()
    {
        if (lineOfSight.visibleTargets.Count == 0)
            return;
        lineOfSight.SetMaterial(seenMaterial);
        MoveTo(lineOfSight.visibleTargets[0].position); // move towards target
        lastKnownPos = lineOfSight.visibleTargets[0].position;
        lastKnownTime = Time.time;
    }

    internal override void DoAttack()
    {
        Debug.Log("Pew Pew!");
    }

    private bool canJump = true;
    void MoveTo(Vector3 pos)
    {
        Vector2 delta = (pos - transform.position).normalized;
        // rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(delta),
        //     Time.fixedDeltaTime * movementSpeed));
        rb.velocity = new Vector2(delta.x * movementSpeed, rb.velocity.y);
        if (delta.y > 0 && rb.velocity.y < 1 && canJump)
        {
            canJump = false;
            rb.AddForce(Vector2.up * (100 * 4));
        }

        if (delta.x < 0)
            transform.localScale = new Vector3(-1,1,1);
        else
            transform.localScale = new Vector3(1,1,1);

        // rb.MovePosition(Vector3.Lerp(rb.position, rb.position + delta, Time.fixedDeltaTime * movementSpeed));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        canJump = true;
    }
}