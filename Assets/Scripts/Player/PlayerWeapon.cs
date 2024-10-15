using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Transform weaponPos;

    private PlayerActions actions; 
    private PlayerEnergy playerEnergy;
    private PlayerMovement playerMovement;
    private Weapon currentWeapon;

    private int WeaponIndex;   //0-1 
    private Weapon[] equippedWeapons = new Weapon[2];

    private void Awake()
    {
        actions = new PlayerActions();
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
        if (playerMovement.MoveDirection != Vector2.zero)
        {
            RotateWeapon(playerMovement.MoveDirection);
        }
    }
    private void CreateWeapon(Weapon weaponPrefab)
    {
        currentWeapon = Instantiate(weaponPrefab, weaponPos.position, Quaternion.identity, weaponPos);
        equippedWeapons[WeaponIndex] = currentWeapon;       
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
}
