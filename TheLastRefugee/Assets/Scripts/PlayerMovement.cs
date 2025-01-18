using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public float airMultiplier;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    private bool isJumping = false;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

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

        HandleAnimations();  // Call function to handle animation logic
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
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

        // Debugging: Check the input and direction vectors
        Debug.Log($"Horizontal Input: {horizontalInput}, Vertical Input: {verticalInput}");
        Debug.Log($"Move Direction: {moveDirection}");

        // Apply force for movement
        if (grounded && !isJumping)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded && !isJumping)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
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
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
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
        if (isJumping)
        {
            return;
        }
        if (grounded)
        {
            if (verticalInput > 0)
            {
                playerAnimator.SetBool("IsWalkingForward", true);
            }else if(verticalInput < 0)
            {
                playerAnimator.SetBool("IsWalkingBackward", true);
            }else if(horizontalInput > 0)
            {
                playerAnimator.SetBool("IsWalkingRight", true);
            }else if(horizontalInput < 0)
            {
                playerAnimator.SetBool("IsWalkingLeft", true);
            }
        }
    }
}
