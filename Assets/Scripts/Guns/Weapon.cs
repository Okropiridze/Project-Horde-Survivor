using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected StatsSO stats;
    [SerializeField] protected ProjectileSO projectileSO;
    [SerializeField] protected Animator anim;
    protected PlayerController player;
    private SpriteRenderer spriteRenderer;
    [SerializeField] protected Transform muzzle;
    protected bool shotDelayed = false;
    protected bool isReloading = false;
    protected float projectilesInMag;

    protected float damage;
    protected float knockback;
    protected float projectileSpeed;
    protected float destroyAfter;

    private DamageDealer dealer = DamageDealer.weapon;


    public void SceneEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //stats = UpgradeEventsManager.Instance.GetWeaponStats();
        projectilesInMag = stats.GetStat(Stat.magSize);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateAmmoText();

        damage = stats.GetStat(Stat.damage);
        knockback = stats.GetStat(Stat.knockback);
        projectileSpeed = stats.GetStat(Stat.projectileSpeed);
        destroyAfter = stats.GetStat(Stat.destroyBulletAfter);
    }

    public void ChangeProjectileSize()
    {
        transform.localScale = Vector3.one * stats.GetStat(Stat.projectileSize);
    }

    public virtual void WishSpecial()
    {
        if (CanSpecial())
        {
            SpecialAttack();
        }
    }

    public virtual void WishShoot()
    {
        if (CanShoot())
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        return shotDelayed == false && projectilesInMag > 0;
    }

    protected virtual bool CanSpecial()
    {
        return true;
    }

    public virtual void WishRealod()
    {
        if (shotDelayed == false && projectilesInMag != stats.GetStat(Stat.magSize))
        {
            StartReload();
        }
    }

    protected float GetDamage()
    {
        float runAndGunBonus = stats.GetStat(Stat.damage) * stats.GetStat(Stat.runAndGunBonus) / 100;
        float damage = stats.GetStat(Stat.damage);
        if (player.IsMoving()) return damage + runAndGunBonus;
        else return damage;
    }

    private void Shoot()
    {
        player.ChangeMoveSpeed(-40);
        CameraManager.Instance.ShakeCamera(0.1f, 0.1f);
        Quaternion savedRotation = muzzle.rotation;
        CancelInvoke("Reload");
        anim.SetBool("isReloading", false);
        muzzle.Rotate(0, 0, -stats.GetStat(Stat.projectiles) * stats.GetStat(Stat.spread) / 2);
        for (int i = 0;i < stats.GetStat(Stat.projectiles); i++)
        {
            muzzle.Rotate(0, 0, stats.GetStat(Stat.spread));
            GameObject projectile = Instantiate(projectileSO.bulletPrefab, muzzle.position, muzzle.rotation);
            projectile.GetComponent<Bullet>().SetGunStats(GetDamage(), stats.GetStat(Stat.knockback), stats.GetStat(Stat.projectileSpeed), destroyAfter, stats.GetStat(Stat.projectileBounce), stats.GetStat(Stat.projectilePiercing), stats.GetStat(Stat.projectileSize));
        }
        muzzle.rotation = savedRotation;
        projectilesInMag -= 1;
        List<OnEventFunctionalities> onEventFunctionalities = UpgradeEventsManager.Instance.GetOnEventFunctionalities();
        foreach (OnEventFunctionalities eventFunc in onEventFunctionalities)
        {
            eventFunc.AfterShot();
        }
        UpdateAmmoText();
        StartFireRateTimer();

    }

    protected virtual void SpecialAttack()
    {

    }

    protected void StartFireRateTimer()
    {
        shotDelayed = true;
        Invoke("ResetFireRate", 1f / stats.GetStat(Stat.attackSpeed));
    }

    private void StartReload()
    {
        isReloading = true;
        anim.SetBool("isReloading", true);
        Invoke("Reload", stats.GetStat(Stat.reloadTime));
    }

    private void Reload()
    {
        if(isReloading == true)
        {
            isReloading = false;
            anim.SetBool("isReloading", false);
            List<OnEventFunctionalities> onEventFunctionalities = UpgradeEventsManager.Instance.GetOnEventFunctionalities();
            foreach(OnEventFunctionalities eventFunc in onEventFunctionalities)
            {
                eventFunc.AfterReload();
            }
            projectilesInMag = stats.GetStat(Stat.magSize);
            UpdateAmmoText();
        }
    }

    public void UpdateAmmoText()
    {
        PlayerUI.Instance.SetAmmoText(projectilesInMag, stats.GetStat(Stat.magSize));
    }

    protected void ResetFireRate()
    {
        player.ResetMoveSpeed();
        shotDelayed = false;
        if(projectilesInMag != stats.GetStat(Stat.reloadTime)) StartReload();
    }

    public SpriteRenderer GetSprite()
    {
        return spriteRenderer;
    }
}
