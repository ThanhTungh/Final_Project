using System;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private ItemData itemData;
    
    private PlayerActions actions;
    private bool canInteract;

    private void Awake() 
    {
        actions = new PlayerActions();
    }
    private void Start() 
    {
        actions.Interactions.Pickup.performed += ctx => Pickup();    
    }

    private void Pickup()
    {
        if (canInteract)
        {
            itemData.Pickup();
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
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
}