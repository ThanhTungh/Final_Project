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
        projectile.Damage = itemWeapon.Damage;
    }
    public override void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}