using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float runspeed;
    public float crouchspeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float runcooldown;
    public float airMultiplier;
    bool readyToJump;

    public bool sprinted;

    public float crouchingspeed;
    public float crouchyscale;
    public float startyscale;
    bool readytorun;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode crouchkey = KeyCode.LeftControl;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startyscale = transform.localScale.y;
        readyToJump = true;
        readytorun = true;

        sprinted = true;
        walkSpeed = moveSpeed;
        sprintSpeed = runspeed;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // Handle crouching
        if (sprinted == false && grounded)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchyscale, transform.localScale.z);
            moveSpeed = crouchspeed;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startyscale, transform.localScale.z);
            moveSpeed = sprinted ? walkSpeed : 0f; // If crouching (sprinted = false), set moveSpeed to 0, otherwise use walkSpeed
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(sprint) && readytorun && grounded)
        {
            readytorun = false;
            Run();
            Invoke(nameof(ResetRun), runcooldown);
        }

        if (Input.GetKeyDown(crouchkey) && grounded)
        {
            sprinted = false;
        }
        if (Input.GetKeyUp(crouchkey))
        {
            sprinted = true;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Run()
    {
        // Don't allow running if crouched
        if (!sprinted)
            return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * runspeed * 10f, ForceMode.Force);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ResetRun()
    {
        readytorun = true;
    }
}