using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    GameObject player;

    public float offsetY;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");

	}

    // Update is called once per frame
    void Update() {

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z);
        transform.rotation = player.transform.rotation;

	}
}
