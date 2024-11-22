using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI damageTMP;

    public void SetDamage(float value)
    {
        damageTMP.text = value.ToString();
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }

}
