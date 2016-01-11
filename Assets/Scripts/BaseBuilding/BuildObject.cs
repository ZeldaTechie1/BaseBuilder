using UnityEngine;
using System.Collections;

public class BuildObject : MonoBehaviour {


    [SerializeField]
    GameObject snapPoint_go;
    public GameObject Placeholder;
    public bool isPlaced;
    public bool snap;
    public int dimX;
    public int dimY;
    public int dimZ;

    // Use this for initialization
    void Start () {
        for (int count = 0; count < dimX; count++)//spawns snap points along the x axis
        {
            Vector3 curPosition = transform.position;
            //spawns top two
            SpawnSnapPoint(count, curPosition.x - ((.5f * (dimX - 1)) - (count)), curPosition.y + (.5f * dimY), curPosition.z - (.5f * dimZ));
            SpawnSnapPoint(count, curPosition.x - ((.5f * (dimX - 1)) - count), curPosition.y + (.5f * dimY), curPosition.z + (.5f * dimZ));
            //spawns bottom two
            SpawnSnapPoint(count, curPosition.x - ((.5f * (dimX - 1)) - (count)), curPosition.y - (.5f * dimY), curPosition.z - (.5f * dimZ));
            SpawnSnapPoint(count, curPosition.x - ((.5f * (dimX - 1)) - count), curPosition.y - (.5f * dimY), curPosition.z + (.5f * dimZ));
        }
        for (int count = 0; count < dimY; count++)//spawns snap points along the y axis
        {
            Vector3 curPosition = transform.position;
            //spawns right two
            SpawnSnapPoint(count, curPosition.x + (.5f * dimX), curPosition.y - ((.5f * (dimY - 1)) - (count)), curPosition.z - (.5f * dimZ));
            SpawnSnapPoint(count, curPosition.x + (.5f * dimX), curPosition.y - ((.5f * (dimY - 1)) - (count)), curPosition.z + (.5f * dimZ));
            //spawns left two
            SpawnSnapPoint(count, curPosition.x - (.5f * dimX), curPosition.y - ((.5f * (dimY - 1)) - (count)), curPosition.z - (.5f * dimZ));
            SpawnSnapPoint(count, curPosition.x - (.5f * dimX), curPosition.y - ((.5f * (dimY - 1)) - (count)), curPosition.z + (.5f * dimZ));
        }
        for (int count = 0; count < dimZ; count++)//spawns snap points along the z axis
        {
            Vector3 curPosition = transform.position;
            //spawns front two
            SpawnSnapPoint(count, curPosition.x - (.5f * dimX), curPosition.y + (.5f * dimY), curPosition.z - ((.5f * (dimZ - 1)) - (count)));
            SpawnSnapPoint(count, curPosition.x + (.5f * dimX), curPosition.y + (.5f * dimY), curPosition.z - ((.5f * (dimZ - 1)) - (count)));
            //spawns back two
            SpawnSnapPoint(count, curPosition.x - (.5f * dimX), curPosition.y - (.5f * dimY), curPosition.z - ((.5f * (dimZ - 1)) - (count)));
            SpawnSnapPoint(count, curPosition.x + (.5f * dimX), curPosition.y - (.5f * dimY), curPosition.z - ((.5f * (dimZ - 1)) - (count)));
        }
    }
	
	// Update is called once per frame
	void Update () {
       if(!snap)
        {
            if(Placeholder !=null)
            {
                if(Placeholder.tag == "Placeholder")
                {
                    this.transform.position = Placeholder.transform.position;
                    this.transform.rotation = Placeholder.transform.rotation;
                }
            }
        }
	}

    void SpawnSnapPoint(int count, float x, float y, float z)
    {
        Vector3 position = new Vector3(x,y,z);
        GameObject snap = Instantiate(snapPoint_go, position, Quaternion.identity) as GameObject;
        snap.transform.SetParent(this.gameObject.transform);
    }

    public void Snap(Vector3 position, Vector3 pointPosition)//moves the object towards the correct place to snap
    {
        if(snap)
        {
            Debug.Log("Boop");
            Vector3 direction = position - pointPosition;
            Vector3 newPosition = this.transform.position + direction;
            this.transform.position = newPosition;
        }
        

    }
}
