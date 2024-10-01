using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string message, Color nameColor)
    {
        text.text = message;
        text.color = nameColor;
    }
}
