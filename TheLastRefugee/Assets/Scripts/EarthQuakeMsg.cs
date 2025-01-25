using UnityEngine;
using System.Collections; // Required for IEnumerator

public class CanvasVisibilityToggle : MonoBehaviour
{
    public Canvas canvas; // Assign the Canvas in the Inspector
    public float showDelay = 50f; // Time in seconds to wait before showing the Canvas
    public float visibleDuration = 5f; // Time in seconds for the Canvas to remain visible

    private void Start()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas is not assigned!");
            return;
        }

        // Start the toggle coroutine
        StartCoroutine(ToggleCanvasVisibility());
        canvas.enabled = false;
    }

    private IEnumerator ToggleCanvasVisibility()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(showDelay);

        // Make the Canvas visible
        canvas.enabled = true;

        // Wait for the visible duration
        yield return new WaitForSeconds(visibleDuration);

        // Make the Canvas invisible
        canvas.enabled = false;
    }
}
