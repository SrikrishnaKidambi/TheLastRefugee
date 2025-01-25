using UnityEngine.SceneManagement; // Add this at the top of your script
using UnityEngine;
using TMPro;    // Import TextMeshPro namespace
using UnityEngine.UI;  

public class StopwatchTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("Time in seconds to start the timer. Default is 120 seconds (2 minutes).")]
    public float startTime = 120f;  // Start time in seconds (default 2 minutes)

    public float timeRemaining;
    private bool isTimerRunning = false;
    private bool isPaused = false;

    [Header("UI Elements")]
    [Tooltip("The UI TextMeshPro element to display the timer.")]
    public TMP_Text timerText;  // Reference to the UI TextMeshPro element for displaying the timer
    public Text messageText;
    // Event when the timer finishes
    public event System.Action OnTimerFinished;


    [SerializeField] private UnityAndGeminiV3 unityGeminiScript;
    [SerializeField] private TMP_Text evalautionText;
    [SerializeField] private Image evalautionBackground;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        if (unityGeminiScript != null)
        {
            unityGeminiScript.enabled = false;
        }
        InitializeTimer();  // Initialize the timer with the start time
    }
void Update()
{
    if (isTimerRunning && !isPaused && timeRemaining > 0)
    {
        timeRemaining -= Time.deltaTime;
        UpdateTimerText();
        Debug.Log("Timer Running: " + timeRemaining); // Check the remaining time
    }
    else if (timeRemaining <= 0 && isTimerRunning)
    {
        isTimerRunning = false;
        timeRemaining = 0;
        UpdateTimerText();
        OnTimerEnd();
    }
}

    void UpdateTimerText()
    {
            // Ensure the timer text is visible
    if (timerText != null)
    {
        timerText.gameObject.SetActive(true);  // Ensure it's visible
    }
        // Convert remaining time to minutes and seconds for display
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);  // Display in MM:SS format
    }

    void OnTimerEnd()
    {
        // Trigger custom event or any action after timer ends
        OnTimerFinished?.Invoke();  // Notify any listeners
        messageText.text = "Time Up";
        Invoke(nameof(EnableUnityGeminiScript), 1f);
        //Time.timeScale = 0;
        Debug.Log("Timer Finished!");




    // Load a new scene when the timer reaches 0
    SceneManager.LoadScene("Endscreen");  // Replace "YourSceneName" with the name of your scene


    }
    private void EnableUnityGeminiScript()
    {
        if (evalautionBackground != null)
        {
            evalautionBackground.gameObject.SetActive(true);
        }
        if (evalautionText != null)
        {
            evalautionText.gameObject.SetActive(true);
        }
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
        if (quitButton != null)
        {
            quitButton.gameObject.SetActive(true);
        }
        if (unityGeminiScript != null)
        {
            unityGeminiScript.enabled = true;
            Debug.Log("UnityGemini Script has been enabled");
        }
    }

    #region Timer Controls

    public void InitializeTimer()
    {
        // Initialize the timer to the start time (e.g., 120 seconds) and update text
        timeRemaining = startTime;
        isTimerRunning = true;
        isPaused = false;
        UpdateTimerText();  // Immediately update the text to reflect the starting time
    }

    public void StartTimer()
    {
        if (!isTimerRunning)  // Ensure it starts only once
        {
            isTimerRunning = true;
            isPaused = false;
        }
    }

    public void PauseTimer()
    {
        if (isTimerRunning && !isPaused)
        {
            isPaused = true;  // Pause the timer
        }
    }

    public void ResumeTimer()
    {
        if (isTimerRunning && isPaused)
        {
            isPaused = false;  // Resume the timer
        }
    }

    public void ResetTimer()
    {
        timeRemaining = startTime;  // Reset timer to the starting time (e.g., 120 seconds)
        UpdateTimerText();  // Update the display
    }

    public void SetStartTime(float newStartTime)
    {
        startTime = newStartTime;
        InitializeTimer();  // Re-initialize the timer with the new start time
    }

    #endregion
}
