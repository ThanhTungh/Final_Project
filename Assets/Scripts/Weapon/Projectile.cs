using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Configure bullet parameters for each different gun

    [Header("Config")]
    [SerializeField] private float speed;
    public Vector3 Direction{get; set;}
    public float Damage { get; set; }

    void Update()
    {
        transform.Translate(Direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<ITakeDamage>() != null)
        {
            other.GetComponent<ITakeDamage>().TakeDamage(Damage);
        }
        Debug.Log(Damage);
        Destroy(gameObject);
    }
}
