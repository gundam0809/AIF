using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump = true;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [Header("Ground Check")]
    public float playerHieght;
    public LayerMask Ground;
    public bool grounded = true;
    public Transform orientation;
    float horizontatInput;
    float verticalInput;

    Vector3 moveDirection;
    public Rigidbody rb;
    //rotation variables
    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {

        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHieght * 0.5f + 0.2f, Ground);
        MyInput();
        SpeedControl();
        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        //player rotation with mouse
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontatInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded == true)
        {
            readyToJump = false;

            Jump();
            StartCoroutine(resetJump());

            jumpCooldown = 2;
            Console.WriteLine("jump");
        }
    }
    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontatInput;
        // on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        }

        // in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // limit velocity if needed
        //if we go faster then our movement speed
        if (flatVel.magnitude > moveSpeed)
        {
            //calculate what the max velocity is then apply it.
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }
    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 1f, rb.velocity.z);

        //add force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private IEnumerator resetJump()
    {
        yield return new WaitForSeconds(jumpCooldown);

        Debug.Log("Hmm whatthe sigma?");
        readyToJump = true;

    }
}

