using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    float startingProjectileCount;
    protected override bool CanSpecial()
    {
        return shotDelayed == false && projectilesInMag > 0;
    }

    protected override void SpecialAttack()
    {
        player.ChangeMoveSpeed(-40);
        CameraManager.Instance.ShakeCamera(0.1f, 0.1f);
        Quaternion savedRotation = muzzle.rotation;
        CancelInvoke("Reload");
        anim.SetBool("isReloading", false);
        float newSpread = stats.GetStat(Stat.spread) * 0.4f;
        float halfProjectiles = Mathf.Floor(stats.GetStat(Stat.projectiles) / 2);
        muzzle.Rotate(0, 0, -halfProjectiles * newSpread / 2);
        for (int i = 0; i < halfProjectiles; i++)
        {
            muzzle.Rotate(0, 0, newSpread);
            GameObject projectile = Instantiate(projectileSO.bulletPrefab, muzzle.position, muzzle.rotation);
            projectile.GetComponent<Bullet>().SetGunStats(GetDamage(), knockback, projectileSpeed * 2, destroyAfter, stats.GetStat(Stat.projectileBounce), stats.GetStat(Stat.projectilePiercing), stats.GetStat(Stat.projectileSize));
        }
        muzzle.rotation = savedRotation;
        projectilesInMag -= 1;
        UpdateAmmoText();
        StartFireRateTimer();
    }
}
