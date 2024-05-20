using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Gun gun;
    private EnemyDetection enemyDetection;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Awake()
    {
        lineRenderer = GetComponentInParent<LineRenderer>();
        gun = FindAnyObjectByType<Gun>();
        enemyDetection = FindAnyObjectByType<EnemyDetection>();

        if (gun != null && enemyDetection != null)
        {
            startPoint = gun.transform.position;
            try
            {
                endPoint = enemyDetection.objectHit.transform.position;
            }
            catch (NullReferenceException)
            {

            }

        }

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}
