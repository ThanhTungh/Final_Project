using UnityEngine;

public class ItemTextManager : Singleton<ItemTextManager>
{


    [Header("Text")]
    [SerializeField] private ItemText textPrefab;  

    public ItemText ShowMessage(string message, Color nameColor, Vector3 position)
    {
        ItemText text = Instantiate(textPrefab);
        text.transform.position = position;

        text.SetText(message, nameColor);
        return text;
    }
}