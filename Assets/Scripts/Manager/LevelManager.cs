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

    private Room currentRoom;
    private int currentLevelIndex;
    private int currentDungeonIndex;
    private GameObject currentDungeonGO;

    private void Awake() // Awake(): https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    { 
        Instance = this;
    }

    private void Start()
    {
        CreateDungeon();
    }

    /* 
        Create Dungeons with LevelManager
    */
    private void CreateDungeon()
    {
        currentDungeonGO = Instantiate(dungeonLibrary.Levels[currentLevelIndex].Dungeons[currentDungeonIndex], transform);
    }

    private void ContinueDungeon()
    {
        currentDungeonIndex++;
        if (currentDungeonIndex > dungeonLibrary.Levels[currentLevelIndex].Dungeons.Length - 1)
        {
            currentDungeonIndex = 0;
            currentLevelIndex++;
        }

        Destroy(currentDungeonGO);
        CreateDungeon();
    }


    /* -------------------------------------------------------------------------------------------------------- */


    /* 

        event Action<Room> OnPlayerEnterEvent

    */

    private void PlayerEnterEventCallback(Room room)
    {
        currentRoom = room;

        if (currentRoom.RoomCompleted == false)
        {
            currentRoom.CloseDoors();
        }
    }

    /* -------------------------------------------------------------------------------------------------------- */


    /* 

        event Action OnPortalEvent

    */
    private void PortalEventCallback()
    {
        ContinueDungeon();
    }


    /* 

        Multicast Delegate: https://learn.unity.com/tutorial/delegates

    */

    private void OnEnable() 
    {
        Room.OnPlayerEnterEvent += PlayerEnterEventCallback;
        Portal.OnPortalEvent += PortalEventCallback;
    }

    private void OnDisable() 
    {
        Room.OnPlayerEnterEvent -= PlayerEnterEventCallback;
        Portal.OnPortalEvent -= PortalEventCallback;
    }

    /* -------------------------------------------------------------------------------------------------------- */
    
}
