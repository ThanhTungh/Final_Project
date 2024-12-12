using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomTemplates", menuName = "Dungeon/Room Templates")]
public class RoomTemplate : ScriptableObject 
{
    [Header("Templates")]
    public Texture2D[] Templates; 

    [Header("Props")]
    public RoomProp[] PropsData;
}

[Serializable] 
public class RoomProp
{
    public string Name;
    public Color PropColor;
    public GameObject PropPrefab;
}
