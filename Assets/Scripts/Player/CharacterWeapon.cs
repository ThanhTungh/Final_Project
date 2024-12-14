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
        currentWeapon.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
