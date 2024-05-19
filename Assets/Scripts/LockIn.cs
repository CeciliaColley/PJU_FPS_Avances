using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockIn : MonoBehaviour
{
    [SerializeField] private CameraSwitcher cameraSwitcher;
    private void OnTriggerEnter(Collider other)
    {
        FPSController fPSController = other.GetComponent<FPSController>();

        if (fPSController != null)
        {
            cameraSwitcher.SwitchCameraAndSpawn();
        }
    }
}
