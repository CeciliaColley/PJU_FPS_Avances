using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    Material material;
    

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    public void ReceiveDamage(float damage)
    {
        health -= damage;
        material.color = Color.yellow;
    }
}
