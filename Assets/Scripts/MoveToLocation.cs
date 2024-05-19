using System.Collections;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{
    [SerializeField] private Vector3 desiredLocation;
    [SerializeField] private float moveSpeed = 1.0f; // Speed at which the object moves

    private void OnEnable()
    {
        // Start the move coroutine when the object is enabled
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (gameObject.transform.position != desiredLocation)
        {
            // Smoothly move the object towards the desired location
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                desiredLocation,
                moveSpeed * Time.deltaTime
            );

            // Wait until the next frame before continuing the loop
            yield return null;
        }
    }
}
