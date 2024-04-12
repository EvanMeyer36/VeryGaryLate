using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "MainMenu"; // Name of the scene to load after the video

    private bool hasEnded = false; // Flag to prevent skipping after the video has ended

    void Start()
    {
        videoPlayer.loopPointReached += EndReached; // Subscribe to the loopPointReached event
        videoPlayer.Play();
    }

    void Update()
    {
        if (!hasEnded && Input.GetKeyDown(KeyCode.Space))
        {
            SkipVideo();
        }
    }

    void EndReached(VideoPlayer vp)
    {
        SkipVideo();
    }

    void SkipVideo()
    {
        hasEnded = true; // Set the flag to prevent further skipping
        SceneManager.LoadScene(nextSceneName); // Load the next scene when the video ends
    }
}
