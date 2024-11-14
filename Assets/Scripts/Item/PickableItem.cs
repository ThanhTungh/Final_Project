using System;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private ItemData item;

    private PlayerActions actions;
    private bool canInteract;
    private ItemText nameText;
    private ButtonManagerDungeon buttonManager;

    private void Awake() 
    {
        actions = new PlayerActions();
    }
    private void Start() 
    {
        actions.Interactions.Pickup.performed += ctx => Pickup();    
        buttonManager = FindObjectOfType<ButtonManagerDungeon>();
    }

    private void Pickup()
    {
        if (canInteract)
        {
            item.Pickup();
            Destroy(gameObject);
        }
    }
    private void ShowItemName()
    {
        Vector3 textPos = new Vector3(0f, 1f, 0f);
        if (item is ItemWeapon weapon)
        {
            Color itemColor = GameManager.Instance.GetWeaponNameColor(weapon.Rarity);
            nameText = ItemTextManager.Instance.ShowMessage(weapon.ID, itemColor, textPos + transform.position);
        }
        else
        {
            nameText = ItemTextManager.Instance.ShowMessage(item.ID, Color.white, textPos + transform.position);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            ShowItemName();
            buttonManager.SetCurrentPickableItem(this); 
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            Destroy(nameText.gameObject);
            buttonManager.SetCurrentPickableItem(null);
        }
    }

    private void OnEnable() 
    {
        actions.Enable();    
    }
    private void OnDisable() 
    {
        actions.Disable();    
    }
    public void PickupButtonPressed()
    {
        Pickup();
    }
    
}