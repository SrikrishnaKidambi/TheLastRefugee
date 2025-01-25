using UnityEngine;
using UnityEngine.UI;

public class FloodScript : MonoBehaviour
{
    public float upVelocity = 0.5f;
    public float maxLimit = 3.5f;
    public float playerHeight = 1.3f;
    [SerializeField] private RainFallScript rainFallScript;

    public float startRiseTime = 60f;
    public float healthReductionInterval = 2f;
    public float floodDamage = 30f;

    private bool isRising = false;
    private float timer = 0f;
    public Text messageText;

    void Start()
    {
        InvokeRepeating(nameof(CheckAndApplyFloodDamage), healthReductionInterval, healthReductionInterval);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= startRiseTime && !isRising)
        {
            isRising = true;
        }

        if (isRising)
        {
            IncreaseFlood();
        }
    }

    void IncreaseFlood()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.y < maxLimit)
        {
            currentPosition.y += upVelocity * Time.deltaTime;

            if (currentPosition.y > maxLimit)
            {
                currentPosition.y = maxLimit;
            }

            transform.position = currentPosition;
        }
        else
        {
            isRising = false;
        }
    }

    void CheckAndApplyFloodDamage()
    {
        float floodHeight = transform.position.y;

        if (floodHeight > playerHeight)
        {
            ApplyFloodDamage();
        }
    }

    void ApplyFloodDamage()
    {
        rainFallScript.playerHealth -= floodDamage;
        rainFallScript.playerHealth = Mathf.Max(rainFallScript.playerHealth, 0); // Ensure health doesn't go below zero

        // Update the health display
        //rainFallScript.UpdateHealthUI();

        Debug.Log($"Flood damage applied. Player health: {rainFallScript.playerHealth}");

        if (rainFallScript.playerHealth <= 0f)
        {
            messageText.text = "Health:0";
            Debug.Log("Player has died!");
            if (rainFallScript.playerHealth - 15 <= 0f)
            {
                rainFallScript.playerHealth = 0f;
            }
            CancelInvoke(nameof(CheckAndApplyFloodDamage));

            // Additional death logic can be added here, e.g., showing a game-over screen
        }
    }
}
