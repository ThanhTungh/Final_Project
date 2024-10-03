using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    // public static UIManager Instance;


    [Header("References")]
    [SerializeField] private PlayerConfig playerConfig;


    [Header("Player UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image armorBar;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private Image energyBar;
    [SerializeField] private TextMeshProUGUI energyText;


    [Header("UI Extra")]
    [SerializeField] private CanvasGroup fadePanel;


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
        //Hiệu ứng mượt mà: Mathf.Lerp (Linear Interpolation) giúp giá trị healthBar.fillAmount thay đổi một cách mượt mà từ giá trị hiện tại đến giá trị mong muốn (playerConfig.CurrentHealth / playerConfig.MaxHealth). Điều này tạo ra một hiệu ứng "trượt" khi thanh máu giảm hoặc tăng, thay vì thay đổi đột ngột.
        //Trải nghiệm người dùng tốt hơn: Hiệu ứng mượt mà này thường mang lại trải nghiệm người dùng tốt hơn, đặc biệt khi giá trị máu thay đổi từ từ, người chơi sẽ cảm thấy thanh máu phản hồi một cách tự nhiên hơn.
        //healthBar.fillAmount = playerConfig.CurrentHealth / playerConfig.MaxHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerConfig.CurrentHealth / playerConfig.MaxHealth, 10f * Time.deltaTime);
        healthText.text = playerConfig.CurrentHealth + " / " + playerConfig.MaxHealth;

        energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, playerConfig.CurrentEnergy / playerConfig.MaxEnergy, 10f * Time.deltaTime);
        energyText.text = playerConfig.CurrentEnergy + " / " + playerConfig.MaxEnergy;

        armorBar.fillAmount = Mathf.Lerp(armorBar.fillAmount, playerConfig.CurrentArmor / playerConfig.MaxArmor, 10f * Time.deltaTime);
        armorText.text = playerConfig.CurrentArmor + " / " + playerConfig.MaxArmor;
    }


    /* 
        Load Fade UI
    */
    public void FadeNewDungeon(float value)
    {
        StartCoroutine(Helpers.IEFade(fadePanel, value, 1.5f));
    }

    /* -------------------------------------------------------------------------------------------------------- */
    
}
