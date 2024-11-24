using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : Singleton<MenuManager>
{
    [Header("Config")]
    [SerializeField] private PlayerCreation[] players;

    [Header("UI")]
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private Image playerIcon;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerLevel;
    [SerializeField] private TextMeshProUGUI playerHealthMaxStat;
    [SerializeField] private TextMeshProUGUI playerArmorMaxStat;
    [SerializeField] private TextMeshProUGUI playerEnergyMaxStat;
    [SerializeField] private TextMeshProUGUI playerCriticalMaxStat;
    [SerializeField] private TextMeshProUGUI coinsTMP;
    [SerializeField] private TextMeshProUGUI playerUpgradeCostTMP;
    [SerializeField] private TextMeshProUGUI playerUnlockCostTMP;

    [Header("Bars")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image armorBar;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image criticalBar;

    [Header("Buttons")]
    [SerializeField] private GameObject unlockButton;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private GameObject selectButton;


    public PlayFabManager playFabManager => PlayFabManager.Instance;
    private SelectablePlayer currentPlayer;
    public SelectablePlayer CurrentPlayer => currentPlayer;

    public static string jsonString;

    private void LoadDataPlayer()
    {
        playFabManager.LoadPlayerData(CurrentPlayer.Config.key);
    }

    private void initData()
    {
        PlayerModel playerModel = JsonConvert.DeserializeObject<PlayerModel>(jsonString);
        // Now you can access the deserialized values:
        CurrentPlayer.Config.Level = playerModel.level;
        CurrentPlayer.Config.MaxHealth = playerModel.MaxHealth;
        CurrentPlayer.Config.MaxArmor = playerModel.MaxArmor;
        CurrentPlayer.Config.MaxEnergy = playerModel.MaxEnergy;
        CurrentPlayer.Config.Unlocked = playerModel.unlock;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreationPlayer();
    }

    private void Update()
    {
        coinsTMP.text = CoinManager.Coins.ToString();
    }

    private void CreationPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            PlayerMovement player = Instantiate(players[i].Player, players[i].CreationPos.position,
                                                Quaternion.identity, players[i].CreationPos);
            // each player has movement so need call "PlayerMovement" class

            // Create a clone of an object in prefab,... (https://docs.unity3d.com/ScriptReference/Object.Instantiate.html)
            player.enabled = false;
            var playerRb = player.GetComponent<Rigidbody2D>();
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void ClickPlayer(SelectablePlayer selectablePlayer)
    {
        currentPlayer = selectablePlayer;
        VerifyPlayer();
        ShowPlayerStats();
    }

    public void SelectPlayer()
    {
        if (currentPlayer.Config.Unlocked)
        {
            currentPlayer.GetComponent<PlayerMovement>().enabled = true; // currentPlayer => player in CreationPlayer() found
            currentPlayer.Config.ResetPlayerStats();
            GameManager.Instance.Player = currentPlayer.Config;
            ClosePlayerPanel();
        }
    }

    public void UpgradePlayer()
    {
        if (CoinManager.Coins >= currentPlayer.Config.UpgradeCost)
        {
            CoinManager.Instance.RemoveCoins(currentPlayer.Config.UpgradeCost);
            UpgradePlayerStats();
            ShowPlayerStats();
        }
    }

    public void UnlockPlayer()
    {
        if (CoinManager.Coins >= currentPlayer.Config.UnlockCost)
        {
            CoinManager.Instance.RemoveCoins(currentPlayer.Config.UnlockCost);
            currentPlayer.Config.Unlocked = true;
            VerifyPlayer();
            ShowPlayerStats();
        }
    }

    private void UpgradePlayerStats()
    {
        PlayerConfig config = currentPlayer.Config;
        config.Level++;
        config.MaxHealth++;
        config.MaxArmor++;
        config.MaxEnergy += 10f;
        config.CriticalChance += 2f;
        config.CriticalDamage += 5f;

        // Character combat index limit
        config.MaxHealth = Math.Min(config.MaxHealth, config.HealthMaxUpgrade);
        config.MaxArmor = Math.Min(config.MaxArmor, config.ArmorhMaxUpgrade);
        config.MaxEnergy = Math.Min(config.MaxEnergy, config.EnergyMaxUpgrade);
        config.CriticalChance = Math.Min(config.CriticalChance, config.CriticalMaxUpgrade);


        float upgrade = config.UpgradeCost;
        config.UpgradeCost = upgrade + (upgrade * (config.UpgradeMultiplier / 100f));
    }

    private void ShowPlayerStats()
    {
        playerPanel.SetActive(true);
        playerIcon.sprite = currentPlayer.Config.Icon;
        playerName.text = currentPlayer.Config.Name;
        playerLevel.text = $"Level {currentPlayer.Config.Level}";
        playerHealthMaxStat.text = currentPlayer.Config.MaxHealth.ToString();
        playerArmorMaxStat.text = currentPlayer.Config.MaxArmor.ToString();
        playerEnergyMaxStat.text = currentPlayer.Config.MaxEnergy.ToString();
        playerCriticalMaxStat.text = currentPlayer.Config.CriticalChance.ToString();

        playerUnlockCostTMP.text = currentPlayer.Config.UnlockCost.ToString();
        playerUpgradeCostTMP.text = currentPlayer.Config.UpgradeCost.ToString();

        // Update Bars
        healthBar.fillAmount = currentPlayer.Config.MaxHealth / currentPlayer.Config.HealthMaxUpgrade;
        armorBar.fillAmount = currentPlayer.Config.MaxArmor / currentPlayer.Config.ArmorhMaxUpgrade;
        energyBar.fillAmount = currentPlayer.Config.MaxEnergy / currentPlayer.Config.EnergyMaxUpgrade;
        criticalBar.fillAmount = currentPlayer.Config.CriticalChance / currentPlayer.Config.CriticalMaxUpgrade;
        
        LoadDataPlayer();
        Invoke("initData", 0.5f);
        
    }

    private void VerifyPlayer()
    {
        if (currentPlayer.Config.Unlocked == false)
        {
            upgradeButton.SetActive(false);
            selectButton.SetActive(false);
            unlockButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(true);
            selectButton.SetActive(true);
            unlockButton.SetActive(false);
            var playerRb = currentPlayer.GetComponent<Rigidbody2D>();
            playerRb.constraints = RigidbodyConstraints2D.None;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void ClosePlayerPanel()
    {
        playerPanel.SetActive(false);
        SaveDataPlayer(CurrentPlayer.Config);
    }



    private void SaveDataPlayer(PlayerConfig playerConfig)
    {
        PlayerModel playerModel = new PlayerModel();
        playerModel.level = playerConfig.Level;
        playerModel.MaxHealth = playerConfig.MaxHealth;
        playerModel.MaxArmor = playerConfig.MaxArmor;
        playerModel.MaxEnergy = playerConfig.MaxEnergy;
        playerModel.unlock = playerConfig.Unlocked;

        var key = playerConfig.key;
        string json = JsonConvert.SerializeObject(playerModel);
        Debug.Log(json + "----");
        playFabManager.SavePlayerData(key, json);
    }


}

[Serializable]
public class PlayerCreation
{
    public PlayerMovement Player;
    public Transform CreationPos;
}


