using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float health = 100; // Variable used by healthbar.

    private FPSController player;
    private ExplodeWhenDies exploadWhenDies;
    private NavMeshAgent agent;
    private bool isTouchingPlayer = false;
    private float timer = 0f;
    private Vector3 initialPosition;

    [SerializeField] private GameObject blood;
    [SerializeField] private float damage = 5.0f;
    [SerializeField]private float damageInterval = 2.0f; // Interval in seconds
    [SerializeField] private GameObject happyFlower;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        exploadWhenDies = GetComponent<ExplodeWhenDies>();
        player = FindAnyObjectByType<FPSController>();
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        try
        {
            if (PlayerState.Instance.wateredPlants)
            {
                Instantiate(happyFlower, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        catch (NullReferenceException) { }
        
    }

        private void Update()
    {
        if (PlayerState.Instance.isInArena)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            isTouchingPlayer = false;
            agent.SetDestination(initialPosition);
        }
        if (isTouchingPlayer)
        {
            timer += Time.deltaTime; // Increment timer by the time passed since the last frame

            if (timer >= damageInterval)
            {
                PlayerState.Instance.ReceiveDamage(damage);
                timer = 0f; // Reset timer after applying damage
            }
        }
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
            Instantiate(happyFlower, transform.position, transform.rotation);
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

    private void OnTriggerEnter(Collider other)
    {
        FPSController fPSController = other.GetComponent<FPSController>();

        if (fPSController != null)
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FPSController fPSController = other.GetComponent<FPSController>();

        if (fPSController != null)
        {
            isTouchingPlayer = false;
        }
    }
}
