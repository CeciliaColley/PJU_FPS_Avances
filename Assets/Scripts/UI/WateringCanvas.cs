using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WateringCanvas : MonoBehaviour
{

    [SerializeField] private Image lifeBar;
    [SerializeField] private Image waterBar;
    [SerializeField] private Gun gun;

    private void Update()
    {
        lifeBar.fillAmount = PlayerState.Instance.health / PlayerState.Instance.maxHealth;
        waterBar.fillAmount = gun.bullets / gun.maxBullets;
    }

}
