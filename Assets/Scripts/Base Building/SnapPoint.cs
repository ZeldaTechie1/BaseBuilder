using UnityEngine;
using System.Collections;

public class SnapPoint : MonoBehaviour {

    public bool snap;
    public Vector3 snapPos;

    void Update()
    {
       if(snap)
        {
            Snap(snapPos, this.transform.position);
        }
    }

	void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "SnapPoint")
        {
            if(transform.parent != null)
            {
                if (!transform.parent.GetComponent<BuildObject>().isPlaced)
                {
                    if (col.transform.parent.GetComponent<BuildObject>().isPlaced && !transform.parent.GetComponent<BuildObject>().isPlaced)
                    {
                        Vector3 distanceTo = col.transform.position;
                        snapPos = distanceTo;
                        Snap(distanceTo, this.transform.position);
                    }
                    else
                    {
                        Debug.LogError("This object isn't placed, please go away." + transform.parent.name);
                    }
                }
            }
            else
            {
                Debug.LogError("This SnapPoint has no parent!");
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "SnapPoint")
        {
            if (transform.parent != null)
            {
                if (!transform.parent.GetComponent<BuildObject>().isPlaced)
                {
                    if (col.transform.parent.GetComponent<BuildObject>().isPlaced && !transform.parent.GetComponent<BuildObject>().isPlaced)
                    {
                        StopSnap();
                    }
                    else
                    {
                        Debug.LogError("This object isn't placed, please go away." + transform.parent.name);
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
        transform.parent.GetComponent<BuildObject>().Snap(direction, position);
    }
    public void StartSnap()//moves the object towards the correct place to snap
    {
        snap = true;
    }
    public void StopSnap()//moves the object towards the correct place to snap
    {
        snap = false;
    }
}
