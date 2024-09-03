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
    }

    private void GetTiles()
    {
        if (roomType == RoomType.RoomEntrance || roomType == RoomType.RoomFree)
        {
            return;
        }

        foreach (Vector3Int tilePos in extraTilemap.cellBounds.allPositionsWithin)
                                                                                    // cellBounds: https://docs.unity3d.com/ScriptReference/Tilemaps.Tilemap-cellBounds.html
                                                                                    // cellBounds.allPositionsWithin: Returns all positions that lie within the specified bounding box. These positions are usually represented as vectors.
        {
            Vector3 position = extraTilemap.CellToWorld(tilePos); // CellToWorld: https://docs.unity3d.com/ScriptReference/GridLayout.CellToWorld.html
            Vector3 newPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, position.z);
            tiles.Add(newPosition, true);
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
