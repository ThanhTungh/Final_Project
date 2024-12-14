using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private bool targetInRange;
    private ITakeDamage target; 

    public override void UseWeapon()
    {
        PlayShootAnimation();
        if (targetInRange)
        {
            if (target != null)
            {
                target.TakeDamage(itemWeapon.Damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        ITakeDamage entity = other.GetComponent<ITakeDamage>();
        if (entity != null)
        {
            target = entity;
            targetInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        ITakeDamage entity = other.GetComponent<ITakeDamage>();
        if (entity != null)
        {
            target = null;
            targetInRange = false;
        }
    }
}   
