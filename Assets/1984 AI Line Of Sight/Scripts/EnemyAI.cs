using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the base class for enemies, to use it you need to create a new class and inherit from this.
/// </summary>
public abstract class EnemyAI : MonoBehaviour
{
    public enum State 
    {
        Idle,
        Patrol,
        Follow,
        Attack
    }

    [SerializeField]protected LineOfSight lineOfSight;

    public State currentState;
    
    // used for the following and patrolling.
    internal Vector3 lastKnownPos;
    internal float lastKnownTime;

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Idle:
                if (lineOfSight.visibleTargets.Count > 0) // I saw the player, Start following him 
                    currentState = State.Follow;
                else
                    DoIdle();
                break;
            case State.Patrol:
                if (lineOfSight.visibleTargets.Count > 0) // I saw the player, Start following him 
                    currentState = State.Follow;
                else if ((Time.time - lastKnownTime) > 2) // It has been 20 seconds since we saw the player, switching to idle.
                    currentState = State.Idle;
                else
                    DoPatrol();
                break;
            case State.Follow:
                if (lineOfSight.visibleTargets.Count == 0) // I lost the player, I'll start looking around...
                    currentState = State.Patrol;
                else
                {
                    if (Vector3.Distance(transform.position, lineOfSight.visibleTargets[0].position) < attackRange)
                        currentState = State.Attack;
                    else
                        DoFollow();
                }
                break;
            case State.Attack:
                if (lineOfSight.visibleTargets.Count == 0) // I lost the player, I'll start looking around...
                    currentState = State.Patrol;
                else
                {
                    if (Vector3.Distance(transform.position, lineOfSight.visibleTargets[0].position) < attackRange)
                        DoAttack();
                    else
                        currentState = State.Follow;
                }
                break;
        }
    }

    public float attackRange = 2;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // In your inherited class you have to implement this methods,
    // inside you can set Animator parameters and interact with the environment.
    internal abstract void DoPatrol();
    internal abstract void DoIdle();
    internal abstract void DoFollow();
    internal abstract void DoAttack();
}
