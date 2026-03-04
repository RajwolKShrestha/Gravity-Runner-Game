using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public SpeedManager speedManager;
    public bool isActive = true; // flag to control camera movement
    void Start()
    {
        isActive = true; // ✅ reset camera movement on scene load
    }
    void Update()
    {
        if (isActive)
        {
            transform.position += Vector3.right * speedManager.GetSpeed() * Time.deltaTime;
        }
    }

    public void StopCamera()
    {
        isActive = false;
    }
}