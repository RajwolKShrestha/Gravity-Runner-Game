using UnityEngine;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 }

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpeedManager speedManager;

    private float gravityDirection = 1f; // 1 = down, -1 = up
    public float baseGravity = 5f;       // gravity strength

    private Camera mainCamera;
    private bool isGameOverTriggered = false; // ✅ new flag

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = baseGravity * gravityDirection;

        mainCamera = Camera.main;
        Time.timeScale = 1f; // ensure time is unfrozen when scene starts
        isGameOverTriggered = false;     // reset flag
    }

    void Update()
    {
        // Constant forward movement
        transform.position += Vector3.right * speedManager.GetSpeed() * Time.deltaTime;

        // Input check
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FlipGravity();
        }

        // Bounds check
        if (!isGameOverTriggered && mainCamera != null)
        {
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

            if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
            {
                TriggerGameOver();
            }
        }
    }

    private void FlipGravity()
    {
        gravityDirection *= -1;
        rb.gravityScale = baseGravity * gravityDirection;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        transform.Rotate(0f, 0f, 180f);
    }

    private void TriggerGameOver()
    {
        if (isGameOverTriggered) return; // ✅ prevent multiple calls
        isGameOverTriggered = true;

        GameManagerScript gm = Object.FindFirstObjectByType<GameManagerScript>();
        if (gm != null)
        {
            gm.GameOver(); // plays beep + shows panel + freezes time
        }
    }
}