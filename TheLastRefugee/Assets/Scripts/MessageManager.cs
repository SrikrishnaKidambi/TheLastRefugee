using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component for displaying messages
    public TextMeshProUGUI messageText;

    // Display a message for a certain duration
    public void DisplayMessage(string message, float duration)
    {
        if (messageText != null)
        {
            messageText.text = message;
            StartCoroutine(ClearMessageAfterDelay(duration));
        }
    }

    // Coroutine to clear the message after a delay
    private System.Collections.IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageText != null)
        {
            messageText.text = "";
        }
    }
}
