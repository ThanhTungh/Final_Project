using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]//Tao 1 menu de tao 1 ScriptableObject moi
public class PlayerConfig : ScriptableObject//ScriptableObject la 1 data container duoc chua trong asset
{
    public int Level;
    public string Name;
    public Sprite Icon;

}
