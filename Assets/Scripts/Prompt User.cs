using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptUser : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        FPSController fPSController = other.GetComponent<FPSController>();

        if (fPSController != null)
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}
