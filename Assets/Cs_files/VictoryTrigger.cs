using UnityEngine;
using System.Collections;

public class VictoryTrigger : MonoBehaviour
{
    public float extraGravity = 10f;      // stronger downward pull
    public float stopDistance = 2f;       // how far forward to move before stopping
    public GameObject victoryPanel;       // assign in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Movement movement = other.GetComponent<Movement>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector3 stopPosition = rb.transform.position + Vector3.right * stopDistance;
                StartCoroutine(MoveAndStop(rb, stopPosition, movement));
            }

            // Stop camera after same forward distance
            if (Camera.main != null)
            {
                CameraMovement cam = Camera.main.GetComponent<CameraMovement>();
                if (cam != null)
                {
                    float targetX = Camera.main.transform.position.x + stopDistance;
                    StartCoroutine(MoveAndStopCamera(cam, targetX));
                }
            }

            // ✅ Stop score updates
            ScoreManagerScript scoreManager = Object.FindFirstObjectByType<ScoreManagerScript>();
            if (scoreManager != null)
            {
                scoreManager.scoringActive = false;
            }
        }
    }

    private IEnumerator MoveAndStop(Rigidbody2D rb, Vector3 stopPosition, Movement movement)
    {
        // Let the player keep moving until reaching stopPosition
        while (rb.transform.position.x < stopPosition.x)
        {
            yield return null;
        }

        // Now disable movement script so it won’t push forward anymore
        if (movement != null)
        {
            movement.enabled = false;
        }

        // Stop horizontal movement and apply downward gravity
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = extraGravity;

        // Show victory panel after short delay
        yield return new WaitForSecondsRealtime(2f);
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private IEnumerator MoveAndStopCamera(CameraMovement cam, float targetX)
    {
        while (Camera.main.transform.position.x < targetX)
        {
            yield return null;
        }

        cam.StopCamera(); // disables camera movement
        cam.speedManager.SetSpeed(0f);
    }
}