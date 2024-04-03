using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string nextSceneName = "MainMenu"; // Name of the scene to load after the video
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player has the tag "Player"
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        // Find the GameManager1 component in the scene
        GameManager1 gameManager = FindObjectOfType<GameManager1>();
        if (gameManager != null)
        {
            int score = gameManager.score;
        int time = Mathf.RoundToInt(gameManager.timeLeft);

        // Save the current score and remaining time
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("RemainingTime", time);
        
        // Save the current level index
        SceneManager.LoadScene(nextSceneName); // Load the next scene when the video ends
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        }
        else
        {
            Debug.LogError("GameManager1 not found in the scene.");
        }
    }
}
