using UnityEngine;

public class ShakeTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // When you press the space bar
        {
            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.shakeDuration = 0.7f;  // 0.7s duration
                CameraShake.Instance.shakeMagnitude = 0.4f; // 0.4 intensity

                CameraShake.Instance.ShakeCamera(); // Trigger the shake
            }
        }
    }
}
