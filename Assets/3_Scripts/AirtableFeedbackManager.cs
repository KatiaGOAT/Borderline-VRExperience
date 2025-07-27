using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using TMPro;
public class AirtableFeedbackManager : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField playerNameInput;          // Input field for player name
    public Slider emotionalImpactSlider;        // Slider for emotional impact rating
    public Slider realismSlider;                // Slider for realism rating
    public Slider empathySlider;                // Slider for empathy rating
    public Slider immersionSlider;              // Slider for immersion rating
    public Slider overallScoreSlider;           // Slider for overall score rating    

    [Header("Airtable Settings")]
    //public string apiKey = "YOUR_AIRTABLE_API_KEY";
    public string accessToken = "YOUR_ACCESS_TOKEN";    // Access token for Airtable API authentication
    public string baseID = "YOUR_BASE_ID";              // Base ID for your Airtable base
    public string tableName = "Feedback";               // Name of the table in your Airtable base

    [Header("UI Panels")]
    public GameObject questionsPanel;      // Panel with sliders and submit button
    public GameObject thankYouPanel;       // Panel shown after submission
    public GameObject statusPanel;     // Panel shown for submission progress
    public GameObject retryPanel;      // Panel shown for errors

    // Called when the user presses the Submit button
    public void SubmitFeedback()
    {
        // Retrieve player name from input field
        string playerName = playerNameInput.text;

        // Retrieve slider values and round to nearest integer
        int emotionalImpact = Mathf.RoundToInt(emotionalImpactSlider.value);
        int realism = Mathf.RoundToInt(realismSlider.value);
        int empathy = Mathf.RoundToInt(empathySlider.value);
        int immersion = Mathf.RoundToInt(immersionSlider.value);
        int overallScore = Mathf.RoundToInt(overallScoreSlider.value);

        // Get current date and time in ISO 8601 format (e.g., 2025-07-16T14:35:00Z)
        string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        // Show submission in progress panel
        if (statusPanel != null)
            statusPanel.SetActive(true);

        // Hide other panels
        if (questionsPanel != null)
            questionsPanel.SetActive(false);
        if (thankYouPanel != null)
            thankYouPanel.SetActive(false);
        if (retryPanel != null)
            retryPanel.SetActive(false);


        // Start coroutine to send feedback to Airtable
        StartCoroutine(SendToAirtable(playerName, emotionalImpact, realism, empathy, immersion, overallScore, timestamp));
    }

    // Coroutine to send feedback data to Airtable via a POST request
    IEnumerator SendToAirtable(string name, int emotionalImpact, int realism, int empathy, int immersion, int overallScore, string timestamp)
    {
        // Build the Airtable API endpoint URL
        string airtableURL = $"https://api.airtable.com/v0/{baseID}/{tableName}";

        // Create JSON data 
        string jsonPayload = $@"{{
            ""fields"": {{
                ""PlayerName"": ""{name}"",
                ""EmotionalImpact"": {emotionalImpact},
                ""Realism"": {realism},
                ""Empathy"": {empathy},
                ""Immersion"": {immersion},
                ""OverallScore"": {overallScore},
                ""SubmissionTime"": ""{timestamp}""
            }}
        }}";

        // Create a new UnityWebRequest for POST
        UnityWebRequest www = new UnityWebRequest(airtableURL, "POST");

        // Convert the JSON payload into a byte array
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();

        // Set necessary request headers for Airtable API
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "Bearer " + accessToken);

        // Send the request and wait for a response
        yield return www.SendWebRequest();

        // Check if submission was successful
        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Feedback submitted successfully!");

            // Update UI to show thank you message

            if (statusPanel != null)
                statusPanel.SetActive(false);

            if (questionsPanel != null)
                questionsPanel.SetActive(false);

            if (thankYouPanel != null)
                thankYouPanel.SetActive(true);

            if (retryPanel != null)
                retryPanel.SetActive(false);
        }
        else
        {
            Debug.LogError($"Feedback submission failed: {www.error}");

            if (statusPanel != null)
                statusPanel.SetActive(false);

            if (retryPanel != null)
                retryPanel.SetActive(true);

        }
    }
}
