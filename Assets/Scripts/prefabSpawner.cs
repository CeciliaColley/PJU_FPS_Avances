using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn; // Prefab to spawn
    [SerializeField] private float spawnTime = 5.0f;

    private float timeSinceLastSpawn; // Tracks time since the last spawn

    void Update()
    {
        // Update the timer
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn a new prefab
        if (timeSinceLastSpawn >= spawnTime)
        {
            // Spawn the prefab at the current position and rotation
            Instantiate(prefabToSpawn, transform.position, transform.rotation);

            // Reset the timer
            timeSinceLastSpawn = 0f;
        }
    }
}
