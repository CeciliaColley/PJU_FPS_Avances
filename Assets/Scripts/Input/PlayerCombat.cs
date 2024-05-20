using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerCombat : MonoBehaviour
{
    IA_PlayerActions playerActions;
    
    private RaycastHit objectHit;
    private Camera look;
    private Gun gun;
    private AudioSource gunSound;

    private void Awake()
    {
        playerActions = new IA_PlayerActions();
    }

    private void Start()
    {
        gunSound = GetComponent<AudioSource>();
        look = GetComponentInChildren<Camera>();
        gun = GetComponentInChildren<Gun>();
    }

    private void OnEnable()
    {
        playerActions.GunActions.Enable();

        playerActions.GunActions.Shoot.started += ctx => Shoot();
    }

    private void OnDisable()
    {
        playerActions.GunActions.Disable();

        playerActions.GunActions.Shoot.started -= ctx => Shoot();
    }

    private void Shoot()
    {
        if (gun != null && PlayerState.Instance.hasGun)
        {
            if (gun.bullets > 0)
            {
                // Is gun aiming at an enemy
                Vector3 aim = look.transform.position;
                if (Physics.Raycast(aim, look.transform.forward, out RaycastHit objectHit, Mathf.Infinity, gun.enemies))
                {
                    // Show that the gun was shot
                    Instantiate(gun.gunShotLine);

                    // Try to get the Enemy component
                    try
                    {
                        // Try to get the Enemy component
                        Enemy e = objectHit.transform.GetComponentInParent<Enemy>();
                        e.ReceiveDamage(gun.hitDamage, objectHit.point);
                    }
                    catch (NullReferenceException)
                    {
                    }

                    try
                    {
                        // Try to get the Enemy component
                        ShooterEnemy shooterEnemy = objectHit.transform.GetComponentInParent<ShooterEnemy>();
                        shooterEnemy.ReceiveDamage(gun.hitDamage, objectHit.point);
                    }
                    catch (NullReferenceException)
                    {
                    }
                    

                    gun.bullets--;
                }
                else
                {
                    gun.bullets--;
                }
                gunSound.Play();

            }
            else
            {
                Debug.Log("Out of bullets: " + gun.bullets);
            }
        }
    }
}
