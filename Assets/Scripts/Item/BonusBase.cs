using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBase : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed;

    protected Transform player;

    // Update is called once per frame
    private void Update()
    {
        if (player == null) return;
        
        transform.position = Vector3.MoveTowards(transform.position, 
                            player.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) <= 0.1f)
        {
            GetBonus();
            Destroy(gameObject);
        }
    }

    protected virtual void GetBonus()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
        }
    }

}
