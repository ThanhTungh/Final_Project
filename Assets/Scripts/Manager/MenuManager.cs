using System;
using System.Collections;
using System.Collections.Generic;
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


    private SelectablePlayer currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        CreationPlayer();
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
        }
    }

    public void ClickPlayer(SelectablePlayer selectablePlayer)
    {
        currentPlayer = selectablePlayer;
        ShowPlayerStats();
    }

    public void SelectPlayer()
    {
        if (currentPlayer.Config.Unlocked)
        {
            currentPlayer.GetComponent<PlayerMovement>().enabled = true; // currentPlayer => player in CreationPlayer() found
            currentPlayer.Config.ResetPlayerStats();
            ClosePlayerPanel();
        }
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

    }

    public void ClosePlayerPanel()
    {
        playerPanel.SetActive(false);

    }

}

[Serializable]
public class PlayerCreation
{
    public PlayerMovement Player;
    public Transform CreationPos;
}
