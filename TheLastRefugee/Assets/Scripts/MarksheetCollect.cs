using UnityEngine;
using TMPro;

public class MakrsheetCollects : MonoBehaviour
{
    // Boolean to track whether the item has been collected
    public bool isCollected = false;

    // Reference to the TextMeshProUGUI component for displaying messages
    public TextMeshProUGUI messageText;

    // Reference to a helper GameObject to manage the message
    public MessageManager messageManager;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is tagged as "Player"
        if (other.CompareTag("Player") && !isCollected)
        {
            // Set the collectible as collected
            isCollected = true;

            // Make the asset vanish
            gameObject.SetActive(false);

            // Display the collection message using the MessageManager
            if (messageManager != null)
            {
                messageManager.DisplayMessage("Marksheet Collected!", 2f);
            }
        }
    }
}

