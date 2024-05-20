using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArena : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FPSController fPSController = other.GetComponent<FPSController>();

        if (fPSController != null)
        {
            PlayerState.Instance.isInArena = false;
        }
    }
}
