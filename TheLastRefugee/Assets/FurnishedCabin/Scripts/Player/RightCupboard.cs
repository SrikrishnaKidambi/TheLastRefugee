using UnityEngine;

public class RightCupboardController : MonoBehaviour
{
    private bool isOpen = false; // Tracks the door's state (open/closed)

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

    void OnMouseDown()
    {
        ToggleDoor(); // Trigger when the object is clicked
    }

    private void ToggleDoor()
    {
        if (!isOpen)
        {
            // Open the door (set rotation to X: 0, Y: -100.83, Z: 0)
            transform.localRotation = Quaternion.Euler(0f, -100.83f, 0f);
        }
        else
        {
            // Close the door (set rotation back to X: 0, Y: 0, Z: 0)
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        isOpen = !isOpen; // Toggle the state
        Debug.Log("Door toggled. isOpen = " + isOpen);
    }

    void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.O)) // Trigger when E key is pressed
        {
            ToggleDoor();
        }
    }
}

