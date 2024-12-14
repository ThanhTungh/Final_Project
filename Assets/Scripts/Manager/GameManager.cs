using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    // public static GameManager Instance;

    public PlayerConfig Player { get; set; }


}
