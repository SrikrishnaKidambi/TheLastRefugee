using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f; // Default walking speed
    public float runSpeed = 10f; // Speed when running
    public float groundDrag;
    public float airMultiplier;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    private bool isJumping = false;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.RightShift; // Key to trigger running

    [Header("Animator")]
    [SerializeField] private Animator playerAnimator;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded = true;

    public Transform oreintation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private bool isRunning = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0f;
        }

        HandleAnimations(); // Call function to handle animation logic
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Check if the run key is being held
        isRunning = Input.GetKey(runKey);

        if (Input.GetKeyDown(jumpKey) && grounded && !isJumping)
        {
            Jump();
            playerAnimator.SetBool("IsJumping", true);
        }
    }

    private void MovePlayer()
    {
        // Calculate the movement direction based on player orientation and input
        moveDirection = oreintation.forward * verticalInput + oreintation.right * horizontalInput;

        // Apply force for movement
        float currentSpeed = isRunning && grounded ? runSpeed : walkSpeed;

        if (grounded && !isJumping)
        {
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded && !isJumping)
        {
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void Jump()
    {
        isJumping = true;
        grounded = false;
        Debug.Log("Jump triggered.");
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Invoke(nameof(ResetJump), 0.2f);
    }

    private void ResetJump()
    {
        isJumping = false;
        if (grounded)
        {
            playerAnimator.SetBool("IsJumping", false);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > (isRunning ? runSpeed : walkSpeed))
        {
            Vector3 limitedVel = flatVel.normalized * (isRunning ? runSpeed : walkSpeed);
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsGround) != 0)
        {
            grounded = true;
            isJumping = false;
            playerAnimator.SetBool("IsJumping", false);
            Debug.Log("Player grounded.");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsGround) != 0)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsGround) != 0)
        {
            grounded = false;
            Debug.Log("Player not grounded.");
        }
    }

    private void HandleAnimations()
    {
        playerAnimator.SetBool("IsWalkingForward", false);
        playerAnimator.SetBool("IsWalkingBackward", false);
        playerAnimator.SetBool("IsWalkingLeft", false);
        playerAnimator.SetBool("IsWalkingRight", false);
        playerAnimator.SetBool("IsRunning", false);

        if (isJumping)
        {
            return;
        }

        if (grounded)
        {
            if (verticalInput > 0)
            {
                if (isRunning)
                {
                    playerAnimator.SetBool("IsRunning", true);
                }
                else
                {
                    playerAnimator.SetBool("IsWalkingForward", true);
                }
            }
            else if (verticalInput < 0)
            {
                playerAnimator.SetBool("IsWalkingBackward", true);
            }
            else if (horizontalInput > 0)
            {
                playerAnimator.SetBool("IsWalkingRight", true);
            }
            else if (horizontalInput < 0)
            {
                playerAnimator.SetBool("IsWalkingLeft", true);
            }
        }
    }
}
