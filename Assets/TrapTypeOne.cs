using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTypeOne : MonoBehaviour, ITrapTrigger
{
    public Rigidbody trapRb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void trigger()
    {
        trapRb.isKinematic = false;
    }
}
