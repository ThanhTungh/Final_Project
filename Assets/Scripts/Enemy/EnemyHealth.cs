using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
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
        if (enemyHealth <= 0)
        {
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
