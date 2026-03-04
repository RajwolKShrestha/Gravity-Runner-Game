using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManagerScript : MonoBehaviour
{
    [Header("UI References (assign in Inspector)")]
    public Text scoreText;
    public Text highScoreText;

    [Header("Gameplay References")]
    public SpeedManager speedManager;
    public float scoreMultiplier = 0.25f;

    private static ScoreManagerScript instance;
    private float score = 0f;
    private int highScore;

    // ✅ New flag to control scoring
    public bool scoringActive = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Level"))
        {
            ResetScore();
            scoringActive = true; // ✅ Reactivate scoring on new run
            speedManager = Object.FindFirstObjectByType<SpeedManager>();
        }
        else
        {
            if (scoreText != null) scoreText.text = "";
            if (highScoreText != null) highScoreText.text = "";
        }
    }

    void Update()
    {
        if (scoringActive && SceneManager.GetActiveScene().name.Contains("Level"))
        {
            if (speedManager != null)
            {
                score += speedManager.GetSpeed() * scoreMultiplier * Time.deltaTime;
            }

            if (scoreText != null)
            {
                scoreText.text = "Score: " + Mathf.FloorToInt(score);
            }

            if (Mathf.FloorToInt(score) > highScore)
            {
                highScore = Mathf.FloorToInt(score);
                PlayerPrefs.SetInt("HighScore", highScore);
            }

            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + highScore;
            }
        }
    }

    public static void ResetScore()
    {
        if (instance != null)
            instance.score = 0f;
    }

    public static void ClearHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        if (instance != null)
        {
            instance.highScore = 0;

            // ✅ Only update UI if in gameplay scene
            if (SceneManager.GetActiveScene().name.Contains("Level"))
            {
                if (instance.highScoreText != null)
                    instance.highScoreText.text = "High Score: " + instance.highScore;
            }
        }
    }
}