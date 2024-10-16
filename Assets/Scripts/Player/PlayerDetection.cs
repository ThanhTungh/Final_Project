using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float rangeDetection;

    public EnemyHealth EnemyTarget { get; set; }

    private CircleCollider2D myCollider2D;
    private List<EnemyHealth> enemyList = new List<EnemyHealth>();

    private void Awake()
    {
        myCollider2D = GetComponent<CircleCollider2D>();
    }
 
    // Start is called before the first frame update
    void Start()
    {
        myCollider2D.radius = rangeDetection;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemyList.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }

        if (enemy == EnemyTarget)
        {
            EnemyTarget = null;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        int n = enemyList.Count;
        for (int i = 0; i < n; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, enemyList[i].transform.position);
        }
    }
}
