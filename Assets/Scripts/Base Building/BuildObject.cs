using UnityEngine;
using System.Collections;

public class BuildObject : MonoBehaviour {


    [SerializeField]
    GameObject snapPoint_go;
    [SerializeField]
    bool isPlaced;
    bool isSnapped;
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
	
	}

    void SpawnSnapPoint(int count, float x, float y, float z)
    {
        Vector3 curPosition = transform.position;
        Vector3 newPosition = new Vector3(x,y,z);
        GameObject snap = Instantiate(snapPoint_go, newPosition, Quaternion.identity) as GameObject;
        snap.transform.SetParent(this.gameObject.transform);
    }

    public void Snap(Vector3 direction)//moves the object towards the correct place to snap
    {
        if(!isPlaced && !isSnapped)
        {
            Vector3 newPosition = this.transform.position + direction;
            this.transform.position = newPosition;
        }
    }

}
