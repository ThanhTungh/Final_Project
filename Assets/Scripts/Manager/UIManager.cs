using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    // public static UIManager Instance;

    [Header("Player UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image armorBar;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private Image energyBar;
    [SerializeField] private TextMeshProUGUI energyText;


    [Header("UI Extra")]
    [SerializeField] private CanvasGroup fadePanel;

    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI completedTMP;

    [Header("UI Weapon")]
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponEnergyTMP;
 

    // private void Awake()
    // {
    //     Instance = this;
    // }

    void Update()
    {
        UpdatePlayerUI();
    }

    private void UpdatePlayerUI()
    {
        PlayerConfig playerConfig = GameManager.Instance.Player;
        //Hiệu ứng mượt mà: Mathf.Lerp (Linear Interpolation) giúp giá trị healthBar.fillAmount thay đổi một cách mượt mà từ giá trị hiện tại đến giá trị mong muốn (playerConfig.CurrentHealth / playerConfig.MaxHealth). Điều này tạo ra một hiệu ứng "trượt" khi thanh máu giảm hoặc tăng, thay vì thay đổi đột ngột.
        //Trải nghiệm người dùng tốt hơn: Hiệu ứng mượt mà này thường mang lại trải nghiệm người dùng tốt hơn, đặc biệt khi giá trị máu thay đổi từ từ, người chơi sẽ cảm thấy thanh máu phản hồi một cách tự nhiên hơn.
        //healthBar.fillAmount = playerConfig.CurrentHealth / playerConfig.MaxHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerConfig.CurrentHealth / playerConfig.MaxHealth, 10f * Time.deltaTime);
        healthText.text = playerConfig.CurrentHealth + " / " + playerConfig.MaxHealth;

        energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, playerConfig.Energy / playerConfig.MaxEnergy, 10f * Time.deltaTime);
        energyText.text = playerConfig.Energy + " / " + playerConfig.MaxEnergy;

        armorBar.fillAmount = Mathf.Lerp(armorBar.fillAmount, playerConfig.Armor / playerConfig.MaxArmor, 10f * Time.deltaTime);
        armorText.text = playerConfig.Armor + " / " + playerConfig.MaxArmor;
    }


    /* 
        Load Fade UI
    */
    public void FadeNewDungeon(float value)
    {
        StartCoroutine(Helpers.IEFade(fadePanel, value, 1.5f));
    }

    /* -------------------------------------------------------------------------------------------------------- */
    
    public void UpdateLevelText(string levelText)
    {
        levelTMP.text = levelText;
    }

    private void RoomCompletedCallback()
    {
        StartCoroutine(IERoomCompleted());
    }

    private IEnumerator IERoomCompleted()
    {
        completedTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        completedTMP.gameObject.SetActive(false);
    }

    private void WeaponUIUpdateCallback(Weapon currentWeapon)
    {
        if (weaponPanel.activeSelf == false)
        {
            weaponPanel.SetActive(true);
        }

        weaponEnergyTMP.text = currentWeapon.ItemWeapon.RequireEnergy.ToString();
        weaponIcon.sprite = currentWeapon.ItemWeapon.Icon;
    }

    private void OnEnable() 
    {
        PlayerWeapon.OnWeaponUIUpdateEvent += WeaponUIUpdateCallback;
        LevelManager.OnRoomCompletedEvent += RoomCompletedCallback;
    }

    private void OnDisable() 
    {
        PlayerWeapon.OnWeaponUIUpdateEvent -= WeaponUIUpdateCallback;
        LevelManager.OnRoomCompletedEvent -= RoomCompletedCallback;
    }

}
