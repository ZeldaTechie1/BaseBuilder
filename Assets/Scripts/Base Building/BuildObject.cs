using UnityEngine;
using System.Collections;

public class BuildObject : MonoBehaviour {

    SnapPoint[,,] snapPoints;
    [SerializeField]
    GameObject snapPoint_go;

    // Use this for initialization
    void Start () {
        string name = this.gameObject.name;
        int dim1 = int.Parse(name[0]+"");
        int dim2 = int.Parse(name[2]+"");
        int dim3 = int.Parse(name[4] + "");
        Debug.Log("Dimensions: " + dim1 + "x" + dim2 + "x" + dim3);
        snapPoints = new SnapPoint[dim1,dim2,dim3];

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
