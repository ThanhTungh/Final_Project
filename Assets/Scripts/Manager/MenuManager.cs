using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private PlayerCreation[] players;

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
        currentPlayer.GetComponent<PlayerMovement>().enabled = true; // currentPlayer => player in CreationPlayer() found
    }
}

[Serializable]
public class PlayerCreation
{
    public PlayerMovement Player;
    public Transform CreationPos;
}
