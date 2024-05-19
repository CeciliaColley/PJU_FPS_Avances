using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockIn : MonoBehaviour
{
    [SerializeField] private GameObject waterCanvas;
    [SerializeField] private CameraSwitcher cameraSwitcher;
    private void OnTriggerEnter(Collider other)
    {
        if (!PlayerState.Instance.isInArena)
        {
            FPSController fPSController = other.GetComponent<FPSController>();

            if (fPSController != null)
            {
                cameraSwitcher.SwitchCameraAndSpawn();
                StartCoroutine(WaitForCoroutineToEnd());
            }
        }
    }

    private IEnumerator WaitForCoroutineToEnd()
    {
        yield return new WaitUntil(() => cameraSwitcher.switchedBack);
        PlayerState.Instance.isInArena = true;
        waterCanvas.SetActive(true);
    }
}
