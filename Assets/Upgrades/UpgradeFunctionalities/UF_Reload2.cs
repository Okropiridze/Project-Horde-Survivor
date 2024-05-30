using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UF_Reload2 : OnEventFunctionalities
{
    [SerializeField] private StatsUpgradeSO upgrade;
    private StatsSO weaponStats;
    private float startingDamage = 0;
    private bool damageReseted = true;

    public override void UpgradeEnter()
    {
        weaponStats = UpgradeEventsManager.Instance.GetWeaponStats();
        startingDamage = weaponStats.GetStat(Stat.damage);
        UpgradeEventsManager.Instance.AddOnEventFunctionalities(this);
        upgrade.upgradeToApply[Stat.damage] = 0;
        upgrade.DoUpgrade();
    }

    public override void AfterShot()
    {
        if(damageReseted == false)
        {
            upgrade.upgradeToApply[Stat.damage] = 0;
            damageReseted = true;
        }
    }

    public override void AfterReload()
    {
        startingDamage = weaponStats.GetStat(Stat.damage);
        upgrade.upgradeToApply[Stat.damage] = startingDamage * 2;
        damageReseted = false;
    }
}
