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
    GameObject player, pContainer;
    Vector3 currPos;

    //head bob
    public float headBobSpeed, bobAmtX, bobAmtY;
    public float midpoint;
    float stepCtr;
    float hori, verti, timer, waveslice, waveslice2, camPosy, camPosx;

    void Awake()
    {
    }
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player Physics");
        pContainer = GameObject.Find("Player Container");
        pController = pContainer.GetComponent<PlayerController>();        
    }
	
	// Update is called once per frame
    void Update(){
        currPos = player.transform.position;
    }
	void FixedUpdate () {
        LookAround();
	}
    void LateUpdate(){
        CameraPosition();
    }

    void CameraPosition()
    {
        transform.position = player.transform.position;

        //head bob stuff     
           
        waveslice = 0;
        hori = Input.GetAxis("Horizontal");
        verti = Input.GetAxis("Vertical");
        
        if ((Mathf.Abs(hori) == 0) && Mathf.Abs(verti) == 0 || !pController.grounded)
        {
            timer = 0.0f;
        }
        else
        {            
            waveslice = Mathf.Cos(timer * 2);
            waveslice2 = Mathf.Sin(timer);

            timer = timer + (headBobSpeed * pController.currSpeed);
            //this if statement causes the camera to come back down once it has reached it's max height at PI * 2
            if(timer > Mathf.PI * 2)
            {
                timer -= (Mathf.PI * 2);
            }
            Debug.Log(timer);
        }
        if (waveslice != 0)
        {
            float _transChange = waveslice * bobAmtY;
            float _transChange2 = waveslice2 * bobAmtX * (pController.currSpeed / 2);

            float _totalAxes = Mathf.Abs(hori) + Mathf.Abs(verti);
            _totalAxes = Mathf.Clamp(_totalAxes, 0.0f, 1.0f);
            _transChange = _totalAxes * _transChange;
            _transChange2 = _totalAxes * _transChange2;

            camPosy = player.transform.position.y;
            camPosy = midpoint * _transChange;

            camPosx = player.transform.position.x;
            camPosx = midpoint * _transChange2;

            transform.position = new Vector3 (player.transform.position.x + camPosx, player.transform.position.y + camPosy, player.transform.position.z);
        }
        /*
        Vector3 _camPos;
        
        stepCtr += Vector3.Distance(playerLastPos, player.transform.position) * headBobSpeed;

        _camPos.x = transform.position.x;
        _camPos.y = transform.position.y;

        _camPos.x = Mathf.Sin(stepCtr) * bobAmtX;
        _camPos.y = Mathf.Cos(stepCtr * 2) * bobAmtY * -1;

        playerLastPos = player.transform.position;
        */
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
