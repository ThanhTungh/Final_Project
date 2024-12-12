using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Transform itemPos;
    
    [Header("Item")]
    [SerializeField] private bool usePredefinedChest;
    [SerializeField] private GameObject predefinedItem;
    private Animator animator;
    private bool openedChest;
    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    private void ShowItem()
    {
        if(usePredefinedChest)
        {
            Instantiate(predefinedItem, transform.position, Quaternion.identity, itemPos);
        }
        else
        {
            GameObject randomItem = LevelManager.Instance.GetRandomItemForChest();
            Instantiate(randomItem, transform.position, Quaternion.identity, itemPos);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(openedChest == true){
            return;
        }
        
        if(other.CompareTag("Player") == false){
            return;
        }
        else{
            animator.SetTrigger("OpenChest");
            ShowItem();
            openedChest = true;
        }
    }
}
