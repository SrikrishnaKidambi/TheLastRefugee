using UnityEngine;
using TMPro;
using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class CycloneScenario : MonoBehaviour
{

    //[Header("JSON API Configuration")]
    //public TextAsset jsonApi;
    //private string apiKey = "";
    //private string apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent";
    public TMP_Text messageText;        // Reference to UI text for displaying messages
    public float proximityRange = 5f;  // Range to detect proximity to objects
    [SerializeField] private int proximityCounter = 0;  // Counter to track how many times the player gets close
    private float proximityCooldown = 4f;  // Time in seconds before the proximityCounter can increment again
    private float lastProximityTime = 0f; // Time when proximity was last detected
    public bool isGameOver = false;
    public AudioSource winAudio;       // AudioSource for the winning song
    public Transform player;           // Reference to the player's transform
    public Transform house;            // Reference to the house's transform
    public float houseProximityRange = 3f; // Range to detect if the player is near the house
    public float gameTimeLimit = 180f; // Game time limit in seconds (3 minutes)
    private float startTime;
    [SerializeField] public Collector collector;

    //[SerializeField] private UnityAndGeminiV3 unityAndGemini;
    [SerializeField] private TMP_Text evaluationText;
    [SerializeField] private UnityEngine.UI.Image evaluationImage;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private RainFallScript rainFallScript;
    [SerializeField] private UnityAndGeminiV3 unityAndGemini;
    void Start()
    {
        //UnityAndGeminiKey jsonApiKey = JsonUtility.FromJson<UnityAndGeminiKey>(jsonApi.text);
        //apiKey = jsonApiKey.key;
        
        startTime = Time.time; // Record the start time of the game
    }

    void Update()
    {
        DetectNearbyObjects();

        // Check if game time is over
        if (Time.time - startTime >= gameTimeLimit)
        {
            if (!collector.isCollected || !IsPlayerAtHouse())
            {
                GameOver("Time's up! You failed to collect the items and reach the house.");
            }
        }

        // Check for winning condition
        if (collector.isCollected && IsPlayerAtHouse())
        {
            PlayerWin();
        }
    }

    private void DetectNearbyObjects()
    {
        GameObject[] dangerObjects = GameObject.FindGameObjectsWithTag("Danger");

        foreach (GameObject obj in dangerObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance <= proximityRange)
            {
                HandleProximity(obj);
                break;
            }
        }
    }

    private void HandleProximity(GameObject obj)
    {
        if (Time.time - lastProximityTime >= proximityCooldown)
        {
            proximityCounter++;
            lastProximityTime = Time.time;

            if (proximityCounter == 3 || proximityCounter == 4)
            {
                string parentName = obj.transform.parent.name;

                if (parentName == "Trees")
                {
                    DisplayMessage("You are going close to the trees. Please stay away!");
                }
                else if (parentName == "WindMills")
                {
                    DisplayMessage("You are going close to the poles. Please stay away!");
                }
                else if (parentName == "StreetLights")
                {
                    DisplayMessage("You are going close to the windmills. Please stay away!");
                }
            }
            else if (proximityCounter == 5)
            {
                string parentName = obj.transform.parent.name;
                if (parentName == "Trees")
                {
                    GameOver("Game Over! The tree has fallen due to the cyclone. You may die.");

                }
                else if (parentName == "WindMills")
                {
                    GameOver("Game Over! The windmill has fallen due to the cyclone. You may die.");
                }
                else if (parentName == "StreetLights")
                {
                    GameOver("Game Over! The street light has fallen due to the cyclone. You may die.");
                }
                TriggerFallAnimation(obj);
            }
        }
    }

    private void TriggerFallAnimation(GameObject obj)
    {
        Animator animator = obj.GetComponent<Animator>();
        AudioSource audioSource = obj.GetComponent<AudioSource>();

        if (animator != null)
        {
            string parentName = obj.transform.parent.name;

            if (parentName == "Trees")
            {
                animator.Play("TreeAnimation");
            }
            else if (parentName == "WindMills")
            {
                animator.Play("WindmilAnimation");
            }
            else if (parentName == "StreetLights")
            {
                animator.Play("LightAnimation");
            }
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning($"No AudioSource attached to {obj.name}. Audio will not play.");
            }
        }
    }

    private void DisplayMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        Invoke(nameof(HideMessage), 5f);
    }

    private void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }

    private bool IsPlayerAtHouse()
    {
        float distanceToHouse = Vector3.Distance(player.position, house.position);
        return distanceToHouse <= houseProximityRange;
    }

    private void GameOver(string message)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            DisplayMessage(message);
            Debug.Log("Game Over");

            Invoke(nameof(EnableUnityGeminiScript), 5f);
        }
    }

    private void PlayerWin()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            DisplayMessage("Congratulations! You've reached the house and survived the cyclone!");
            Debug.Log("Player Wins!");
            Invoke(nameof(EnableUnityGeminiScript), 5f);
            if (winAudio != null)
            {
                winAudio.Play();
            }
        }
    }
    private void EnableUnityGeminiScript()
    {
        unityAndGemini.isFromCycloneScript = true;
        if (evaluationImage != null)
        {
            evaluationImage.gameObject.SetActive(true);
        }
        if (evaluationText != null)
        {
            evaluationText.gameObject.SetActive(true);
        }
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
        if (quitButton != null)
        {
            quitButton.gameObject.SetActive(true);
        }
        if (unityAndGemini != null)
        {
            unityAndGemini.enabled = true;
            //Invoke(nameof(EnableUnityGeminiScript), 1f);
            Debug.Log("UnityGemini Script has been enabled");
        }
    }
}