//PROBLEMS TO FIX (FOR NOW): 
//1. we need to make it so that you can't change speeds while youre in the air.
//2. For some super weird reason when you play the game the player start off raised from the ground... idk why... there doesnt seems to be an object there but there might be an invisible collider or rigid body... its weird
//3. Collisions causing the player to bounce when running into an object when they should stop movement in that direction (it kinda sticking to the wall)

//NOTE
//-all inputs should be in update 
//-we might want to switch camera movement to a seperate script
//(ANTONIO's INPUT):
//STILL NEEDED:
//-combos 

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public string active;//string that contains the current "active control"
    float xLook; //required to keep track of camera position

    //Speed-stuff
    [SerializeField]
    public float crouchSpeed, walkSpeed, runSpeed;

    //mouse sensitivity
    public float mouseSensX, mouseSensY;

    //Crouching
    private float crouchHeight = 1f, standHeight = 2f; //we should shrink from the bottom since in crouch the feet go up into the torso (allows jump crouch to work properly) *not sure if this does that

    public float jumpForce; //boost force would just be jumpForce *2

    CapsuleCollider cc;
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
    void FixedUpdate()//udpate NOT reliant on framerate but with a reliable timeer
    {   
    }

    //shoud be used for camera movements
    void LateUpdate()//happens once per frame
    {
        LookAround();
    }

    void Update()//called once per frame
    {   

        //NOTE: GetButton is constantly checking if the button is down; GetButtonDown on checks if its tapped

        //MODIFY SLIGHTLY TO USE THE ACTIVE BUTTON THING - if you need me to explain why @antonio give me a call
        if(grounded)
        {

            if (Input.GetButtonDown("Jump") && (active== "null" || active == "Jump")) 
            {
                active = "Jump";
                Jump();
            }
            else if (Input.GetButton("Run") && (active == "null" || active == "Run")) 
            {
                active = "Run";
                Run();
                if (Input.GetButtonDown("Crouch"))
                    Slide();
                else if (Input.GetButtonDown("Jump"))
                    Jump();//Run Jump
            }
            else if (Input.GetButton("Crouch") && (active == "null" || active == "Crouch")) 
            {
                active = "Crouch";
                Crouch();
                if (Input.GetButtonDown("Jump"))
                    HighJump();
                else if (Input.GetButtonDown("Run"))
                    Roll();
            }
            else
            {
                active = "null";
                Walk();
            }

            //This piece would be to lean to either side 
            //at the moment it doesnt exclude anything so it works all the time (easily fixable)
            //In my opinion this should only work when you are grounded && (Runing[Maybe], standing[yes], crouching[yes]) - Suggest this stay here for now
            if (Input.GetButton("leanLeft"))
                Lean("left");
            else if (Input.GetButton("leanRight"))
                Lean("right");

        }
        else
        {
            
            if (Input.GetButton("Crouch"))
            {
                active = "Crouch"; //kinda feel an issue in my head to transition between crouch in air and not in air... idk maybe...
                //Crouch without reducing your speed
                Walk();
            }
            else
            {
                active = "null";
                Walk();
            }
            
        }

        Debug.Log("Grounded: "+grounded+" ||| Active: "+active);
    }        

    void LookAround()
    {
        
        //***might want to add smoothing on a spectrum
        //For Example: if a camera movement is faster than a certain speed the camera lags behind becasue a human cant do a 720 spin in 1 second
        //***might want to add a button for a quick 180 to quickly get to enemies behind you

        //Calc left to right rigid body rotation and apply
        float _yLook = Input.GetAxis("Mouse X");
        Vector3 _rot = new Vector3(0f, _yLook, 0f) * mouseSensX;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(_rot));

        //Calc up and down cam rotation and apply (due limits set to xLook we dont need to use use quaternions
        xLook += Input.GetAxis("Mouse Y") * mouseSensX;
        xLook = Mathf.Clamp(xLook, -60, 60); //limits how far up or down the player can look

        cam.transform.localRotation = Quaternion.Euler(-xLook, 0, 0);
        
    }

    //Add smooth stopping and acceleration MAYBE (for realisms sake)
    void Move(float _moveSpeed)
    {
        
        float _xMove = Input.GetAxis("Horizontal");
        float _zMove = Input.GetAxis("Vertical");

        /*
        rb.AddForce(new Vector3(-_zMove,0f,0f)*_moveSpeed, ForceMode.Impulse);
        rb.AddForce(new Vector3(0f, 0f, _xMove) * _moveSpeed, ForceMode.Impulse);
        */
        
        Vector3 _movHori = rb.velocity * _xMove; //use velcities here for no jitter
        Vector3 _movVerti = rb.transform.forward * _zMove;

        Vector3 _velocity = (_movHori + _movVerti).normalized * _moveSpeed; //get direction and then multiply that direction by speed
        
        if(_velocity != Vector3.zero)
            rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
    }

    void Walk()
    {
        Move(walkSpeed);
    }

    //lean to either side
    void Lean(string side)
    {
        //NOT DONE
        //lean to left with q and lean to right with e
    }

    //NORMAL 1 key moves

    void Run()
    {
        Move(runSpeed);
    }

    void Jump()
    {
        rb.AddForce((transform.up * jumpForce), ForceMode.Impulse); //its slowing down to walking speed in the air
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
