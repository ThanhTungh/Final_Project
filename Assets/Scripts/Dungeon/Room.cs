using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum RoomType
{
    RoomFree,
    RoomEntrance,
    RoomEnemy,
    RoomBoss
}

public class Room : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool useDebug;
    [SerializeField] private RoomType roomType;

    [Header("Grid")]
    [SerializeField] private Tilemap extraTilemap; // https://learn.unity.com/tutorial/introduction-to-tilemaps#5f35935dedbc2a0894536cfb
    
    // Position(Key) - Free/Not Free(Value)
    private Dictionary<Vector3, bool> tiles = new Dictionary<Vector3, bool>();

    // Start is called before the first frame update
    private void Start()
    {
        GetTiles();
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
    
    // generate rooms
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

    // Check room type
    private bool NormalRoom()
    {
        return (roomType == RoomType.RoomEntrance || roomType == RoomType.RoomFree);
    }

    // Using Gizmos to give visual debugging or setup aids in the Scene view. (Extra Game Object assigned)
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
