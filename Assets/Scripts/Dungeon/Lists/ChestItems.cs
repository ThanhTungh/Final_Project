using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChestItems", menuName = "Dungeon/Chest Items")]
public class ChestItems : ScriptableObject
{
    public GameObject[] AvailableItems;
}
