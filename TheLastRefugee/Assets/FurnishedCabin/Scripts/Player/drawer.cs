using UnityEngine;

public class DrawerController : MonoBehaviour
{
    private bool isOpen = false; // Tracks the drawer's state (open/closed)
    private float closedPositionZ = 0f; // Z position when the drawer is closed
    private float openPositionZ = 0.319f; // Z position when the drawer is open

    void OnMouseDown()
    {
        ToggleDrawer(); // Trigger when the object is clicked
    }

    private void ToggleDrawer()
    {
        isOpen = !isOpen; // Toggle the state
        float targetPositionZ = isOpen ? openPositionZ : closedPositionZ;

        // Update the local position of the drawer
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            targetPositionZ
        );

        Debug.Log("Drawer toggled. isOpen = " + isOpen);
    }
}
