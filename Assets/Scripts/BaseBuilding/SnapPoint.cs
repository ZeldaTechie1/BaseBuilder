﻿using UnityEngine;
using System.Collections;

public class SnapPoint : MonoBehaviour {

    public bool snap;
    public Vector3 snapPos;
    public GameObject parent;
    public GameObject placeholder;


    void Start()
    {
        parent = this.transform.parent.gameObject;
    }
    void Update()
    {
        if (parent.GetComponent<BuildObject>().Placeholder != null)
            placeholder = parent.GetComponent<BuildObject>().Placeholder;
       if(placeholder != null)
        {
            if(placeholder.tag == "Placeholder" && Vector3.Distance(parent.transform.position, placeholder.transform.position) > GetComponent<SphereCollider>().radius && snap)
            {
                StopSnap();
                Debug.Log("Poop");
            }
        }
    }

	void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "SnapPoint" && !parent.GetComponent<BuildObject>().snap)
        {
            if(parent != null)
            {
                if (!parent.GetComponent<BuildObject>().isPlaced)
                {
                    if (col.transform.parent.GetComponent<BuildObject>().isPlaced && !parent.GetComponent<BuildObject>().isPlaced)
                    {
                        Vector3 distanceTo = col.transform.position;
                        snapPos = distanceTo;
                        Snap(distanceTo, this.transform.position);
                    }
                    else
                    {
                        Debug.LogError("This object isn't placed, please go away." + parent.name);
                    }
                }
            }
            else
            {
                Debug.LogError("This SnapPoint has no parent!");
            }
        }
    }

    void Snap(Vector3 direction, Vector3 position)
    {
        StartSnap();
        parent.GetComponent<BuildObject>().Snap(direction, position);
    }
    public void StartSnap()//moves the object towards the correct place to snap
    {
        snap = true;
        parent.GetComponent<BuildObject>().snap = true;
    }
    public void StopSnap()//moves the object towards the correct place to snap
    {
        snap = false;
        parent.GetComponent<BuildObject>().snap = false;
    }
}
