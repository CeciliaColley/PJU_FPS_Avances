using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LockIn : MonoBehaviour
{
    [SerializeField] private GameObject waterCanvas;
    [SerializeField] private CameraSwitcher cameraSwitcher;
    private void OnTriggerEnter(Collider other)
    {
        if (!PlayerState.Instance.isInArena)
        {
            FPSController fPSController = other.GetComponent<FPSController>();
            if (cameraSwitcher == null)
            {
                cameraSwitcher = FindAnyObjectByType<CameraSwitcher>();
            }

            if (fPSController != null && cameraSwitcher != null)
            {
                cameraSwitcher.SwitchCameraAndSpawn();
                StartCoroutine(WaitForCoroutineToEnd());
            }
        }
    }

    private IEnumerator WaitForCoroutineToEnd()
    {
        if (cameraSwitcher == null)
        {
            cameraSwitcher = FindAnyObjectByType<CameraSwitcher>();
        }
        yield return new WaitUntil(() => cameraSwitcher.switchedBack);
        PlayerState.Instance.isInArena = true;
        waterCanvas.SetActive(true);
    }
}
