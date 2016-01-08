//PROBLEMS TO FIX (FOR NOW): 
//1. we need to make it so that you can't change speeds while youre in the air.

//NOTE
//-all inputs should be in update 
//-we might want to switch camera movement to a seperate script

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Movement
    private float xMove, zMove;

    //Camera
    private float xLook, yLook;

    //Speed-stuff
    [SerializeField]
    public float crouchSpeed, walkSpeed, runSpeed;

    //mouse sensitivity
    public float mouseSensX, mouseSensY;

    Vector3 targetSpeed, smoothMove; //not sure if being used properly

    //Crouching
    private float crouchHeight = 1f, standHeight = 2f; //we should shrink from the bottom since in crouch the feet go up into the torso (allows jump crouch to work properly) *not sure if this does that

    //FUNCTION REQUIRED TO MANAGE THIS***
    bool grounded; //only true when the "feet" of the collider are touching the floor
        //BEST OPTION(bryan thinks): Check if the object has up and down momentum, if it doesnt then its grounded
            //Possible Issues:
                //might be an issue when moving into crouch depending how its coded
        //2nd Option: RayCasting: shot a ray and measure its distance and if its smaller than a value then register as grounded
            //Possible Issues:
                //If you are on an edge it will register as not grounded if we shot the ray from the center which will cause bugs within the logic tree
                //ELSE
                    //not sure how stairs will register, might have to make the raycast detect bigger than the vertical size of one single stair level
                        //this might cause further bugs because it will register as not grounded when about to reach the floor which will let you double jump if your precise enough               

    public float jumpForce, standJump, crouchJump, boostJump;//not sure if any of this is needed

    CapsuleCollider cc;
    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        cc = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();        
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
        xLook += Input.GetAxis("Mouse Y") * mouseSensY * Time.deltaTime;
        yLook = Input.GetAxis("Mouse X") * mouseSensX * Time.deltaTime;

        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        //Debug.Log("isCrouched: " + isCrouched);

        /*
        NEW LOGIC TREE-----dont erase please
            (in this logic tree the arrow keys will allways be active so in-Air movement is possible at walking speed)
            '***'Indicates functions that must be created
                MOVEMENT***
        
        if Grounded
            NOTE: THE ITEMS IN THIS INDENT ARE ALL; If button is held and therefore active
            if JUMP***
            else if RUN***
                Increase speed
                if Crouch TAPPED
                    SLIDE***
            else if CROUCH***
                Lower speed & ofcourse your field of view is lowered
                if Jump TAPPED
                    HIGH JUMP***
                else if Run TAPPED
                    ROLL***      
        else
            if Crouch
                CROUCH IN AIR***
                Note: Run and Jump dont do anything here
        */

        grounded=true;//WILL OBVIOUSLY CAUSE ISSUES - I placed this here to atleast be able to get something done until you get the grounded function done which btw should be placed here

        //NOTE: GetButton is constantly checking if the button is down; GetButtonDown on checks if its tapped

        //MODIFY SLIGHTLY TO USE THE ACTIVE BUTTON THING - if you need me to explain why @antonio give me a call
        if(grounded==true)
        {
            if (Input.GetButtonDown("Jump")) //doesnt require an active button change because will now NOT be grounded (maybe an exception but I dont think so)
                Jump();
            else if (Input.GetButton("Run")) //should also change the active button to run
            {
                Run();
                if (Input.GetButtonDown("Crouch")) //it should only slide the first time its tapped, if its pressed continually it will be ignored
                    Slide();
            }
            else if (Input.GetButton("Crouch")) //should also change the active button to crouch
                Crouch();
            else
                Move(walkSpeed);
        }
        else
        {
            //once you finish the grounded function you can start building this with the logic tree above @antonio
        }


    }

    void LookAround()
    {
        //***might want to add smoothing on a spectrum
        //For Example: if a camera movement is faster than a certain speed the camera lags behind becasue a human cant do a 720 spin in 1 second
        //***might want to add a button for a quick 180 to quickly get to enemies behind you

        xLook = Mathf.Clamp(xLook, -60, 60); //limits how far up or down the player can look
        Camera.main.transform.localRotation = Quaternion.Euler(-xLook, 0, 0);//Rotates camera up and down

        transform.Rotate(0, yLook, 0); //Rotates player side to side
    }

    void Grounded()
    {

    }

    //Bryan Checkpoint---Plz no erase, thank ju!

    void Move(float _moveSpeed) //seems to be working fine
    { 
        Vector3 speed = new Vector3(xMove, 0, zMove);
        speed.Normalize();

        speed = transform.rotation * speed;
        targetSpeed = speed * _moveSpeed;
        speed = Vector3.SmoothDamp(speed, targetSpeed, ref smoothMove, .5f);

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime);
    }

    //NORMAL 1 key moves

    void Run()
    {
        Move(runSpeed);
    }

    void Jump()
    {
        rb.velocity = new Vector3(0, jumpForce, 0); //FIX THIS - for some reason this its falling really slowly once you jump (its should you global gravity)
    }

    void Crouch() //WORRY ABOUT THIS LAST @antonio - I rather take care of it
    {
        Move(crouchSpeed);
        //NOT-DONE
    }

    //COMBOS - dont attempt till all other functions are up and running

    void Slide()
    {

    }

    void HighJump()
    {

    }

    void Roll()
    {

    }

}
