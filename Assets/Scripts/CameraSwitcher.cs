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

    public bool switchedBack = false;

    private void Start()
    {
        // Ensure main camera is active and spawn camera is inactive at the start
        mainCamera.gameObject.SetActive(true);
        if (lockedInCamera != null)
        {
            lockedInCamera.gameObject.SetActive(false);
        }
        else
        {
            lockedInCamera = GameObject.FindGameObjectWithTag("LockedInCamera")?.GetComponent<Camera>();
            lockedInCamera.gameObject.SetActive(false);
        }
    }

    public void SwitchCameraAndSpawn()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if (lockedInCamera == null)
        {
            lockedInCamera = GameObject.FindGameObjectWithTag("LockedInCamera")?.GetComponent<Camera>();
        }
        StartCoroutine(SwitchCameraAndSpawnRoutine());
    }

    private IEnumerator SwitchCameraAndSpawnRoutine()
    {
        // Switch to spawn camera
        try
        {
            mainCamera.gameObject.SetActive(false);
            lockedInCamera.gameObject.SetActive(true);
        }
        catch (MissingReferenceException) { }

        Instantiate(fence, spawnPoint, spawnRotation);

        // Wait for a specified time
        yield return new WaitForSeconds(switchBackTime);

        // Switch back to the main camera
        try
        {
            lockedInCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
        }
        catch (MissingReferenceException) { }

        switchedBack = true;
    }

    public IEnumerator SwitchCamera()
    {
        // Switch to spawn camera
        try
        {
            mainCamera.gameObject.SetActive(false);
            lockedInCamera.gameObject.SetActive(true);
        }
        catch (MissingReferenceException) { }


        // Wait for a specified time
        yield return new WaitForSeconds(switchBackTime);

        // Switch back to the main camera
        try
        {
            lockedInCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
        }
        catch (MissingReferenceException) { }

        switchedBack = true;
    }
}
