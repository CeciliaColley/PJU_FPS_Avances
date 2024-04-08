using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scope : MonoBehaviour
{
    [Header("Shooting Variables")]
    [Tooltip("Color of the scope when the gun is pointed at an enemy.")]
    public Color enemyDetectedColor;
    [Tooltip("Color of the scope when the gun isn't pointed at an enemy.")]
    public Color noEnemyDetectedColor;

    private Image image;
    private EnemyDetection enemyDetection;


    void Start()
    {
        enemyDetection = FindAnyObjectByType<EnemyDetection>();
        image = GetComponent<Image>();
    }


    void Update()
    {
        ChangeScopeColorIfEnemyDetected();
    }

    private void ChangeScopeColorIfEnemyDetected()
    {
        if (enemyDetection != null)
        {
            if (enemyDetection.enemyDetected)
            {
                image.color = enemyDetectedColor;
            }
            else
            {
                image.color = noEnemyDetectedColor;
            }
        } 
    }
}
