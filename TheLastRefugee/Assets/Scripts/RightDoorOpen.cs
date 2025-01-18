using UnityEngine;

public class RightDoorOpen : MonoBehaviour
{
    private bool isOpen = false; // Tracks the door's state (open/closed)
    private float closedRotation = 0f; // Y rotation when the door is closed
    private float openRotation = 90f; // Y rotation when the door is open

    private bool canOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered the collider");
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
    //void OnMouseDown()
    //{
    //    ToggleDoor(); // Trigger when the object is clicked
    //}

    void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E)) // Trigger when E key is pressed
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen; // Toggle the state
        float targetRotationY = isOpen ? openRotation : closedRotation;

        // Update the rotation of the door
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotationY, transform.rotation.eulerAngles.z);

        Debug.Log("Door toggled. isOpen = " + isOpen);
    }
}

