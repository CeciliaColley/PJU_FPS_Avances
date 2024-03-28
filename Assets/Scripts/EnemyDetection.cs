using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [Tooltip("Layer with object's to detect.")]
    public LayerMask enemies;

    private RaycastHit objectHit;
    private Camera look;

    void Start()
    {
        look = transform.parent.GetComponentInChildren<Camera>();
    }

    public bool DetectEnemy()
    {
        Vector3 aim = look.transform.position;
        if (Physics.Raycast(aim, look.transform.forward, out objectHit, Mathf.Infinity, enemies))
        {
            return true;
        }
        else { return false; }
    }
}
