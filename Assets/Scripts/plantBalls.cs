using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBalls : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of movement
    [SerializeField] private float distance = 60f; // Distance to move

    private Vector3 targetDirection; // Direction towards the player
    private Vector3 initialPosition; // Initial position

    void Start()
    {
        // Store initial position
        initialPosition = transform.position;

        FPSController player = FindAnyObjectByType<FPSController>();

        // Calculate direction towards the player
        if (player != null)
        {
            targetDirection = (player.transform.position - transform.position).normalized;
        }
    }

    void Update()
    {
        // Move the object towards the player
        transform.position = Vector3.MoveTowards(transform.position, transform.position + targetDirection, speed * Time.deltaTime);

        // Check if the object has moved the desired distance
        if (Vector3.Distance(initialPosition, transform.position) >= distance)
        {
            // If the object has moved the desired distance, destroy it
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        FPSController fPSController = other.GetComponent<FPSController>();
        if (fPSController != null)
        {
            // Reduce the player's health
            PlayerState.Instance.health -= 15;

            // Destroy the object after hitting the player
            Destroy(gameObject);
        }
    }
}