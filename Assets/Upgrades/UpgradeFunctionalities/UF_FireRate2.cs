using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UF_FireRate2 : OnEventFunctionalities
{
    [SerializeField] private StatsUpgradeSO upgrade;
    private float bonusFireRate = 0;

    public override void UpgradeEnter()
    {
        UpgradeEventsManager.Instance.AddOnEventFunctionalities(this);
        upgrade.percentUpgradeToApply[Stat.attackSpeed] = 0;
        upgrade.DoUpgrade();
    }

    public override void AfterShot()
    {
        bonusFireRate += 10;
        upgrade.percentUpgradeToApply[Stat.attackSpeed] = bonusFireRate;
    }

    public override void AfterReload()
    {
        bonusFireRate = 0;
        upgrade.percentUpgradeToApply[Stat.attackSpeed] = bonusFireRate;
    }
}
