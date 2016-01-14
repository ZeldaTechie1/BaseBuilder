using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour {

    //cam rotation
    public float mouseSensX, mouseSensY;
    public float xLook, yLook;
    float currXRotation, currYRotation;    

    //smoothing
    float xSmoothMouseV, ySmoothMouseV;
    public float mouseSmoothTime;

    //find player
    public GameObject player;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        LookAround();
	}
    void LateUpdate()
    {
        CameraPosition();
    }

    void CameraPosition()
    {
        transform.position = player.transform.position;
    }

    void LookAround()
    {
        xLook += Input.GetAxis("Mouse X") * mouseSensX; //look on y axis (up and down)
        yLook -= Input.GetAxis("Mouse Y") * mouseSensY; //look on x axis (side to side)

        yLook = Mathf.Clamp(yLook, -60, 60); //limits how far the player can look up or down

        currXRotation = Mathf.SmoothDampAngle(currXRotation, xLook, ref xSmoothMouseV, mouseSmoothTime); //smooths side to side look
        currYRotation = Mathf.SmoothDampAngle(currYRotation, yLook, ref ySmoothMouseV, mouseSmoothTime); //smooths up down look        

        transform.rotation = Quaternion.Euler(currYRotation, currXRotation, 0); //applies rotation
    }
}
