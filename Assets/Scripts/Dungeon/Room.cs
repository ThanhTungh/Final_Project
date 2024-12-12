using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public enum RoomType
{
    RoomFree,
    RoomEntrance,
    RoomEnemy,
    RoomBoss
}

public class Room : MonoBehaviour
{

    public static event Action<Room> OnPlayerEnterEvent; 


    [Header("Config")]
    [SerializeField] private bool useDebug;
    [SerializeField] private RoomType roomType;

    [Header("Grid")]
    [SerializeField] private Tilemap extraTilemap; 
    
    [Header("Doors")]
    [SerializeField] private Transform[] posDoorNS;
    [SerializeField] private Transform[] posDoorWE;

    public bool RoomCompleted { get; set; }

    public RoomType RoomType => roomType; 

    private Dictionary<Vector3, bool> tiles = new Dictionary<Vector3, bool>();

    private List<Door> doorList = new List<Door>();

    private void Start()
    {
        GetTiles();
        CreateDoors();
        GenerateRoomUsingTemplate();
    }

    private void GetTiles()
    {
        if (NormalRoom() || BossRoom())
        {
            return;
        }

        foreach (Vector3Int tilePos in extraTilemap.cellBounds.allPositionsWithin)                                                                             
        {
            Vector3 position = extraTilemap.CellToWorld(tilePos); 
            Vector3 newPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, position.z);
            tiles.Add(newPosition, true);
        }
    }
    
    public void SetRoomCompleted()
    {
        RoomCompleted = true;
        OpenDoors();
    }

    public void CloseDoors()
    {
        for (int i = 0; i < doorList.Count; i++)
        {
            doorList[i].ShowCloseAnimation();
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doorList.Count; i++)
        {
            doorList[i].ShowOpenAnimation();
        }
    }


    private void GenerateRoomUsingTemplate()
    {
        if (NormalRoom() || BossRoom())
        {
            return;
        }

        int randomIndex = Random.Range(0, LevelManager.Instance.RoomTemplates.Templates.Length);
        Texture2D texture = LevelManager.Instance.RoomTemplates.Templates[randomIndex];
        List<Vector3> positions = new List<Vector3>(tiles.Keys);
        
        for (int y = 0, a = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++, a++)
            {
                Color pixelColor = texture.GetPixel(x, y); 

                foreach (RoomProp prop in LevelManager.Instance.RoomTemplates.PropsData)
                {
                    if (pixelColor == prop.PropColor)
                    {
                        GameObject propInstance = Instantiate(prop.PropPrefab, extraTilemap.transform); 

                        propInstance.transform.position = new Vector3(positions[a].x, positions[a].y, 0f);

                        if (tiles.ContainsKey(positions[a])) 
                        {
                            tiles[positions[a]] = false;
                        }
                    }
                }
            }
        }

    }

    public Vector3 GetAvailableTilePos()
    {
        List<Vector3> availableTiles = 
            (from tile 
            in tiles 
            where tile.Value // == true
            select tile.Key).ToList(); // Linq
        
        int randomIndex = Random.Range(0, availableTiles.Count);
        Vector3 pos = availableTiles[randomIndex];
        return pos;
    }




    private void CreateDoors()
    {
        if (posDoorNS.Length > 0)
        {
            for (int i = 0; i < posDoorNS.Length; i++)
            {
                RegisterDoor(LevelManager.Instance.DungeonLibrary.DoorNS, posDoorNS[i]);
            }
        }

        if (posDoorWE.Length > 0)
        {
            for (int i = 0; i < posDoorWE.Length; i++)
            {
                RegisterDoor(LevelManager.Instance.DungeonLibrary.DoorWE, posDoorWE[i]);
            }
        }


    }

    private void RegisterDoor(GameObject doorPrefab, Transform objTransform)
    {
        GameObject doorGO = Instantiate(doorPrefab, objTransform.position, Quaternion.identity, objTransform); 

        Door door = doorGO.GetComponent<Door>();
        doorList.Add(door); 
    }




    private bool NormalRoom()
    {
        return (roomType == RoomType.RoomEntrance || roomType == RoomType.RoomFree);
    }

    private bool BossRoom()
    {
        return (roomType == RoomType.RoomBoss);
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (NormalRoom())
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            if (OnPlayerEnterEvent != null)
            {
                OnPlayerEnterEvent.Invoke(this);
            }
        }
    }


    private void OnDrawGizmosSelected() { 

        if (useDebug == false)
        {
            return;
        }

        if (tiles.Count > 0)
        {
            foreach (KeyValuePair<Vector3, bool> tile in tiles) 
                                                                
            {
                if (tile.Value) // true
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(tile.Key, Vector3.one * 0.8f); 
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(tile.Key, 0.3f); 
                }
            }
        }
    }

}
