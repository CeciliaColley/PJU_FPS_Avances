using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Shooting Variables")]
    [Tooltip("Layer where the game objects that can get shot are.")]
    public LayerMask enemies;
    [Tooltip("Ammount of damage dealt by one shot")]
    public float hitDamage = 10;
    [Tooltip("Maximum time's the player can shoot without reloading.")]
    public float bullets;
    public float maxBullets = 100.0f;
    [Tooltip("Smoke line that the gun generates when shooting. Is a prefab.")]
    public GameObject gunShotLine;

    private void Start()
    {
        bullets = maxBullets;
        if (enemies == 0)
        {
            Debug.LogWarning("In the gun script attached to " + gameObject.name + ", the enemies layer mask is not set, so there is nothing for the gun to shoot at.");
        }
    }
}
