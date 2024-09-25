using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform weaponPos;

    private PlayerActions actions; 
    private PlayerEnergy playerEnergy;
    private PlayerMovement playerMovement;
    private Weapon currentWeapon;

    private void Awake()
    {
        actions = new PlayerActions();
        playerEnergy = GetComponent<PlayerEnergy>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    void Start()
    {
        actions.Weapon.Shoot.performed += context => ShootWeapon();
        CreateWeapon(initialWeapon);
    }

    void Update()
    {
        if (playerMovement.MoveDirection != Vector2.zero)
        {
            RotateWeapon(playerMovement.MoveDirection);
        }
    }
    private void CreateWeapon(Weapon weaponPrefab)
    {
        currentWeapon = Instantiate(weaponPrefab, weaponPos.position, Quaternion.identity, weaponPos);
        
    }
    private void ShootWeapon()
    {
        if (currentWeapon == null)//khong co weapon nao thi return
        {
            return;
        }
        if (CanUseWeapon() == false)//khong co energy hoac khong the su dung weapon thi return
        {
            return;
        }
        currentWeapon.UseWeapon();
        playerEnergy.UseEnergy(currentWeapon.ItemWeapon.RequireEnergy);
        //acces require energy de tru energy khi dung weapon
    }

    private void RotateWeapon(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(direction.x > 0)//facing right
        {
            weaponPos.localScale = Vector3.one;
            currentWeapon.transform.localScale = Vector3.one;

            //currentWeapon.transform.localScale = new Vector3(1, 1, 1);
        }
        else//facing left
        {
            weaponPos.localScale = new Vector3(-1, 1, 1);
            currentWeapon.transform.localScale = new Vector3(-1, -1, 1);

            //currentWeapon.transform.localScale = new Vector3(-1, 1, 1);
        }
        currentWeapon.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    private bool CanUseWeapon()
    {
        if(currentWeapon.ItemWeapon.WeaponType == WeaponType.Gun && playerEnergy.CanUseEnergy)
        {
            return true;
        }
        if(currentWeapon.ItemWeapon.WeaponType == WeaponType.Melee)
        {
            return true;
        }
        return false;
    }
    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }
}
