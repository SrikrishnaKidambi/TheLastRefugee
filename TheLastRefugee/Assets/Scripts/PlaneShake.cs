using UnityEngine;

public class PlaneShake : MonoBehaviour
{
    public float shakeDuration = 120.0f;        // How long the shake lasts
    public float shakeMagnitude = 0.2f;       // Intensity of the shake (higher = stronger shake)
    public float delayBeforeShake = 50.0f;     // Delay before the shake starts

    private Vector3 originalPosition;         // Original position of the plane
    private float shakeTimer = 0f;            // Timer to control shake duration
    private bool isShaking = false;           // Flag to control if shaking is active

    void Start()
    {
        // Store the original position of the plane
        originalPosition = transform.position;

        // Start the shake after the specified delay
        Invoke("StartShake", delayBeforeShake);
    }

    void Update()
    {
        // If the shake is active, update the plane's position
        if (isShaking)
        {
            shakeTimer += Time.deltaTime;

            // Apply shake to position (only in the XZ plane)
            Vector3 shakePosition = originalPosition + new Vector3(
                Random.Range(-shakeMagnitude, shakeMagnitude), // Shake along X-axis
                0,                                             // No movement along Y-axis
                Random.Range(-shakeMagnitude, shakeMagnitude)  // Shake along Z-axis
            );

            // Update the plane's position
            transform.position = shakePosition;

            // Stop shaking after the specified duration
            if (shakeTimer >= shakeDuration)
            {
                StopShake();
            }
        }
    }

    // Function to start the shake
    public void StartShake()
    {
        isShaking = true;
        shakeTimer = 0f;
    }

    // Function to stop the shake
    private void StopShake()
    {
        isShaking = false;
        transform.position = originalPosition;
    }
}
