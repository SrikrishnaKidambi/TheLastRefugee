using UnityEngine;

public class DrawOpen : MonoBehaviour
{
    public float moveDistance = 0.5f; // Distance the drawer moves
    public float moveSpeed = 2.0f; // Speed of the movement

    private bool isOpen = false; // Tracks whether the drawer is open or closed
    private Vector3 initialPosition; // Stores the drawer's initial position
    private Vector3 openPosition; // Target position when the drawer is open

    private bool canOpen = false; // Tracks if the player is in range

    private void Start()
    {
        // Save the initial position and calculate the open position
        initialPosition = transform.localPosition;
        openPosition = initialPosition + new Vector3(0, 0, moveDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered range.");
            canOpen = true; // Allow interaction when the player is nearby
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left range.");
            canOpen = false; // Disallow interaction when the player leaves
        }
    }

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.P)) // Check if the player presses the "P" key
        {
            ToggleDrawer(); // Open or close the drawer
        }
    }

    private void ToggleDrawer()
    {
        isOpen = !isOpen; // Toggle the drawer state

        // Determine the target position based on the drawer state
        Vector3 targetPosition = isOpen ? openPosition : initialPosition;

        Debug.Log("Toggling drawer. Moving to position: " + targetPosition);

        StopAllCoroutines(); // Stop any ongoing movement
        StartCoroutine(MoveDrawer(targetPosition)); // Start moving the drawer
    }

    private System.Collections.IEnumerator MoveDrawer(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.localPosition;

        while (elapsedTime < moveSpeed)
        {
            transform.localPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition; // Ensure the final position is exact
        Debug.Log("Drawer movement complete. Final position: " + transform.localPosition);
    }
}
