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
    public int bullets = 15;

    private void Start()
    {
        if (enemies == 0)
        {
            Debug.LogWarning("In the gun script attached to " + gameObject.name + ", the enemies layer mask is not set, so there is nothing for the gun to shoot at.");
        }
    }
}
