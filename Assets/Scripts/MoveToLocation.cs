using System.Collections;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{
    [SerializeField] private Vector3 desiredLocation;
    [SerializeField] private float moveSpeed = 1.0f; // Speed at which the object moves

    private Vector3 initialLocation;

    private void Start()
    {
        initialLocation = transform.position;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (transform.position != desiredLocation)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                desiredLocation,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        yield return new WaitUntil(() => PlayerState.Instance.wateredPlants);

        while (transform.position != initialLocation)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                initialLocation,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        Destroy(gameObject);
    }
}

