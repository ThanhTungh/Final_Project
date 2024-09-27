using System;
using UnityEngine;

public class Prop : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float durability;
    private float counter;
    public void TakeDamage(float amount)
    {
        counter++;
        if (counter >= durability)
        {
            Destroy(gameObject);
        }
    }
}