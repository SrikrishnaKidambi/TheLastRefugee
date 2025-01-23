using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;  // Camera transform to apply the shake
    public float shakeDuration = 0f;   // How long the shake lasts
    public float shakeAmount = 0.5f;   // Intensity of the shake
    public float decreaseFactor = 1.0f;

    private Vector3 originalPosition;  // Stores the original position of the camera
    private bool isShaking = false;    // Flag to control shake state

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = transform;  // Use the transform of the attached object
        }

        originalPosition = cameraTransform.position;
    }

    void Update()
    {
        if (isShaking)
        {
            if (shakeDuration > 0)
            {
                // Apply a random offset to the camera's position
                cameraTransform.position = originalPosition + new Vector3(
                    Random.Range(-shakeAmount, shakeAmount),
                    Random.Range(-shakeAmount, shakeAmount),
                    0f // No shake in the Z-axis for a 3D third-person view
                );

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                // End shake and reset position
                shakeDuration = 0f;
                isShaking = false;
                cameraTransform.position = originalPosition;
            }
        }
    }

    // Public method to trigger the shake
    public void TriggerShake(float duration, float intensity)
    {
        if (!isShaking)
        {
            originalPosition = cameraTransform.position;  // Update original position in case it changes
        }
        shakeDuration = duration;
        shakeAmount = intensity;
        isShaking = true;
    }
}
