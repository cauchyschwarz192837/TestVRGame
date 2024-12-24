using UnityEngine;

public class ZOscillation : MonoBehaviour
{
    public float amplitude = 1.0f; // Amplitude of the motion
    public float frequency = 1.0f; // Frequency of the motion

    private Vector3 initialPosition; // Initial position of the GameObject

    private void Start()
    {
        // Store the initial position of the GameObject
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new position along the z-axis based on sine wave motion
        float newZ = initialPosition.z + amplitude * Mathf.Sin(Time.time * frequency);

        // Rotate the GameObject by 180 degrees when reaching the amplitude
        if (Mathf.Approximately(newZ, initialPosition.z + amplitude) ||
            Mathf.Approximately(newZ, initialPosition.z - amplitude))
        {
            transform.Rotate(Vector3.up, 180f);
        }

        // Update the GameObject's position
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}

