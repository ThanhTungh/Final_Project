using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [Header("Config")]
    public string ID;
    public Sprite Icon;
    public virtual void Pickup()
    {

    }

}
