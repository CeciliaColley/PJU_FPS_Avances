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

    private void Awake()
    {
        playerActions = new IA_PlayerActions();
    }

    private void Start()
    {
        look = GetComponentInChildren<Camera>();
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
        gun = GetComponentInChildren<Gun>();

        if (gun.bullets > 0)
        {
            // Is gun aiming at an enemy
            Vector3 aim = look.transform.position;
            if (Physics.Raycast(aim, look.transform.forward, out objectHit, Mathf.Infinity, gun.enemies))
            {
                // Get the enemy script atatched to the objectHit and to invoke desired methods
                Enemy e = objectHit.transform.GetComponentInParent<Enemy>();
                e.ReceiveDamage(gun.hitDamage);

                gun.bullets--;
            }
            else
            {
                gun.bullets--;
            }
        }
    }
}
