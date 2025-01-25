using UnityEngine;
using UnityEngine.UI;

public class FloodScript : MonoBehaviour
{
    public float upVelocity = 0.5f;
    public float maxLimit = 3.5f;
    public float playerHeight = 1.3f;
    public bool reachedHighAltitude = false;
    //public float playerHealth = 100f;
    [SerializeField] private RainFallScript rainFallScript;
    [SerializeField] private UnityAndGeminiV3 unityGeminiScript;

    public float startRiseTime = 60f;
    public float healthReductionInterval = 2f;
    public float floodDamage = 30f;
    public Text messageText;

    //public Text healthText; // Reference to the UI Text object

    private bool isRising = false;
    private float timer = 0f;

    void Start()
    {
        if (unityGeminiScript != null)
        {
            unityGeminiScript.enabled = false;
        }
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
            if (!reachedHighAltitude)
            {
                Debug.Log("Player has not reached high altitude. Applying flood damage");
                ApplyFloodDamage();
            }
        }
    }

    void ApplyFloodDamage()
    {
        rainFallScript.playerHealth -= floodDamage;
        rainFallScript.healthText.text = $"Health {rainFallScript.playerHealth}";

        Debug.Log($"Flood damage applied. Player health: {rainFallScript.playerHealth}");

        if (rainFallScript.playerHealth <= 0f)
        {
            rainFallScript.healthText.text = "Health : 0";
            Debug.Log("Player has died!");
            messageText.text = "GAME OVER!! You are wasted.";
            Invoke(nameof(EnableUnityGeminiScript),1f);
             
            CancelInvoke(nameof(CheckAndApplyFloodDamage));
            // Additional death logic here
            //Time.timeScale = 0f; 
        }
    }

    //void UpdateHealthUI()
    //{
    //    if (healthText != null)
    //    {
    //        healthText.text = $"Health: {Mathf.Max(playerHealth, 0):0}";
    //    }
    //}
    private void EnableUnityGeminiScript()
    {
        if (unityGeminiScript != null)
        {
            unityGeminiScript.enabled = true;
            Debug.Log("UnityGemini Script has been enabled");
        }
    }
}