using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public static event Action OnPlayerDeadEvent;


    [Header("Player")]
    [SerializeField] private PlayerConfig playerConfig;
    private void Update(){
        //test damage and recover
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RecoverHealth(1);
        }        
    }
    public void RecoverHealth(float amount)
    {
        playerConfig.CurrentHealth += amount;
        if (playerConfig.CurrentHealth > playerConfig.MaxHealth)
        {
            playerConfig.CurrentHealth = playerConfig.MaxHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        DamageManager.Instance.ShowDamage(amount, transform);

        if (playerConfig.Armor > 0)
        {
            //hàm Mathf.Max được sử dụng để đảm bảo rằng các giá trị của Armor và CurrentHealth không bao giờ trở nên âm sau khi bị trừ bởi một giá trị nào đó.
            //pha huy giap truoc roi damage vao mau
            float remainingDamage = amount - playerConfig.Armor;
            playerConfig.Armor = Mathf.Max(playerConfig.Armor - amount, 0f);
            if (remainingDamage > 0)
            {
                playerConfig.CurrentHealth = Mathf.Max(playerConfig.CurrentHealth - remainingDamage, 0f);
            }
        }
        else
        {
            playerConfig.CurrentHealth = Mathf.Max(playerConfig.CurrentHealth - amount, 0f);
        }

        if (playerConfig.CurrentHealth <= 0)
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        OnPlayerDeadEvent?.Invoke();
        Destroy(gameObject);
    }

}
