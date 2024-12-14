using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerWeapon : CharacterWeapon
{
    public static event Action<Weapon> OnWeaponUIUpdateEvent;

    [Header("Player")]
    [SerializeField] private PlayerConfig config;

    private int WeaponIndex;   //0-1 
    private Weapon[] equippedWeapons = new Weapon[2];

    private PlayerActions actions;
    private PlayerEnergy playerEnergy;
    private PlayerDetection detection;
    private PlayerMovement playerMovement;


    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
        detection = GetComponentInChildren<PlayerDetection>();
        playerEnergy = GetComponent<PlayerEnergy>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        actions.Weapon.Shoot.performed += context => ShootWeapon();
        actions.Interactions.ChangeWeapon.performed += context => ChangeWeapon();
    }

    void Update()
    {
        if (currentWeapon == null)
        {
            return;
        }
        RotatePlayerWeapon();

    }
    private void CreateWeapon(Weapon weaponPrefab)
    {
        currentWeapon = Instantiate(weaponPrefab, weaponPos.position, Quaternion.identity, weaponPos);
        equippedWeapons[WeaponIndex] = currentWeapon;
        equippedWeapons[WeaponIndex].CharacterParent = this;
        OnWeaponUIUpdateEvent?.Invoke(currentWeapon);
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (equippedWeapons[0] == null)
        {
            CreateWeapon(weapon);
            return;
        }
        if (equippedWeapons[1] == null)
        {
            WeaponIndex++;
            equippedWeapons[0].gameObject.SetActive(false);
            CreateWeapon(weapon);
            return;
        }
        currentWeapon.DestroyWeapon();
        equippedWeapons[WeaponIndex] = null;

        CreateWeapon(weapon);

    }
    private void ChangeWeapon()
    {
        if (equippedWeapons[0] == null)
        {
            return;
        }
        if (equippedWeapons[1] == null)
        {
            return;
        }

        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        WeaponIndex = 1 - WeaponIndex;
        currentWeapon = equippedWeapons[WeaponIndex];
        currentWeapon.gameObject.SetActive(true);
        ResetWeaponForChange();
        OnWeaponUIUpdateEvent?.Invoke(currentWeapon);
    }

    private void RotatePlayerWeapon()
    {
        if (playerMovement.MoveDirection != Vector2.zero)
        {
            RotateWeapon(playerMovement.MoveDirection);
        }

        if (detection.EnemyTarget != null)
        {
            Vector3 dirToEnemy = detection.EnemyTarget.transform.position -
                                transform.position;
            RotateWeapon(dirToEnemy);
        }
    }

    private void ShootWeapon()
    {
        if (currentWeapon == null)
        {
            return;
        }
        if (CanUseWeapon() == false)
        {
            return;
        }
        SoundManager.Instance.PlaySFX(SoundName.Shoot);
        currentWeapon.UseWeapon();
        playerEnergy.UseEnergy(currentWeapon.ItemWeapon.RequireEnergy);
    }


    private bool CanUseWeapon()
    {
        if (currentWeapon.ItemWeapon.WeaponType == WeaponType.Gun && playerEnergy.CanUseEnergy)
        {
            return true;
        }
        if (currentWeapon.ItemWeapon.WeaponType == WeaponType.Melee)
        {
            return true;
        }
        return false;
    }

    public void ResetWeaponForChange()
    {
        Transform weaponTransform = currentWeapon.transform;
        weaponTransform.rotation = Quaternion.identity;
        weaponTransform.localScale = Vector3.one;
        weaponPos.rotation = Quaternion.identity;
        weaponPos.localScale = Vector3.one;
        playerMovement.FaceRightDirection();
    }
    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }

    public void ShootButtonPressed()
    {
        ShootWeapon();
    }

    public void ChangeButtonPressed()
    {
        ChangeWeapon();
    }

}
