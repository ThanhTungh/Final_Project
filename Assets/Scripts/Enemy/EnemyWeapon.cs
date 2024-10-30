using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : CharacterWeapon
{
    [Header("Config")]
    [SerializeField] private Weapon initialWeapon;

    private void Start() 
    {
        CreateWeapon();
    }

    private void CreateWeapon()
    {
        currentWeapon = Instantiate(initialWeapon, weaponPos.position, 
                                    Quaternion.identity, weaponPos);
        
    }

    private void Update() 
    {
        if (LevelManager.Instance.SelectedPlayer == null) return;
        Vector3 dirToPlayer = LevelManager.Instance.SelectedPlayer.transform.position 
                            - transform.position;
        RotateWeapon(dirToPlayer.normalized);
    }

    public void UseWeapon()
    {
        currentWeapon.UseWeapon();
    }

    
}
