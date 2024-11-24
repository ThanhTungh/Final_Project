using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
// Using Singleton Pattern 
public class LevelManager : Singleton<LevelManager>
{
    public static event Action OnRoomCompletedEvent;
    // public static LevelManager Instance; 

    [Header("Config")]
    [SerializeField] private RoomTemplate roomTemplates;
    [SerializeField] private DungeonLibrary dungeonLibrary;

    public RoomTemplate RoomTemplates => roomTemplates;
    public DungeonLibrary DungeonLibrary => dungeonLibrary;
    public GameObject SelectedPlayer { get; set; }

    private Room currentRoom;
    private int currentLevelIndex;
    private int currentDungeonIndex;
    private int enemyCounter;
    private GameObject currentDungeonGO;

    private List<GameObject> currentLevelChestItems = new List<GameObject>();

    // private void Awake() // Awake(): https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    // { 
    //     Instance = this;
    // }

    protected override void Awake()
    {
        base.Awake();
        CreatePlayer();
    }

    private void Start()
    {
        CreateDungeon();
    }

    private void CreatePlayer()
    {
        if (GameManager.Instance.Player != null)
        {
            SelectedPlayer = Instantiate(GameManager.Instance.Player.PlayerPrefab);
        }
    }


    private void CreateEnemies()
    {
        int enemyAmount = GetEnemyAmount();
        enemyCounter = enemyAmount;
        for (int i = 0; i < enemyAmount; i++)
        {
            Vector3 tilePos = currentRoom.GetAvailableTilePos();
            EnemyBrain enemy = Instantiate(GetEnemy(), tilePos, 
                        Quaternion.identity, currentRoom.transform);
            enemy.RoomParent = currentRoom;
        }
    }

    private EnemyBrain GetEnemy()
    {
        EnemyBrain[] enemies = dungeonLibrary.Levels[currentLevelIndex].Enemies;
        int randomIndex = Random.Range(0, enemies.Length);
        EnemyBrain randomEnemy = enemies[randomIndex];
        return randomEnemy;
    }

    private int GetEnemyAmount()
    {
        int amount = Random.Range(
            dungeonLibrary.Levels[currentLevelIndex].MinEnemiesPerRoom,
            dungeonLibrary.Levels[currentLevelIndex].MaxEnemiesPerRoom);
        
        return amount;
    }

    /* 
        Create Dungeons with LevelManager
    */
    private void CreateDungeon()
    {
        currentDungeonGO = Instantiate(dungeonLibrary.Levels[currentLevelIndex].Dungeons[currentDungeonIndex], transform);
        currentLevelChestItems = new List<GameObject>(dungeonLibrary.Levels[currentLevelIndex].ChestItems.AvailableItems);
    }

    private void CreateChestInsideRoom()
    {
        Vector3 chestPos = currentRoom.GetAvailableTilePos();
        Instantiate(dungeonLibrary.Chest, chestPos, Quaternion.identity, 
                    currentRoom.transform);
    }

    private void CreateBonusInEnemyPos(Transform enemyPos)
    {
        int bonusAmount = Random.Range(dungeonLibrary.Levels[currentLevelIndex].MinBonusPerEnemy,
                                      dungeonLibrary.Levels[currentLevelIndex].MaxBonusPerEnemy);

        for (int i = 0; i < bonusAmount; i++)
        {
            int randomBonusIndex = Random.Range(0, dungeonLibrary.EnemyBonus.Length);
            Vector3 bonusExtraPos = (Vector3) Random.insideUnitCircle.normalized * dungeonLibrary.BonusCreationRadius;
            Instantiate(dungeonLibrary.EnemyBonus[randomBonusIndex], 
                        enemyPos.position + bonusExtraPos, Quaternion.identity,
                        currentRoom.transform);
        }
    }

    private void CreateTombstonesInEnemyPos(Transform enemyTransform)
    {
        Instantiate(dungeonLibrary.Tombstones, enemyTransform.position,
                    Quaternion.identity, currentRoom.transform);
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
            if (SelectedPlayer != null)
            {
                SelectedPlayer.transform.position = entranceRoom.transform.position;
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
        UIManager.Instance.UpdateLevelText(GetCurrentLevelText());
        UIManager.Instance.FadeNewDungeon(0f);
    }

    /* -------------------------------------------------------------------------------------------------------- */
    

    private string GetCurrentLevelText()
    {
        return $"{dungeonLibrary.Levels[currentLevelIndex].Name} - {currentDungeonIndex + 1}";
    }

    
    /* 

        event Action<Room> OnPlayerEnterEvent

    */

    private void PlayerEnterEventCallback(Room room)
    {
        currentRoom = room;

        if (currentRoom.RoomCompleted == false)
        {
            currentRoom.CloseDoors();
            switch (currentRoom.RoomType)
            {
                case RoomType.RoomEnemy:
                    CreateEnemies();
                    break;
                
                case RoomType.RoomBoss:
                    break;
            }
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

    private void TouchEndGameEventCallback()
    {
        StartCoroutine(IEContinueDungeon());
    }

    private void EnemyKilledCallback(Transform enemyTransform)
    {
        enemyCounter--;
        CreateTombstonesInEnemyPos(enemyTransform);
        CreateBonusInEnemyPos(enemyTransform);
        
        if (enemyCounter <= 0)
        {
            if (currentRoom.RoomCompleted == false)
            {
                enemyCounter = 0;
                currentRoom.SetRoomCompleted();
                CreateChestInsideRoom();
                OnRoomCompletedEvent?.Invoke();
            }
        }
    }

    /* 

        Multicast Delegate: https://learn.unity.com/tutorial/delegates

    */

    private void OnEnable() 
    {
        Room.OnPlayerEnterEvent += PlayerEnterEventCallback;
        Portal.OnPortalEvent += PortalEventCallback;
        EnemyHealth.OnEnemyKilledEvent += EnemyKilledCallback;
        OnePiece.OnTouchEndGameEvent += TouchEndGameEventCallback;
    }

    private void OnDisable() 
    {
        Room.OnPlayerEnterEvent -= PlayerEnterEventCallback;
        Portal.OnPortalEvent -= PortalEventCallback;
        EnemyHealth.OnEnemyKilledEvent -= EnemyKilledCallback;
        OnePiece.OnTouchEndGameEvent -= TouchEndGameEventCallback;
    }

    /* -------------------------------------------------------------------------------------------------------- */
    
}
