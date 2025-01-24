using UnityEngine;

public class RotatingCollectible : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 45f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the GameObject around its local axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}

