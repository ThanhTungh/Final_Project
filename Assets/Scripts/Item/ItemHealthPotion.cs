using UnityEngine;

[CreateAssetMenu(menuName = "Items/Health Potion")]
public class ItemHealthPotion : ItemData
{
    [Header("Config")]
    [SerializeField] private float health;

    public override void Pickup()
    {
        LevelManager.Instance.SelectedPlayer.GetComponent<PlayerHealth>().RecoverHealth(health);
    }
}