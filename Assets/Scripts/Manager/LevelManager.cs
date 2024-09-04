using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using Singleton Pattern 
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; 
    [SerializeField] private RoomTemplate roomTemplates;
    public RoomTemplate RoomTemplates => roomTemplates;

    private void Awake() { 
                            // Awake(): https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
        Instance = this;
    }
}
