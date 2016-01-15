using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour {


    PlayerController pController;
    //cam rotation
    public float mouseSensX, mouseSensY;
    [HideInInspector] 
    public float xLook, yLook;
    float currXRotation, currYRotation;    

    //smoothing
    float xSmoothMouseV, ySmoothMouseV;
    public float mouseSmoothTime;

    //find player
    GameObject player;

    //head bob
    public float headBobSpeed, bobAmtX, bobAmtY, heightRatio;
    float stepCtr;
    Vector3 playerLastPos;

    void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player Physics");
        pController = player.GetComponent<PlayerController>();
        
    }
	
	// Update is called once per frame
    void Update(){
        
    }
	void FixedUpdate () {
        LookAround();
	}
    void LateUpdate(){
        CameraPosition();
    }

    void CameraPosition()
    {
        playerLastPos = player.transform.position;
        transform.position = playerLastPos;

        //head bob stuff
        Vector3 _camPos;
        
        stepCtr += Vector3.Distance(playerLastPos, player.transform.position) * headBobSpeed;

        _camPos.x = transform.localPosition.x;
        _camPos.y = transform.localPosition.y;

        _camPos.x = Mathf.Sin(stepCtr) * bobAmtX;
        _camPos.y = Mathf.Cos(stepCtr * 2) * bobAmtY * -1;

        playerLastPos = player.transform.position;
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
