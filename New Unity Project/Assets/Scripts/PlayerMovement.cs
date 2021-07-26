using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    Vector3 moveDirection;
    float horizontalMovement;
    float verticalMovement;

    [SerializeField] private float movementMultiplier = 6f;
    [SerializeField] private float rbDrag = 7f;
    [SerializeField] private float offGroundDrag = 1f;

    [SerializeField] private float storedMoveSpeed = 10f;
    [SerializeField] private float airSpeed = 1f;


    private float dashingCooldown;
    [SerializeField] private float dashingDelay = 3f;
    [SerializeField] private float dashDuration = 0.5f;
    private bool isDashing = false;

    [SerializeField] private float dashingSpeed = 6f;
    [SerializeField] private float dashingDrag = 5f;

    Rigidbody rb;

    //vars that change
    private float moveSpeed;

    //jumping variables
    [SerializeField] private float jumpMultiplier = 15f;

    //groundCheck vars
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private Transform groundCheckObj;
    bool onGround;



    private void Start()
    {
        moveSpeed = storedMoveSpeed;
        // get rb object
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }




    private void Update()
    {

        onGround = Physics.CheckSphere(groundCheckObj.position, groundDistance, groundMask);
        MyInput();
        ControlDrag();

        if (Input.GetButtonDown("Jump") && onGround)
        {
            PlayerJump();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= dashingCooldown)
        {
            dashingCooldown = Time.time + dashingDelay;
            isDashing = true;

            rb.AddForce(moveDirection * dashingSpeed * 10, ForceMode.VelocityChange)
        }


        if(isDashing)
        {
            // up the drag to make dashing stiffer 
            rb.drag = dashingDrag;
            
            // add a timer for the drag
            Invoke("DashReset", dashDuration);
        }

        else if(!onGround)
        {
            rb.drag = offGroundDrag;
            moveSpeed = airSpeed;
            
        }

        else
        {
            moveSpeed = storedMoveSpeed;
        }
    }




    private void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }




    private void FixedUpdate()
    {
        // No need for deltaTime
        MovePlayer();
    }




    void MovePlayer()
    {
        // normalize
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }




    void ControlDrag()
    {
        rb.drag = rbDrag;
    }




    void PlayerJump()
    {
        rb.AddForce(transform.up * jumpMultiplier, ForceMode.VelocityChange);
    }

    void DashReset()
    {
        isDashing = false;
    }
}


