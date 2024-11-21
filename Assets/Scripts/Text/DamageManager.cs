using UnityEngine;

public class DamageManager : Singleton<DamageManager>
{
    [Header("Config")]
    [SerializeField] private DamageText damageTextPrefab;

    public void ShowDamage(float damage, Transform entityPos)
    {
        Vector3 extraPos = Vector3.right * 0.5f;
        DamageText instance = Instantiate(damageTextPrefab, 
                            entityPos.position + extraPos,
                            Quaternion.identity, entityPos);
        
        instance.SetDamage(damage);

    }
}