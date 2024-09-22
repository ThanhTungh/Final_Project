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
    private PlayerMovement playerMovement;
    private Weapon currentWeapon;

    private void Awake()
    {
        actions = new PlayerActions();
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
        if (currentWeapon == null)
        {
            return;
        }
        currentWeapon.UseWeapon();
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
    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }
}
