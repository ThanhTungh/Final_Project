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

    public override void Pickup()
    {
        base.Pickup();
        Debug.Log("Picked up weapon: " + name);
    }
}
