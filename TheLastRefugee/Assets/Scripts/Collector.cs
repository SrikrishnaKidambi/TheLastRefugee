using UnityEngine;

public class Collector : MonoBehaviour
{
    public GameObject collectedMessage; // UI element to show "Collected"
    private GameObject nearbyPad;       // Reference to the pad object the player is near
    public bool isCollected = false;   // Boolean to track if the pad is collected

    void Update()
    {
        // Check if the player presses 'C' and is near the pad
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (nearbyPad != null && !isCollected)
            {
                CollectPad();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the range of a pad
        if (other.CompareTag("Collectible"))
        {
            nearbyPad = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the range of the pad
        if (other.CompareTag("Collectible"))
        {
            nearbyPad = null;
        }
    }

    private void CollectPad()
    {
        // Mark the pad as collected
        isCollected = true;

        // Optional: Display "Collected" message
        if (collectedMessage != null)
        {
            collectedMessage.SetActive(true);
        }

        // Make the pad vanish
        Destroy(nearbyPad);

        // Add additional logic (e.g., increase score) here
    }
}
