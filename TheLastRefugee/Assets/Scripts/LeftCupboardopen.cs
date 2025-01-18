using UnityEngine;

public class LeftCupboardopen : MonoBehaviour
{
    private bool isOpen = false; // Tracks the door's state (open/closed)

    void OnMouseDown()
    {
        ToggleDoor(); // Trigger when the object is clicked
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // Trigger when E key is pressed
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        if (!isOpen)
        {
            // Open the door (set rotation to X: 0, Y: -100.83, Z: 0)
            transform.localRotation = Quaternion.Euler(0f, 66.96f, 0f);
        }
        else
        {
            // Close the door (set rotation back to X: 0, Y: 0, Z: 0)
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        isOpen = !isOpen; // Toggle the state
        Debug.Log("Door toggled. isOpen = " + isOpen);
    }
}
