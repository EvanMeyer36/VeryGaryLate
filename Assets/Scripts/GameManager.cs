using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float timeLeft = 60.0f; // 60 seconds for the countdown
    public Text scoreText; // Assign in inspector
    public Text timerText; // Assign in inspector

    void Update()
    {
        // Timer countdown
        timeLeft -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.Max(0, Mathf.RoundToInt(timeLeft));

        // End game or add bonus at the end of the level
        if (timeLeft <= 0)
        {
            // End the level or add time bonus to score here
            AddScore(Mathf.RoundToInt(timeLeft)); // Adds remaining time as bonus points
            timeLeft = 0; // Stop the countdown
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
