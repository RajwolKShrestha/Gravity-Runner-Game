using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollison : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            Debug.Log("Game Over!");
            GameManagerScript gameManager = Object.FindFirstObjectByType<GameManagerScript>();
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Game Over!");
            GameManagerScript gameManager = Object.FindFirstObjectByType<GameManagerScript>();
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
        }
    }
}
