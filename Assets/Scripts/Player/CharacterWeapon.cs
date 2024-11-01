using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] protected Transform weaponPos;

    protected SpriteRenderer sp;
    protected Weapon currentWeapon;

    protected virtual void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        if (sp == null)
        {
            sp = GetComponentInChildren<SpriteRenderer>();
        }
    }

    protected void RotateWeapon(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(direction.x > 0) //facing right
        {
            weaponPos.localScale = Vector3.one;
            currentWeapon.transform.localScale = Vector3.one;
            sp.flipX = false; // character flip
            //currentWeapon.transform.localScale = new Vector3(1, 1, 1);
        }
        else //facing left
        {
            weaponPos.localScale = new Vector3(-1, 1, 1);
            currentWeapon.transform.localScale = new Vector3(-1, -1, 1);
            sp.flipX = true;
            //currentWeapon.transform.localScale = new Vector3(-1, 1, 1);
        }
        currentWeapon.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
