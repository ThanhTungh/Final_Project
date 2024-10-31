using UnityEngine;
public enum WeaponType
{
    Melee,
    Gun
}
public enum WeaponRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "Items/Weapon")]
public class ItemWeapon : ItemData
{
    [Header("Data")]
    public WeaponType WeaponType;
    public WeaponRarity Rarity;
    
    [Header("Settings")]
    public float Damage;
    public float RequireEnergy;
    public float TimeBetweenShots;
    public float MinSpread;
    public float MaxSpread;

    [Header("Weapon")]
    public Weapon WeaponPrefab;

    public override void Pickup()
    {
        LevelManager.Instance.SelectedPlayer.GetComponent<PlayerWeapon>().EquipWeapon(WeaponPrefab); 
    }
}
