using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
// Using Singleton Pattern 
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; 

    [Header("Temp")]
    [SerializeField] private GameObject player;

    [Header("Config")]
    [SerializeField] private RoomTemplate roomTemplates;
    [SerializeField] private DungeonLibrary dungeonLibrary;

    public GameObject Player => player;
    public RoomTemplate RoomTemplates => roomTemplates;
    public DungeonLibrary DungeonLibrary => dungeonLibrary;

    private Room currentRoom;
    private int currentLevelIndex;
    private int currentDungeonIndex;
    private GameObject currentDungeonGO;

    private List<GameObject> currentLevelChestItems = new List<GameObject>();
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
        currentLevelChestItems = new List<GameObject>(dungeonLibrary.Levels[currentLevelIndex].ChestItems.AvailableItems);
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
        PositionPlayer();
    }


    /* -------------------------------------------------------------------------------------------------------- */


    /* 
        Position Player when start a dungeon
    */
    private void PositionPlayer()
    {
        Room[] dungeonRooms = currentDungeonGO.GetComponentsInChildren<Room>(); // GetComponentsInChildren<T>: https://docs.unity3d.com/ScriptReference/Component.GetComponentsInChildren.html
        Room entranceRoom = null;
        for (int i = 0; i < dungeonRooms.Length; i++)
        {
            if (dungeonRooms[i].RoomType == RoomType.RoomEntrance)
            {
                entranceRoom = dungeonRooms[i];
            }
        }

        if (entranceRoom != null)
        {
            if (player != null)
            {
                player.transform.position = entranceRoom.transform.position;
            }
        }
    }

    /* -------------------------------------------------------------------------------------------------------- */
    public GameObject GetRandomItemForChest()
    {
        int randomIndex = Random.Range(0, currentLevelChestItems.Count);
        GameObject item = currentLevelChestItems[randomIndex];
        currentLevelChestItems.Remove(item);
        return item;
    }

    /* 

        IEnumerator (Coroutine) of Fade alpha in UIManager when changing each dungeon

    */
    private IEnumerator IEContinueDungeon()
    {
        UIManager.Instance.FadeNewDungeon(1f);
        yield return new WaitForSeconds(2f);
        ContinueDungeon();
        UIManager.Instance.FadeNewDungeon(0f);
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
        StartCoroutine(IEContinueDungeon());
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
