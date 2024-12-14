using UnityEngine;
public enum WeaponType
{
    Melee,
    Gun
}


[CreateAssetMenu(menuName = "Items/Weapon")]
public class ItemWeapon : ItemData
{
    [Header("Data")]
    public WeaponType WeaponType;

    
    [Header("Settings")]
    public float Damage;
    public float RequireEnergy;

    [Header("Weapon")]
    public Weapon WeaponPrefab;

    public override void Pickup()
    {
        LevelManager.Instance.SelectedPlayer.GetComponent<PlayerWeapon>().EquipWeapon(WeaponPrefab); 
    }

}
