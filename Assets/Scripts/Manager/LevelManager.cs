using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using Singleton Pattern 
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; 

    [Header("Config")]
    [SerializeField] private RoomTemplate roomTemplates;
    [SerializeField] private DungeonLibrary dungeonLibrary;


    public RoomTemplate RoomTemplates => roomTemplates;
    public DungeonLibrary DungeonLibrary => dungeonLibrary;


    private void Awake() { 
                            // Awake(): https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
        Instance = this;
    }
}
