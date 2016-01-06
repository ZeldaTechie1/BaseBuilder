using UnityEngine;
using System.Collections;

public class SnapPoint : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "SnapPoint")
        {
            Debug.Log("Boop");
            if(transform.parent != null)
            {
                Vector3 distanceTo = col.transform.position - this.transform.position;
                transform.parent.GetComponent<BuildObject>().Snap(distanceTo);
            }
            else
            {
                Debug.LogError("This SnapPoint has no parent!");
            }
        }
    }

}
