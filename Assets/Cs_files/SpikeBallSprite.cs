using UnityEngine;

public class SpikeBallSprite : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveDistance = 2f;   // how far up/down it moves
    public float moveSpeed = 2f;      // speed of movement

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Rotate continuously
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Move up and down
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }


}
