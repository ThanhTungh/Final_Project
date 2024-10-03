using UnityEngine;

public class ItemTextManager : Singleton<ItemTextManager>
{
    // public static ItemTextManager Instance;

    [Header("Text")]
    [SerializeField] private ItemText textPrefab;
    
    // private void Awake()
    // {
    //     Instance = this;

    // }\
    
    public ItemText ShowMessage(string message, Color nameColor, Vector3 position)
    {
        ItemText text = Instantiate(textPrefab);
        text.transform.position = position;

        text.SetText(message, nameColor);
        return text;
    }
}