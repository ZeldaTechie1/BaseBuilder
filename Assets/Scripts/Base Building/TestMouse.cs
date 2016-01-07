using UnityEngine;
using System.Collections;

public class TestMouse : MonoBehaviour {

    Camera viewCamera;
    public GameObject placeholder;
    public float objectDistance;
    public float objectRotation;
    public float zoomSens;
    public float rotSens;

    // Use this for initialization
    void Start () {
        viewCamera = Camera.main;
        objectDistance = 10;
        objectRotation = 0;
        zoomSens = 10;
        rotSens = 10;
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 planePos = viewCamera.transform.position + (viewCamera.transform.forward * objectDistance);
        Plane ground = new Plane(-viewCamera.transform.forward, planePos);
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

        if(!Input.GetButton("Left Shift"))
        {
            if (objectDistance > 0)
                objectDistance += Input.GetAxisRaw("Mouse ScrollWheel") * zoomSens;
            else
                objectDistance = 1;
        }
        else
        {
            Debug.Log("Rotating!");
            objectRotation += Input.GetAxisRaw("Mouse ScrollWheel") * rotSens;
        }
        placeholder.transform.LookAt(viewCamera.transform);
        placeholder.transform.rotation = Quaternion.Euler(placeholder.transform.localRotation.x, placeholder.transform.localRotation.y + objectRotation, placeholder.transform.localRotation.z);
    }
}
