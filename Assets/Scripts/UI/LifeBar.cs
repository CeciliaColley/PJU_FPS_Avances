using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private EnemyDetection enemyDetection;
    public GameObject enemyLifeBar;
    public Image lifeBar;


    private void Start()
    {
        enemyDetection = FindAnyObjectByType<EnemyDetection>();
    }

    private void Update()
    {
        if (enemyDetection != null && enemyLifeBar != null && lifeBar != null)
        {
            ShowEnemiesHealthWhenDetected();
        }

    }

    private void ShowEnemiesHealthWhenDetected()
    {
        if (enemyDetection.enemyDetected)
        {
            // When enemy dies, catch the exception
            try
            {
                Enemy e = enemyDetection.objectHit.transform.GetComponentInParent<Enemy>();
                lifeBar.fillAmount = e.health / 100;
                enemyLifeBar.SetActive(true);
            }
            catch (NullReferenceException){}
            
        }
        else
        {
            enemyLifeBar.SetActive(false);
        }
    }


}
