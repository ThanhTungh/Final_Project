using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : PersistentSingleton<CoinManager>
{   
    [Header("Config")]
    [SerializeField] private float initialCoinsTest; // Test amount coin


    public float Coins { get; private set; }
    private const string COIN_KEY = "Coins";

    private void Start() 
    {
        Coins = PlayerPrefs.GetFloat(COIN_KEY, initialCoinsTest); // Edit => Clear All PlayerPrefs (in tool to reset)
    }

    public void AddCoins(float amount)
    {
        Coins += amount;
        PlayerPrefs.SetFloat(COIN_KEY, Coins); // PlayerPrefs: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
                                               // Unity uses SharedPreferences API to access the PlayerPrefs data and SharedPreferences.Editor API to modify it.
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
