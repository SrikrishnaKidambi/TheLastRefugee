using UnityEngine;
using UnityEngine.UI;

public class FloodScript : MonoBehaviour
{
    public float upVelocity = 0.5f;
    public float maxLimit = 3.5f;
    public float playerHeight = 1.3f;
    //public float playerHealth = 100f;
    [SerializeField] private RainFallScript rainFallScript;

    public float startRiseTime = 60f;
    public float healthReductionInterval = 2f;
    public float floodDamage = 30f;

    //public Text healthText; // Reference to the UI Text object

    private bool isRising = false;
    private float timer = 0f;

    void Start()
    {
        InvokeRepeating(nameof(CheckAndApplyFloodDamage), healthReductionInterval, healthReductionInterval);
        //UpdateHealthUI(); // Initialize the health display
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
            increaseFlood();
        }
    }

    void increaseFlood()
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
        //UpdateHealthUI(); // Update the health display

        Debug.Log($"Flood damage applied. Player health: {rainFallScript.playerHealth}");

        if (rainFallScript.playerHealth <= 0f)
        {
            Debug.Log("Player has died!");
            CancelInvoke(nameof(CheckAndApplyFloodDamage));
            // Additional death logic here
        }
    }

    //void UpdateHealthUI()
    //{
    //    if (healthText != null)
    //    {
    //        healthText.text = $"Health: {Mathf.Max(playerHealth, 0):0}";
    //    }
    //}
}
