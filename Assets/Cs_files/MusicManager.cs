using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Ensure only one MusicManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // keep across scenes
            GetComponent<AudioSource>().Play(); // start music
        }
        else
        {
            Destroy(gameObject); // prevent duplicates
        }
    }

}
