using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerWeapon playerWeapon;
    private PickableItem pickup;

    private void Update()
    {
        if(MenuManager.Instance.CurrentPlayer != null)
        {
            if (playerMovement == null)
            {
                playerMovement = MenuManager.Instance.CurrentPlayer.GetComponent<PlayerMovement>();
            }
        }
        if(MenuManager.Instance.CurrentPlayer != null)
        {
            if (playerWeapon == null)
            {
                playerWeapon = MenuManager.Instance.CurrentPlayer.GetComponent<PlayerWeapon>();
            }
        }

    }
    

    public void OnDashButtonClicked()
    {
        // Gọi hàm Dash của PlayerMovement khi button được nhấn
        playerMovement.DashButtonPressed();
    }
    public void OnShootButtonClicked()
    {
        // Gọi hàm ShootWeapon của PlayerWeapon khi button được nhấn
        playerWeapon.ShootButtonPressed();
    }
    public void OnSwitchWeaponClicked()
    {
        // Gọi hàm ChangeWeapon của PlayerWeapon khi button được nhấn
        playerWeapon.ChangeButtonPressed();
    }
    public void OnPickupButtonClicked()
    {
        // Gọi hàm Pickup của PickableItem khi button được nhấn
        
    }
}
