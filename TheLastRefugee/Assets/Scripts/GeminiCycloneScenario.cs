using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class GeminiCycloneScneario : MonoBehaviour
{
    [Header("JSON API Configuration")]
    public TextAsset jsonApi;
    private string apiKey = "";
    private string apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent";

    [Header("UI Component")]
    public TMP_Text uiText;

    [Header("Rainfall Script Reference")]
    public RainFallScript rainFallScript; // Reference to the RainFallScript
    public StopwatchTimer timerScript;
    public CycloneScenario cyclonescneario;

    private void Start()
    {
        // Load the API key from the JSON file
        UnityAndGeminiKey jsonApiKey = JsonUtility.FromJson<UnityAndGeminiKey>(jsonApi.text);
        apiKey = jsonApiKey.key;

        // Generate the player's performance prompt and send it to Gemini
        string performancePrompt = GeneratePlayerPerformancePrompt();
        StartCoroutine(SendPromptRequestToGemini(performancePrompt));
    }

    private string GeneratePlayerPerformancePrompt()
    {
        // Retrieve player data from the RainFallScript
        //bool hasMedicine = rainFallScript.hasMedicine;
        bool hasRainCoat = rainFallScript.hasRainCoat;
        //bool hasTorch = rainFallScript.hasTorch;
        //float playerHealth = rainFallScript.playerHealth;

        // Craft a dynamic prompt summarizing the player's actions
        string prompt = "Evaluate the player's performance in the following scenario:\n";
        prompt += $"The player was caught in a cyclone with huge winds and had to collect his marksheet from the school as that is the last day for collection. This is the task to be completed.\n";
        prompt += $"The player collected the marksheet: {(cyclonescneario.collector.isCollected ? "Yes" : "No")}.\n";
        prompt += $"The player may die is : {(cyclonescneario.isGameOver ? "Yes" : "No")}.\n";
        prompt += $"The player collected a raincoat: {(hasRainCoat ? "Yes" : "No")}.\n";
        //prompt += $"The player's health is {playerHealth}.\n";
        prompt += $"The time remaining for the player is {timerScript.timeRemaining}";
        prompt += "The player may die due to the falling of the trees or due to falling of street lights or wind mills on him due to heavy winds";
        prompt += "Based on these details, evaluate the player's performance and give a score out of 100. Include a detailed summary appreciating the precautionary measures he took andwhat could have been taken for better escape of the disaster.";
        prompt += "Focus more on the aspect of educating the player with possible methods of the survival rather than criticizing as this is being generated in a game";
        prompt += "The length of the response should be about 7 to 8 lines at maximum and summary should be very clear and should create an impact on the player";
        prompt += "The response should be displayed in such a way that first part should be acknowledging and appreciating the measures describing them and steps taken by the player and teh remaining part should be the precautions missed by the player.";
        prompt += "The response should be in the form of bullet points and thought provoking";
        prompt += "Also include the score you evalauated in the starting point and remove the stars in the response";

        return prompt;
    }

    public IEnumerator SendPromptRequestToGemini(string promptText)
    {
        Debug.Log(promptText+"is used for the response from gemini");
        string url = $"{apiEndpoint}?key={apiKey}";

        // Format the JSON request with the generated prompt
        string jsonData = "{\"contents\": [{\"parts\": [{\"text\": \"" + promptText + "\"}]}]}";
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        // Create a UnityWebRequest with the JSON data
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                uiText.text = "Error: Unable to fetch response!";
            }
            else
            {
                Debug.Log("Request complete!");
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                if (response.candidates.Length > 0 && response.candidates[0].content.parts.Length > 0)
                {
                    // Retrieve the response text
                    string text = response.candidates[0].content.parts[0].text;

                    // Display the response in the UI text component
                    Debug.Log(text);
                    uiText.text = text;
                }
                else
                {
                    Debug.Log("No text found.");
                    uiText.text = "No response received!";
                }
            }
        }
    }
}
