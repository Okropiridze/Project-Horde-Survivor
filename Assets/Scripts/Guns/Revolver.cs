using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    float startingProjectileCount;
    protected override bool CanSpecial()
    {
        return shotDelayed == false && projectilesInMag > 0;
    }

    protected override void SpecialAttack()
    {
        startingProjectileCount = projectilesInMag;
        shotDelayed = true;
        CancelInvoke("Reload");
        anim.SetBool("isReloading", false);
        player.EnableCustomOrientation();
        player.ChangeMoveSpeed(-40);
        StartCoroutine(Special());
    }

    IEnumerator Special()
    {
        for (int i = 0; i < startingProjectileCount; i++)
        {
            CameraManager.Instance.ShakeCamera(0.1f, 0.1f);
            float randRot = Random.Range(0, 360f);
            player.SetOrientation(randRot);
            for (int j = 0; j < stats.GetStat(Stat.projectiles); j++)
            {
                randRot += stats.GetStat(Stat.spread);
                player.SetOrientation(randRot);
                GameObject projectile = Instantiate(projectileSO.bulletPrefab, muzzle.position, muzzle.rotation);
                projectile.GetComponent<Bullet>().SetGunStats(GetDamage(), knockback, projectileSpeed, destroyAfter, stats.GetStat(Stat.projectileBounce), stats.GetStat(Stat.projectilePiercing), stats.GetStat(Stat.projectileSize));
            }
            projectilesInMag -= 1;
            UpdateAmmoText();
            yield return new WaitForSeconds(0.05f);
        }
        ResetFireRate();
        player.DisableCustomOrientation();
    }
}
