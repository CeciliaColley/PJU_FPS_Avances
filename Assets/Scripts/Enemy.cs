using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public FirstPersonController player;
    public float health = 100;
    Material material;
    ExplodeWhenDies exploadWhenDies;
    public GameObject blood;
    NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponent<Renderer>().material;
        exploadWhenDies = GetComponent<ExplodeWhenDies>();
        player = FindAnyObjectByType<FirstPersonController>();

        
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    public void ReceiveDamage(float damage, Vector3 position)
    {
        health -= damage;
        if (health < 100)
        {
            ReactToDamage(position);
        }
    }

    public void ReactToDamage(Vector3 position)
    {
        if (health <= 0)
        {
            if (exploadWhenDies)
            {
                exploadWhenDies.Explode();
            }
            Destroy(gameObject);
        }
        else
        {
            Bleed(position);
        }
    }

    public void Bleed(Vector3 position)
    {
        Instantiate(blood, position, Quaternion.identity);
    }
}
