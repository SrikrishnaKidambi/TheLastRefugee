using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance; // Singleton for easy access

    [Header("Camera Shake Settings")]
    public float shakeDuration = 0.5f;  // Public field to change shake duration
    public float shakeMagnitude = 0.1f; // Public field to change shake magnitude

    private Vector3 originalPosition;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        originalPosition = transform.localPosition; // Save the initial position of the camera
    }

    /// <summary>
    /// Shake the camera with the current settings.
    /// </summary>
    public void ShakeCamera()
    {
        StopAllCoroutines();  // Stop any ongoing shake
        StartCoroutine(Shake(shakeDuration, shakeMagnitude)); // Use the public variables
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate a random offset within a sphere and apply to the camera's position
            Vector3 randomOffset = Random.insideUnitSphere * magnitude;
            transform.localPosition = originalPosition + randomOffset;

            elapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Reset to the original position after the shake is finished
        transform.localPosition = originalPosition;
    }
}
