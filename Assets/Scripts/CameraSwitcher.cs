using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera lockedInCamera;
    [SerializeField] private GameObject fence;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Quaternion spawnRotation;
    [SerializeField] private float switchBackTime = 5f;  // Time in seconds before switching back to the main camera

    private void Start()
    {
        // Ensure main camera is active and spawn camera is inactive at the start
        mainCamera.gameObject.SetActive(true);
        lockedInCamera.gameObject.SetActive(false);
    }

    public void SwitchCameraAndSpawn()
    {
        StartCoroutine(SwitchCameraAndSpawnRoutine());
    }

    private IEnumerator SwitchCameraAndSpawnRoutine()
    {
        // Switch to spawn camera
        mainCamera.gameObject.SetActive(false);
        lockedInCamera.gameObject.SetActive(true);

        Instantiate(fence, spawnPoint, spawnRotation);

        // Wait for a specified time
        yield return new WaitForSeconds(switchBackTime);

        // Switch back to the main camera
        lockedInCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }
}