using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Speed of rotation (adjustable in the Inspector)
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the GameObject along its local Y-axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
