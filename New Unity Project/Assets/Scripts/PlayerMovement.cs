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
    public float moveSpeed = 10f;

    Rigidbody rb;

    // get rb object

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void Update()
    {
        MyInput();
        ControlDrag();
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
}
