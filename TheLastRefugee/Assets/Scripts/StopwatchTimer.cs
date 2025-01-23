using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class StopwatchTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("Time in seconds to start the timer. Default is 120 seconds (2 minutes).")]
    public float startTime = 120f;  // Start time in seconds (default 2 minutes)

    private float timeRemaining;
    private bool isTimerRunning = false;
    private bool isPaused = false;

    [Header("UI Elements")]
    [Tooltip("The UI TextMeshPro element to display the timer.")]
    public TMP_Text timerText;  // Reference to the UI TextMeshPro element for displaying the timer

    // Event when the timer finishes
    public event System.Action OnTimerFinished;

    void Start()
    {
        InitializeTimer();  // Initialize the timer with the start time
    }

    void Update()
    {
        // Update the timer only if it's running and not paused
        if (isTimerRunning && !isPaused && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;  // Decrease time by the frame's time
            UpdateTimerText();  // Update the display text
        }
        else if (timeRemaining <= 0 && isTimerRunning)
        {
            // Stop the timer when it reaches 0
            isTimerRunning = false;
            timeRemaining = 0;  // Ensure the timer doesn't go negative
            UpdateTimerText();
            OnTimerEnd();  // Trigger any actions when the timer finishes
        }
    }

    void UpdateTimerText()
    {
        // Convert remaining time to minutes and seconds for display
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);  // Display in MM:SS format
    }

    void OnTimerEnd()
    {
        // Trigger custom event or any action after timer ends
        OnTimerFinished?.Invoke();  // Notify any listeners
        Debug.Log("Timer Finished!");
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
