using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    public static event Action<Transform> OnEnemyKilledEvent;

    [Header("Config")]
    [SerializeField] private float health;

    private SpriteRenderer sp;
    private float enemyHealth;
    private Color initialColor;
    private Coroutine colorCoroutine;


    private void Awake() 
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = health;
        initialColor = sp.color;
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        ShowDamageColor();
        DamageManager.Instance.ShowDamage(amount, transform);
        if (enemyHealth <= 0)
        {
            OnEnemyKilledEvent?.Invoke(transform);
            Destroy(gameObject);
        }
    }

    private void ShowDamageColor()
    {
        if (colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
        }

        colorCoroutine = StartCoroutine(IETakeDamage());
    }

    private IEnumerator IETakeDamage()
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sp.color = initialColor;
    }
}
