using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Movement and Turning
    private float xMove, zMove, verticalVel;
    private float xLook, yLook, xLookTarget, yLookTarget;
    public float currSpeed, walkSpeed, runSpeed, crouchSpeed;
    public float mouseSensX, mouseSensY;
    Vector3 targetSpeed, smoothMove;
    private bool canSprint;

    //Crouching
    Vector3 crouchPosition;
    public bool isCrouched, canCrouch;
    private float crouchHeight = 1f, standHeight = 2f;

    CapsuleCollider cc;

    //Jump
    bool grounded;
    public float jumpForce, standJump, crouchJump, boostJump;
    bool jumped;
    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {

        jumpForce = standJump;

        cc = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();        

        canCrouch = true;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        LookAround();

	}

    void Update()
    {

        xLook += Input.GetAxis("Mouse Y") * mouseSensY * Time.deltaTime;
        yLook = Input.GetAxis("Mouse X") * mouseSensX * Time.deltaTime;

        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        Debug.Log("isCrouched: " + isCrouched);
        Debug.Log("Jumped: " + jumped);
        Debug.Log("canCrouch: " + canCrouch);

        jumpForce = standJump;

        if (cc.height > 1f && cc.height < 1.8)
        {
            jumpForce = boostJump;
        }

        if(Input.GetButton("Sprint") && canSprint)
        {
            currSpeed = runSpeed;
            Movement(runSpeed);
        }
        else if (!isCrouched)
        {
            currSpeed = walkSpeed;
            canSprint = true;
            Movement(walkSpeed);
        }

        Crouch();
        Jump();
        
    }

    void Movement(float _moveSpeed)
    {
        
        Vector3 speed = new Vector3(xMove, 0, zMove);
        speed.Normalize();

        speed = transform.rotation * speed;
        targetSpeed = speed * _moveSpeed;
        speed = Vector3.SmoothDamp(speed, targetSpeed, ref smoothMove, .5f);

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime);

    }

    void LookAround()
    {

        xLook = Mathf.Clamp(xLook, -60, 60);

        transform.Rotate(0, yLook, 0); //Rotates player side to side
        Camera.main.transform.localRotation = Quaternion.Euler(-xLook, 0, 0);//Rotates camera up and down

    }

    void Jump()
    {

        //Player can only jump if he's touching the ground
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumped = true;
            rb.velocity = new Vector3(0, jumpForce, 0);
            grounded = false;
        }

    }

    void Crouch()
    {
        //Crouch
        if (Input.GetButton("Crouch") && canCrouch)
        {
            cc.height = Mathf.Lerp(cc.height, crouchHeight, Time.deltaTime * 10); //Smoothly CROUCHES player
            jumpForce = crouchJump;
            currSpeed = crouchSpeed;
            Movement(crouchSpeed);
            isCrouched = true;
            canSprint = false;
        }
        else
        {
            cc.height = Mathf.Lerp(cc.height, standHeight, Time.deltaTime * 2); //Smoothly UNCROUCHES player
            isCrouched = false;
            canSprint = true;
        }
    }

    void OnCollisionEnter (Collision other)
    {

        //Check if player is touching the ground
        if (other.collider)
        {
            grounded = true;
        }

    }

}
