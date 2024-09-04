using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomTemplates", menuName = "Dungeon/Room Templates")]
public class RoomTemplate : ScriptableObject // ScriptableObject: https://docs.unity3d.com/Manual/class-ScriptableObject.html
{
    [Header("Templates")]
    public Texture2D[] Templates; // Texture2D: Renderer image of this object (https://docs.unity3d.com/ScriptReference/Texture2D.html)

    [Header("Props")]
    public RoomProp[] PropsData;
}

[Serializable] // https://docs.unity3d.com/ScriptReference/Serializable.html
public class RoomProp
{
    public string Name;
    public Color PropColor;
    public GameObject PropPrefab;
}
