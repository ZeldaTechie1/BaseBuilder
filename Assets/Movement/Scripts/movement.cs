using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(CharacterController))] // Automatically adds the CharacterController to any object containing this script
public class movement : MonoBehaviour {

    // Crouch
    bool crouching = false;
    float charHeight;

    // Jump 
    bool canJump;
    public float verticalVel;
    public float jumpForce = 2.0f;
    public LayerMask ground;

    // Movement
    bool canRun;
    public float movementSpeed = 2.0f;
    float xMove;
    float zMove;
    float leftRightLook, upDownLook;
    public float mouseSens;
    public float yRange;
    public float runningSpeed = 2.0f;
    public float walkSpeed = 1.0f;
    public float crouchSpeed;
    CharacterController cc;
    Vector3 speed;

	// Use this for initialization
	void Start()
    {

        canRun = true;

        walkSpeed = movementSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);

        cc = GetComponent<CharacterController>();

	}
	

	// Update is called once per frame
	void FixedUpdate () {

        // Disables ability to jump and run while crouching
        if (Input.GetButtonDown("Crouch") && !crouching)
        {
            canRun = false;
            canJump = false;
            Crouch();
        }
        else if (Input.GetButtonDown("Crouch") && crouching)
        {
            canRun = true;
            Stand();
        }

        Movement();

        if (cc.isGrounded && !crouching)
        {
            canJump = true;
            verticalVel = 0;
            Jumping();
        }

	}

    void Movement() {

        // Speed handler
        if (Input.GetButton("Running") && canRun)
        {
            movementSpeed = runningSpeed;
        }
        else if (crouching)
        {
            movementSpeed = crouchSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }

        // Gives movement to character
        xMove = Input.GetAxis("Horizontal") * movementSpeed;
        zMove = Input.GetAxis("Vertical") * movementSpeed;

        //Allows character to look around using mouse 
        leftRightLook = Input.GetAxis("Mouse X") * mouseSens;
        upDownLook -= Input.GetAxis("Mouse Y") * mouseSens;

        // Sets limit so player can't infinately look up or down
        upDownLook = Mathf.Clamp(upDownLook, -yRange, yRange);

        verticalVel += Physics.gravity.y * Time.deltaTime * 2;
               
        speed = new Vector3(xMove, verticalVel, zMove); // Sets gravity on y and movement on x and z 

        transform.Rotate(0, leftRightLook, 0); // Rotates object on the y axis to wherever the mouse is pointing along the x axis

        // Uses camera to rotate player's view up and down
        Camera.main.transform.localRotation = Quaternion.Euler(upDownLook, 0, 0);
        speed = transform.rotation * speed;

        cc.Move(speed * Time.deltaTime);
        
    }

    void Jumping() {

        if (Input.GetButtonDown("Jump") && canJump)
        {
            verticalVel = jumpForce;
        }

    }

    void Crouch() {

        cc.height *= 0.5f;
        crouching = true;
        
    }

    void Stand() {

        cc.height *= 2.0f;
        crouching = false;
        
    }

}