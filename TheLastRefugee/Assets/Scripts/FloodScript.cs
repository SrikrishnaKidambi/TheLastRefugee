using UnityEngine;

public class FloodScript : MonoBehaviour
{
    public float upVelocity = 0.5f;  // Speed at which the flood rises
    public float maxLimit = 3.5f;  // Maximum height the flood can reach

    private bool isRising = false;  // Flag to check if the flood is rising

    void Update()
    {
        // If "F" is pressed, start rising the flood if it's not already rising
        if (Input.GetKeyDown(KeyCode.F) && !isRising)
        {
            isRising = true;  // Start rising the flood
        }

        // If "R" is pressed, the flood lowers
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRising = false;  // Stop the rising (reset the flag)
            lowerFlood();      // Call the function to lower the flood
        }

        // Continuously rise the flood until the maxLimit is reached
        if (isRising)
        {
            increaseFlood();
        }
    }

    void increaseFlood()
    {
        // Get the current position of the flood object
        Vector3 currentPosition = transform.position;

        // If the flood is below the maxLimit, increase the Y position
        if (currentPosition.y < maxLimit)
        {
            // Calculate the change in the Y direction
            currentPosition.y += upVelocity * Time.deltaTime;

            // Ensure the flood doesn't go past the maxLimit
            if (currentPosition.y > maxLimit)
            {
                currentPosition.y = maxLimit;  // Set it exactly to maxLimit
            }

            // Apply the new position to the flood object
            transform.position = currentPosition;
        }
        else
        {
            isRising = false;  // Stop rising once we reach the maxLimit
        }
    }

    void lowerFlood()
    {
        // Get the current position of the flood object
        Vector3 currentPosition = transform.position;

        // Calculate the change in the Y direction for lowering
        currentPosition.y -= upVelocity * Time.deltaTime;

        // Apply the new position to the flood object
        transform.position = currentPosition;
    }
}
