using UnityEngine;
using System.Collections;

public class GroundedCheck : MonoBehaviour {

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInParent<PlayerController>().grounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        //adds a delay of .2 seconds
        Invoke("unGround", 0.2f);
    }

    void OnTriggerStay(Collider other)
    {
        gameObject.GetComponentInParent<PlayerController>().grounded = true;

    }

    void unGround()
    {
        gameObject.GetComponentInParent<PlayerController>().grounded = false;
    }
}
