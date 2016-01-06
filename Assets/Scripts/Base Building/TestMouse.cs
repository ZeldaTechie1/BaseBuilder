using UnityEngine;
using System.Collections;

public class TestMouse : MonoBehaviour {

    Camera viewCamera;
    public GameObject placeholder;

	// Use this for initialization
	void Start () {
        viewCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(-viewCamera.transform.forward, Vector3.zero);
        float rayDistance;
        Vector3 rawPosition;

        if (ground.Raycast(ray, out rayDistance))
        {
            rawPosition = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, rawPosition, Color.red);
            Vector3 newPos = new Vector3(rawPosition.x, rawPosition.y, rawPosition.z);
            Debug.DrawLine(ray.origin, newPos, Color.green);
            placeholder.transform.position = newPos;
        }

    }
}
