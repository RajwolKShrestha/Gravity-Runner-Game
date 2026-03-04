using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1Scene");
        //  Score resets automatically via OnSceneLoaded in ScoreManagerScript
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
        //  Score UI hidden automatically
    }

    public void ClearHighScore()
    {
        ScoreManagerScript.ClearHighScore();
    }
}