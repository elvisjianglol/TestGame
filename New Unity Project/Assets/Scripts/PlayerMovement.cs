using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    Vector3 moveDirection;
    float horizontalMovement;
    float verticalMovement;
    public float movementMultiplier = 6f;
    public float rbDrag = 5f;
    public float offGroundDrag = 1f;
    public float storedMoveSpeed = 10f;
    public float airSpeed = 1f;
    Rigidbody rb;

    //vars that change
    public float moveSpeed;

    //jumping variables
    public float jumpMultiplier = 15f;

    //groundCheck vars
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public Transform groundCheckObj;
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
        if(!onGround)
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




}
