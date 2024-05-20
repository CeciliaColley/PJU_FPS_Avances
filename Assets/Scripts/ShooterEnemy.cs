using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public float health = 100; // Variable used by healthbar.
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject head;
    [SerializeField] private Material wateredMaterial;

    private SkinnedMeshRenderer headSMR;
    private SkinnedMeshRenderer bodySMR;

    private void Start()
    {
        headSMR = head.GetComponent<SkinnedMeshRenderer>();
        bodySMR = body.GetComponent<SkinnedMeshRenderer>();
        StartCoroutine(SetSpawnerActive());
    }
    private void OnEnable()
    {
       try
        {
            if (PlayerState.Instance != null && PlayerState.Instance.wateredPlants)
            {
            // Get the array of materials from the SkinnedMeshRenderer
                headSMR = head.GetComponent<SkinnedMeshRenderer>();
                bodySMR = body.GetComponent<SkinnedMeshRenderer>();
                Material[] materials = headSMR.materials;
                // Replace the last material in the array with wateredMaterial
                materials[materials.Length - 1] = wateredMaterial;
                // Assign the modified array back to the SkinnedMeshRenderer
                headSMR.materials = materials;
                bodySMR.material = wateredMaterial;
                spawner.SetActive(false);
            }
       }
        catch (NullReferenceException) { }
        
    }

    private IEnumerator SetSpawnerActive()
    {
        if (!PlayerState.Instance.wateredPlants)
        {
            yield return new WaitUntil(() => PlayerState.Instance.isInArena);
            spawner.SetActive(true);
            StartCoroutine(SetSpawnerInactive());
        }
        
    }

    private IEnumerator SetSpawnerInactive()
    {
        yield return new WaitUntil(() => !PlayerState.Instance.isInArena);
        spawner.SetActive(false);
        StartCoroutine(SetSpawnerActive());
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
            health = 0;
            // This also seems bizarre to me. How can this be the best way of setting a new material D: 
            // Get the array of materials from the SkinnedMeshRenderer
            Material[] materials = headSMR.materials;
            // Replace the last material in the array with wateredMaterial
            materials[materials.Length - 1] = wateredMaterial;
            // Assign the modified array back to the SkinnedMeshRenderer
            headSMR.materials = materials;
            bodySMR.material = wateredMaterial;
            spawner.SetActive(false);
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
