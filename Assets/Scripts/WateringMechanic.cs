using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringMechanic : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer; // The layer mask for enemies
    [SerializeField] private float radius = 100.0f;
    [SerializeField] private Vector3 center;
    [SerializeField] private string winMessage;
    [SerializeField] private string loseMessage;
    [SerializeField] private GameObject notification;

    private void OnEnable()
    {
        if (!PlayerState.Instance.wateredPlants)
        {
            StartCoroutine(CheckForEnemiesInArena());
        }
        
    }

    private void Update()
    {
        if (CheckForEnemies())
        {
            PlayerState.Instance.wateredPlants = false;
        }
        else
        {
            PlayerState.Instance.wateredPlants = true;
        }
    }

    private IEnumerator CheckForEnemiesInArena()
    {
        yield return new WaitUntil(() => PlayerState.Instance.isInArena && PlayerState.Instance.wateredPlants);
        CameraSwitcher cameraSwitcher = GetComponent<CameraSwitcher>();
        if (cameraSwitcher != null)
        {
            StartCoroutine(cameraSwitcher.SwitchCamera());
        }
        PlayerState.Instance.questNames.Add(winMessage);
        PlayerState.Instance.questNames.Remove(loseMessage);
        if (notification != null)
        {
            notification.SetActive(true);
        }
    }

    private bool CheckForEnemies()
    {
        // I found this in a YT tutorial. It seems really convoluted to me. There has to be a better way. I just want to know if any enemies are active D:

        // Check for colliders within the specified radius on the enemy layer
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, enemyLayer);

        // Check if any colliders were found
        if (hitColliders.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
