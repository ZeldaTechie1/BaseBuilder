//PROBLEMS TO FIX (FOR NOW): 
//1. we need to make it so that you can't change speeds while youre in the air.
//2. For some super weird reason when you play the game the player start off raised from the ground... idk why... there doesnt seems to be an object there but there might be an invisible collider or rigid body... its weird
//3. Collisions causing the player to bounce when running into an object when they should stop movement in that direction (it kinda sticking to the wall)

//NOTE
//-all inputs should be in update 
//-we might want to switch camera movement to a seperate script

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public string active;//string that contains the current "active control"
    float xLook; //required to keep track of camera position

    //Momentum
    [SerializeField]
    float momentum, currMomentum, maxMomentum = 3;

    //Speed-stuff
    [SerializeField]
    public float crouchSpeed, walkSpeed, runSpeed, currSpeed;
    Vector3 targetVel, currVel;
    float _xMove, _zMove;
    float maxChange = 20f;
    float xSmoothV, zSmoothV;
    public float smoothTime;

    //mouse sensitivity
    public float mouseSensX, mouseSensY;
    
    //Crouching
    private float crouchHeight = 1f, standHeight = 2f; //we should shrink from the bottom since in crouch the feet go up into the torso (allows jump crouch to work properly) *not sure if this does that

    public float jumpForce; //boost force would just be jumpForce *2

    CapsuleCollider cc;
    CharacterController CharacterC;
    Rigidbody rb;
    SphereCollider sc;
    Camera cam;

    public bool grounded = true;

	// Use this for initialization
	void Start ()
    {
        cc = GameObject.Find("Player Physics").GetComponent<CapsuleCollider>();
        sc = GameObject.Find("Player Physics").GetComponent<SphereCollider>();
        rb = GameObject.Find("Player Physics").GetComponent<Rigidbody>();
        CharacterC = GetComponent<CharacterController>();

        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
    }

    //link for more info on different updates
    //http://docs.unity3d.com/Manual/ExecutionOrder.html

    //Note
    //anything that affects a rigidbody should be here
    //right after this any physics calc will be made
    //use forces for movement here
    //doesnt reqire multiplication by deltaTime
    //can be called multiple times per frame
    void FixedUpdate()//udpate NOT reliant on framerate but with a reliable timer
    {
    }

    void Update()//called once per frame
    {

        //NOTE: GetButton is constantly checking if the button is down; GetButtonDown on checks if its tapped

        //MODIFY SLIGHTLY TO USE THE ACTIVE BUTTON THING - if you need me to explain why @antonio give me a call
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))// && (active == "null" || active == "Jump"))
            {
                active = "Jump";
                Jump();
            }
            else if (Input.GetButton("Run"))// && (active == "null" || active == "Run"))
            {
                active = "Run";
                Run();
                if (Input.GetButtonDown("Crouch"))
                    Slide();
                else if (Input.GetButtonDown("Jump"))
                    Jump();//Run Jump
            }
            else if (Input.GetButton("Crouch"))//&& (active == "null" || active == "Crouch"))
            {
                active = "Crouch";
                Crouch();
                if (Input.GetButtonDown("Jump"))
                    HighJump();
                else if (Input.GetButtonDown("Run"))
                    Roll();

            }
            else if (Input.GetButton("leanLeft"))
            {
                Lean("left");
            }
            else if (Input.GetButton("leanRight"))
            {
                Lean("right");
            }
            else
            {
                active = "null";
                Walk();
            }
            //This piece would be to lean to either side 
            //at the moment it doesnt exclude anything so it works all the time (easily fixable)
            //In my opinion this should only work when you are grounded && (Runing[Maybe], standing[yes], crouching[yes]) - Suggest this stay here for now          
        }
        Debug.Log("Grounded: "+grounded+" ||| Active: "+active);
    }        

    //Add smooth stopping and acceleration MAYBE (for realisms sake)
    void Move(float _moveSpeed)
    {
        //calculates a target velocity for the player.
        targetVel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVel = rb.transform.rotation * targetVel;
        targetVel *= _moveSpeed;

        //gets value between our current velocity and the velocity we want to get to so it controls the velocity
        //otherwise it would cause the player to shoot off really far 
        Vector3 _velocity = rb.velocity;
        Vector3 _velChange = (targetVel - _velocity);
        _velChange.y = 0;

        rb.AddForce(_velChange, ForceMode.VelocityChange);

        //rotates player according to the cameras rotation on the y axis
        float _xCharRotation = rb.transform.rotation.y;
        _xCharRotation = cam.GetComponent<CameraLook>().xLook;
        rb.transform.rotation = Quaternion.Euler(0, _xCharRotation, 0);

        if (targetVel.x != 0 || targetVel.z != 0)//momentum increases over time the more you move until it reaches max momentum for the players current velocity
            currMomentum += Time.deltaTime;
        else
            currMomentum = 1; //momentum while not moving is 1

        //NOTE: realistically, when youre standing your momentum should be 0; but, for our purposes, setting it to 1 when not moving...
        //currently gives the best results. unless a better method is found to fix this (to set idle momentum exactly to 0), the above seems to work well. 

        momentum = Mathf.Min(currMomentum, maxMomentum);
    }

    void Walk()
    {
        maxMomentum = 1;
        currSpeed = Mathf.Lerp(currSpeed, walkSpeed, 5f * Time.deltaTime); //when player stops running it will not reach walk speed immediatly
        Move(currSpeed);
    }

    //lean to either side
    void Lean(string side)
    {
        //NOT DONE
        //lean to left with q and lean to right with e
        //OR
        //lean with q and the side depends on whether youre pressing left key or right key (A or D)
    }

    //NORMAL 1 key moves

    void Run()
    {
        maxMomentum = 3;
        currSpeed = Mathf.Lerp(currSpeed, runSpeed, 1f * Time.deltaTime); //player takes a bit of time to reach full running speed (humans cant reach max speed in less than one second)
        Move(currSpeed);
    }

    void Jump()
    {
        rb.AddForce(momentum * targetVel.x, jumpForce, momentum * targetVel.z, ForceMode.Impulse); //its slowing down to walking speed in the air
    }

    void Crouch() //WORRY ABOUT THIS LAST @antonio - I rather take care of it
    {
        Move(crouchSpeed);
        //NOT-DONE
    }

    //COMBOS - dont attempt till all other functions are up and running

    void Slide()
    {
        Debug.Log("SLIDE");
    }

    void HighJump()
    {
        rb.AddForce(new Vector3(0f, jumpForce*5, 0f), ForceMode.Impulse);
    }

    void Roll()
    {
        Debug.Log("ROLL");
    }

}
