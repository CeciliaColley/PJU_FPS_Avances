using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyDetection : MonoBehaviour
{
    [Tooltip("Layer with object's to detect.")]
    public LayerMask enemies;
    
    [HideInInspector]
    public RaycastHit objectHit;
    public bool enemyDetected;
    private Camera look;


    void Start()
    {
        look = transform.parent.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Vector3 aim = look.transform.position;
        if (Physics.Raycast(aim, look.transform.forward, out objectHit, Mathf.Infinity, enemies))
        {
            enemyDetected = true;
        }
        else { enemyDetected = false; }
    }
}
