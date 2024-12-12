using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerDungeon : MonoBehaviour
{
    public static ButtonManagerDungeon Instance { get; private set; }
    private PlayerMovement playerMovement;
    private PlayerWeapon playerWeapon;
    private PickableItem currentPickableItem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (LevelManager.Instance.SelectedPlayer != null)
        {
            if (playerMovement == null)
            {
                playerMovement = LevelManager.Instance.SelectedPlayer.GetComponent<PlayerMovement>();
            }

        }

        if (LevelManager.Instance.SelectedPlayer != null)
        {
            if (playerWeapon == null)
            {
                playerWeapon = LevelManager.Instance.SelectedPlayer.GetComponent<PlayerWeapon>();
            }
        }

    }


    public void OnDashButtonClicked()
    {
    
        playerMovement.DashButtonPressed();
    }
    public void OnShootButtonClicked()
    {
        SoundManager.Instance.PlaySFX(SoundName.Shoot);
        playerWeapon.ShootButtonPressed();
    }
    public void OnSwitchWeaponClicked()
    {
        playerWeapon.ChangeButtonPressed();
    }
    public void SetCurrentPickableItem(PickableItem item)
    {
        currentPickableItem = item;
    }

    public void OnPickupButtonClicked()
    {
        if (currentPickableItem != null)
        {
            currentPickableItem.PickupButtonPressed();
        }
    }
}
