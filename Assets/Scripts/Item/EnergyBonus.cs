using UnityEngine;

public class EnergyBonus : BonusBase
{
    [SerializeField] private float energy;

    protected override void GetBonus()
    {
        player.GetComponent<PlayerEnergy>().RecoverEnergy(energy);
    }
}