using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLibrary", menuName = "Dungeon/Library")]
public class DungeonLibrary : ScriptableObject
{
    [Header("Room")]
    public GameObject DoorNS;
    public GameObject DoorWE;
}
