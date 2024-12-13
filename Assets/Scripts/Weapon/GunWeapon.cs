using UnityEngine;
public class GunWeapon : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    public override void UseWeapon()
    {
        PlayShootAnimation();

        Projectile projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootPos.position;
        projectile.Direction = shootPos.right;
        if (CharacterParent is PlayerWeapon player)
        {
            projectile.Damage = player.GetDamageUsingCriticalChance();
        }
        else
        {
            projectile.Damage = itemWeapon.Damage;
        }
    }
    public override void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}