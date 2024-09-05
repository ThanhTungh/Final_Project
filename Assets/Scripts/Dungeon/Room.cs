using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public static event Action<Room> OnPlayerEnterEvent; // Events.UnityAction: https://docs.unity3d.com/ScriptReference/Events.UnityAction.html


    [Header("Config")]
    [SerializeField] private bool useDebug;
    [SerializeField] private RoomType roomType;

    [Header("Grid")]
    [SerializeField] private Tilemap extraTilemap; // https://learn.unity.com/tutorial/introduction-to-tilemaps#5f35935dedbc2a0894536cfb
    
    [Header("Doors")]
    [SerializeField] private Transform[] posDoorNS;
    [SerializeField] private Transform[] posDoorWE;


    // check Room is completed?
    public bool RoomCompleted { get; set; }


    // Position(Key) - Free/Not Free(Value)
    private Dictionary<Vector3, bool> tiles = new Dictionary<Vector3, bool>();

    private List<Door> doorList = new List<Door>();


    // Start is called before the first frame update
    private void Start()
    {
        GetTiles();
        CreateDoors();
        GenerateRoomUsingTemplate();
    }

    // Get Tiles
    private void GetTiles()
    {
        if (NormalRoom())
        {
            return;
        }

        foreach (Vector3Int tilePos in extraTilemap.cellBounds.allPositionsWithin)  // Vector3Int: https://docs.unity3d.com/ScriptReference/Vector3Int.html

                                                                                    // cellBounds: https://docs.unity3d.com/ScriptReference/Tilemaps.Tilemap-cellBounds.html
                                                                                    // cellBounds.allPositionsWithin: Returns all positions that lie within the specified bounding box. These positions are usually represented as vectors.
        {
            Vector3 position = extraTilemap.CellToWorld(tilePos); // CellToWorld: https://docs.unity3d.com/ScriptReference/GridLayout.CellToWorld.html
            Vector3 newPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, position.z);
            tiles.Add(newPosition, true);
        }
    }
    
    /*

        Open/Close Door

    */

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

    /* -------------------------------------------------------------------------------------------------------- */


    /*

        Generate rooms

    */

    private void GenerateRoomUsingTemplate()
    {
        if (NormalRoom())
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
                Color pixelColor = texture.GetPixel(x, y); // get hexadecimal color from texture
                                                           // GetPixel(): https://docs.unity3d.com/ScriptReference/Texture2D.GetPixel.html

                foreach (RoomProp prop in LevelManager.Instance.RoomTemplates.PropsData)
                {
                    if (pixelColor == prop.PropColor)
                    {
                        GameObject propInstance = Instantiate(prop.PropPrefab, extraTilemap.transform); // Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
                                                                                                        
                                                                                                        // transform: include position, rotation, scale... in Unity

                        propInstance.transform.position = new Vector3(positions[a].x, positions[a].y, 0f);

                        if (tiles.ContainsKey(positions[a])) // ContainsKey: Determines whether the Hashtable contains a specific key.
                        {
                            tiles[positions[a]] = false;
                        }
                    }
                }
            }
        }

    }

    /* -------------------------------------------------------------------------------------------------------- */


    /* 
    
        Create each door with each room in each level

    */
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
        GameObject doorGO = Instantiate(doorPrefab, objTransform.position, Quaternion.identity, objTransform); // Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
                                                                                                               // Quaternion: https://docs.unity3d.com/ScriptReference/Quaternion.html

        Door door = doorGO.GetComponent<Door>();
        doorList.Add(door); // add a clone "door" from Instantiate()
    }

    /* -------------------------------------------------------------------------------------------------------- */

    // Check room type
    private bool NormalRoom()
    {
        return (roomType == RoomType.RoomEntrance || roomType == RoomType.RoomFree);
    }

    // Triggle when player go to room
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


    // Using Gizmos to give visual debugging or setup aids in the Scene view. (Extra GameObject assigned)
    private void OnDrawGizmosSelected() { 
                                            // Gizmos: https://docs.unity3d.com/ScriptReference/Gizmos.html
                                            // OnDrawGizmosSelected(): https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDrawGizmosSelected.html

        if (useDebug == false)
        {
            return;
        }

        if (tiles.Count > 0)
        {
            foreach (KeyValuePair<Vector3, bool> tile in tiles) 
                                                                // KeyValuePair: https://docs.unity.com/ugs/en-us/packages/com.unity.services.multiplayer/1.0/api/Unity.Services.Relay.Models.KeyValuePair
                                                                // KeyValuePair: get value from data structures, ... through key-value 
                                                                
            {
                if (tile.Value) // true
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(tile.Key, Vector3.one * 0.8f); // DrawWireCube: https://docs.unity3d.com/ScriptReference/Gizmos.DrawWireCube.html
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(tile.Key, 0.3f); // DrawSphere: https://docs.unity3d.com/ScriptReference/Gizmos.DrawSphere.html
                }
            }
        }
    }

}
