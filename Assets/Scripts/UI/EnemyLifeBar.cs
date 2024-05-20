using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar: MonoBehaviour
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
            // When enemy is detected, try to get the health and display it
            try
            {
                // Attempt to get the health of the detected enemy
                Enemy e = enemyDetection.objectHit.transform.GetComponentInParent<Enemy>();
                if (e != null)
                {
                    lifeBar.fillAmount = e.health / 50;
                    enemyLifeBar.SetActive(true);
                }
                else
                {
                    // If it's not of type Enemy, try to get health of ShooterEnemy
                    ShooterEnemy shooterEnemy = enemyDetection.objectHit.transform.GetComponentInParent<ShooterEnemy>();
                    if (shooterEnemy != null)
                    {
                        lifeBar.fillAmount = shooterEnemy.health / 30;
                        enemyLifeBar.SetActive(true);
                    }
                }
            }
            catch (NullReferenceException)
            {
                // Catch null reference exceptions
            }
        }
        else
        {
            enemyLifeBar.SetActive(false);
        }
    }


}
