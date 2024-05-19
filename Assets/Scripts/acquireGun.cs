using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acquireGun : MonoBehaviour
{
    [SerializeField] private GameObject trunk;
    [SerializeField] private GameObject gun;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerState.Instance.hasGun = true;
            Debug.Log("Pressed E");
            Destroy( trunk );
            Destroy(gun);
            Destroy(gameObject);
        }
    }
}
