using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] protected Transform shootPos;
    [SerializeField] protected ItemWeapon itemWeapon;

    public ItemWeapon ItemWeapon 
    {
        get { return itemWeapon; }
    }

    public CharacterWeapon CharacterParent { get; set;}

    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void PlayShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }
    public virtual void UseWeapon()
    {

    }
    public virtual void DestroyWeapon()
    {

    }
}
