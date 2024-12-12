using System;
using UnityEngine;

public class Prop : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float durability;
    
    public void TakeDamage(float amount)
    {
        if (amount >= durability)
        {
            Destroy(gameObject);
        }
    }
}