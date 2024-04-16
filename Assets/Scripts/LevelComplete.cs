using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EndLevelController : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public Text totalScoreText;
    public Button continueButton;
    public string nextSceneName = "MainMenu"; // Name of the scene to load after the video
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Retrieve score data, assuming they are saved in PlayerPrefs
        int score = PlayerPrefs.GetInt("Score", 0);
        int remainingTime = PlayerPrefs.GetInt("RemainingTime", 0);
        int totalScore = score + (remainingTime * 2); // Calculate total score

        // Update UI elements
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (timeText != null) timeText.text = "Time Left: " + remainingTime;
        if (totalScoreText != null) totalScoreText.text = "Total Score: " + totalScore;

        // Set up button listener
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(GoToNextLevel);
        }
    }

    void GoToNextLevel()
{
    // Load the next level based on the calculated index
    SceneManager.LoadScene(nextSceneName); // Load the next scene when the video ends
}

}
