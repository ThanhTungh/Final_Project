using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLibrary", menuName = "Dungeon/Library")]
public class DungeonLibrary : ScriptableObject
{

    [Header("Levels")]
    public Level[] Levels;


    [Header("Room")]
    public GameObject DoorNS;
    public GameObject DoorWE;
}

[Serializable]
public class Level
{
    public string Name;
    public GameObject[] Dungeons;
}