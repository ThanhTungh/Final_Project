using UnityEngine;

public class PlayerWeapon : CharacterWeapon
{
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
        if(currentWeapon == null)
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
        //Destroy current weapon
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
        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            equippedWeapons[i].gameObject.SetActive(false);
        }
        WeaponIndex = 1 - WeaponIndex;
        currentWeapon = equippedWeapons[WeaponIndex];
        currentWeapon.gameObject.SetActive(true);
        ResetWeaponForChange();
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

    public float GetDamageUsingCriticalChance()
    {
        float damage = currentWeapon.ItemWeapon.Damage;
        float porc = Random.Range(0f, 100f);
        if (porc < config.CriticalChance)
        {
            damage += damage * (config.CriticalDamage / 100f);
            return damage;
        }
        return damage;
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
