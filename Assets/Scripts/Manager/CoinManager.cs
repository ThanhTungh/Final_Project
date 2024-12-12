using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : PersistentSingleton<CoinManager>
{   
    [Header("Config")]
    [SerializeField] private float initialCoinsTest; 


    public float Coins { get; private set; }
    private const string COIN_KEY = "Coins";

    private void Start() 
    {
        Coins = PlayerPrefs.GetFloat(COIN_KEY, initialCoinsTest); 
    }

    public void AddCoins(float amount)
    {
        Coins += amount;
        PlayerPrefs.SetFloat(COIN_KEY, Coins);
        PlayerPrefs.Save();
    }

    public void RemoveCoins(float amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            PlayerPrefs.SetFloat(COIN_KEY, Coins);
            PlayerPrefs.Save();
        }
    }
}
