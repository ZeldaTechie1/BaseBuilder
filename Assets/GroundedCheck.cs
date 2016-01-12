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
        gameObject.GetComponentInParent<PlayerController>().grounded = false;
    }

    void OnTriggerStay(Collider other)
    {
        gameObject.GetComponentInParent<PlayerController>().grounded = true;
        //Debug.Log(other.name);
    }
}
