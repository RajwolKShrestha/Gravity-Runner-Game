using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public float[] SpeedValues;

    public float GetSpeed()
    {
        return SpeedValues[(int)CurrentSpeed];
    }

    public void SetSpeed(float speed)
    {
        // You may want to clamp or validate speed here if needed
        if (SpeedValues != null && SpeedValues.Length > 0)
        {
            // Optionally, update CurrentSpeed based on the closest value
            // For now, just set the first value for demonstration
            SpeedValues[0] = speed;
        }
    }
}
