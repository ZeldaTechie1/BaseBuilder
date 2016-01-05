using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Movement and Turning
    float xMove, zMove, verticalVel;
    float xLook, yLook, xLookTarget, yLookTarget;
    public float moveSpeed, walkSpeed, runSpeed, crouchSpeed;
    public float mouseSensX, mouseSensY;
    Vector3 targetSpeed, smoothMove;

    //Crouching
    Vector3 crouchPosition;
    public bool isCrouched;
    float crouchHeight = 1f, standHeight = 2f;

    CapsuleCollider cc;

    //Jump
    bool grounded;
    public float jumpForce, standJump, crouchJump;
    bool jumped;
    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {

        jumpForce = standJump;

        cc = GetComponent<CapsuleCollider>();

        rb = GetComponent<Rigidbody>();
        moveSpeed = walkSpeed;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        LookAround();

	}

    void Update()
    {
        Jump();

        moveSpeed = walkSpeed;

        xLook += Input.GetAxis("Mouse Y") * mouseSensY * Time.deltaTime;
        yLook = Input.GetAxis("Mouse X") * mouseSensX * Time.deltaTime;

        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        isCrouched = false;

        if (isCrouched && jumped)
        {
            cc.height = standHeight;
        }

        Movement();
        
    }

    void Movement()
    {

        if (Input.GetButton("Sprint"))
        {
            moveSpeed = runSpeed;
        }

        //Crouch
        if (Input.GetButton("Crouch"))
        {
            cc.height = 1f;
            jumpForce = crouchJump;
            moveSpeed = crouchSpeed;
            isCrouched = true;
        }
        else
        {
            cc.height = Mathf.Lerp(cc.height, 2f, Time.deltaTime * 8); //Smoothly uncrouches the player
            jumpForce = standJump;
        }

        Vector3 speed = new Vector3(xMove, 0, zMove);
        speed.Normalize();

        speed = transform.rotation * speed;
        targetSpeed = speed * moveSpeed;
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

    void OnCollisionEnter (Collision other)
    {

        //Check if player is touching the ground
        if (other.collider)
        {
            grounded = true;
        }

    }

}
