using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinAnimation : MonoBehaviour
{
    public float frequency;  // Speed of movement
    public float magnitude; // Range of movement
    public Vector3 direction; // Direction of movement
    float initialOffset;
    Vector3 initialPosition;
    Pickup pickup;

    void Start()
    {
        pickup = GetComponent<Pickup>();

        // Save the starting position of the game object
        initialPosition = transform.position;
        initialOffset = Random.Range(0, frequency);
    }

    void Update()
    {

            // Sine function for smooth bobbing effect
            transform.position = initialPosition + direction * Mathf.Sin(Time.time * frequency + initialOffset) * magnitude;
    }
}
