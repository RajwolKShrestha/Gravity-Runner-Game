using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGameManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene (Gameplay)
        SceneManager.LoadScene("Level1Scene");
        // Or use index: SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }

}
