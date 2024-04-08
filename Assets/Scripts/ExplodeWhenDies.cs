using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeWhenDies : MonoBehaviour
{

    [Header("Explosion Variables")]
    [Tooltip("The force of the explosion (which may be modified by distance).")]
    [SerializeField] private float explosionForce;
    [Tooltip("The radius of the sphere within which the explosion has its effect.")]
    [SerializeField] private float explosionRadius;
    [Tooltip("Adjustment to the apparent position of the explosion to make it seem to lift objects.")]
    [SerializeField] private float upwardsModifier;
    [Tooltip("Reference to the model in pieces")]
    [SerializeField] private GameObject brokenModel;

    public void Explode()
    {
        Instantiate(brokenModel, transform.position, transform.rotation);
        var rbs = brokenModel.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rbs)
        {
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
        }
    }
}
