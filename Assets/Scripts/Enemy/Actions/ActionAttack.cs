using UnityEngine;

public class ActionAttack : FSMAction
{
    [Header("Config")]
    [SerializeField] private float timeBtwAttacks;

    private EnemyBrain enemy;
    private EnemyWeapon enemyWeapon;
    private float attackTimer;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
        enemyWeapon = GetComponent<EnemyWeapon>();
    }

    private void Start() 
    {
        attackTimer = timeBtwAttacks;
    }

    public override void Act()
    {
        if (enemy.Player == null) return;
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            SoundManager.Instance.PlaySFX(SoundName.EnemyShoot);
            enemyWeapon.UseWeapon();
            attackTimer = timeBtwAttacks;
        }
    }
}