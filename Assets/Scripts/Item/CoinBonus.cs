using UnityEngine;

public class CoinBonus : BonusBase
{
    [SerializeField] private float coinsToAdd;

    protected override void GetBonus()
    {
        CoinManager.Instance.AddCoins(coinsToAdd);
    }
}