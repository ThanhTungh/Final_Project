using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] //Tao 1 menu de tao 1 ScriptableObject moi
public class PlayerConfig : ScriptableObject //ScriptableObject la 1 data container duoc chua trong asset
{
    [Header("Key")]
    public string key;

    [Header("Data")]
    public int Level;
    public string Name;
    public Sprite Icon;
    
    [Header("Values")]
    public float CurrentHealth;
    public float MaxHealth;
    public float Armor;
    public float MaxArmor;
    public float Energy;
    public float MaxEnergy;
    public float CriticalChance;
    public float CriticalDamage;

    [Header("Upgrade Values")]
    public float HealthMaxUpgrade;
    public float ArmorhMaxUpgrade;
    public float EnergyMaxUpgrade;
    public float CriticalMaxUpgrade;

    [Header("Extra")]
    public bool Unlocked;
    public float UnlockCost;
    public float UpgradeCost;
    [Range(0, 100f)]
    public int UpgradeMultiplier;

    [Header("Prefab")]
    public GameObject PlayerPrefab;


    public void ResetPlayerStats()
    {
        CurrentHealth = MaxHealth;
        Armor = MaxArmor;
        Energy = MaxEnergy;
    }
}
