using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSpot : MonoBehaviour
{
    [SerializeField] private Gun gun;
    private Collider _collider;
    private bool inCollider = false;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inCollider)
        {
            gun.bullets = gun.maxBullets;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FPSController fPSController = other.GetComponent<FPSController>();

        if (fPSController != null)
        {
            inCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inCollider = false;
    }
}
