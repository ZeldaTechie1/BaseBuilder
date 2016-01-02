using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Movement and Turning
    float xMove, zMove, verticalVel;
    float xLook, yLook, xLookTarget, yLookTarget;
    public float moveSpeed, walkSpeed, runSpeed;
    public float mouseSensX, mouseSensY;
    float yMax = 290, yMin = 50;
    Vector3 targetSpeed, smoothMove;

    //public float smoothStop, smoothVel;
    //float xSmooth, ySmooth;

    //Jump
    bool grounded;
    public float jumpForce;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {

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

        moveSpeed = walkSpeed;

        Movement();
        Jump();

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

            transform.position = new Vector3(transform.position.x, 2.4f, transform.position.z);

        }

        

        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        Vector3 speed = new Vector3(xMove, 0, zMove);
        speed.Normalize();

        speed = transform.rotation * speed;
        targetSpeed = speed * moveSpeed;
        targetSpeed = Vector3.SmoothDamp(speed, targetSpeed, ref smoothMove, .5f);
        rb.MovePosition(rb.position + targetSpeed * Time.fixedDeltaTime);

    }

    void LookAround()
    {

        xLook = Input.GetAxis("Mouse Y") * mouseSensY * Time.fixedDeltaTime;
        yLook = Input.GetAxis("Mouse X") * mouseSensX * Time.fixedDeltaTime;
        //xLook = Mathf.Clamp(yLook, yMin, yMax); //Stops player from infinately looking up or down
        Debug.Log(yLook);
        transform.Rotate(0, yLook, 0); //Rotates player side to side
        Camera.main.transform.Rotate(-xLook, 0, 0);//localRotation = Quaternion.Euler(-xLook, 0, 0);

    }

    void Jump()
    {

        //Player can only jump if he's touching the ground
        if (Input.GetButtonDown("Jump") && grounded)
        {
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
