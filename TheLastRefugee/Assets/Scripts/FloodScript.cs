using UnityEngine;

public class FloodScript : MonoBehaviour
{
    public float upVelocity = 0.5f;          // Speed at which the flood rises
    public float maxLimit = 3.5f;           // Maximum height the flood can reach
    public float playerHeight = 1.3f;       // The height of the player
    public float playerHealth = 100f;       // The player's initial health

    public float startRiseTime = 60f;       // Time to start rising the flood (in seconds)
    public float healthReductionInterval = 2f; // Time interval (in seconds) to check and reduce health
    public float floodDamage = 30f;         // Amount of health to reduce

    private bool isRising = false;          // Flag to check if the flood is rising
    private float timer = 0f;               // Timer to track elapsed time
    private float lastFloodDamageHeight = 1.3f; // Tracks the last height where damage was applied

    void Start()
    {
        // Start invoking the health reduction method every 2 seconds
        InvokeRepeating(nameof(CheckAndApplyFloodDamage), healthReductionInterval, healthReductionInterval);
    }

    void Update()
    {
        // Update the timer based on elapsed time
        timer += Time.deltaTime;

        // Start rising the flood at the specified time
        if (timer >= startRiseTime && !isRising)
        {
            isRising = true; // Start rising the flood
        }

        // Handle rising behavior
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
                currentPosition.y = maxLimit; // Set it exactly to maxLimit
            }

            // Apply the new position to the flood object
            transform.position = currentPosition;
        }
        else
        {
            isRising = false; // Stop rising once we reach the maxLimit
        }
    }

    void CheckAndApplyFloodDamage()
    {
        // Get the current flood height
        float floodHeight = transform.position.y;

        // Check if the flood height exceeds the player height
        if (floodHeight > playerHeight)
        {
            ApplyFloodDamage();
        }
    }

    void ApplyFloodDamage()
    {
        // Reduce player's health
        playerHealth -= floodDamage;

        // Log the health reduction for debugging
        Debug.Log($"Flood damage applied. Player health: {playerHealth}");

        // Handle player death
        if (playerHealth <= 0f)
        {
            Debug.Log("Player has died!");
            CancelInvoke(nameof(CheckAndApplyFloodDamage)); // Stop further damage checks
            // Add additional logic for player death (e.g., respawn, end game, etc.)
        }
    }
}
