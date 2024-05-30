using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UF_Reload3 : OnEventFunctionalities
{
    [SerializeField] private StatsUpgradeSO upgrade;

    public override void UpgradeEnter()
    {
        UpgradeEventsManager.Instance.AddOnEventFunctionalities(this);
        upgrade.percentUpgradeToApply[Stat.attackSpeed] = 0;
        upgrade.DoUpgrade();
    }

    public override void AfterReload()
    {
        upgrade.percentUpgradeToApply[Stat.attackSpeed] = 40;
        Invoke("ResetFireRate", 0.5f);
        
    }

    private void ResetFireRate()
    {
        upgrade.percentUpgradeToApply[Stat.attackSpeed] = 0;
    }
}
