using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform oreintation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    [Header("Camera Settings")]
    public float rotationSpeed=5f;
    public Vector3 cameraOffset=new Vector3(0f,2f,100f);

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleCameraMovement();
        HandlePlayerOrientation();
        if (Input.GetKey(KeyCode.Equals))
        {
            cameraOffset.z += 3f;
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            cameraOffset.z -= 3f;
        }
    }
    private void HandleCameraMovement()
    {
        Vector3 targetPos = player.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * rotationSpeed);
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }

    private void HandlePlayerOrientation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = oreintation.forward * verticalInput + oreintation.right * horizontalInput;
        if (inputDir.magnitude > 0.1f)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        oreintation.forward = new Vector3(transform.forward.x, 0f, transform.forward.z);

    }
}
