using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehaviour : MonoBehaviour
{
    public float checkGroundRange;
    public float triggerEnemyRange;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkGround();
    }
    bool isGrounded;
    void checkGround()
    {
        if (Physics.CheckSphere(transform.position, checkGroundRange, whatIsGround) && !isGrounded)
        {
            isGrounded = true;
            Collider[] c = Physics.OverlapSphere(transform.position, triggerEnemyRange, whatIsEnemy);
            for (int i = 0; i < c.Length; i++)
            {
                c[i].GetComponent<EnemyBehaviour>().trigger(gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkGroundRange);
        Gizmos.DrawWireSphere(transform.position, triggerEnemyRange);
    }
}
