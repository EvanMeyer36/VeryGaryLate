using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "MainMenu"; // Name of the scene to load after the video

    void Start()
    {
        videoPlayer.loopPointReached += EndReached; // Subscribe to the loopPointReached event
        videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName); // Load the next scene when the video ends
    }
}
