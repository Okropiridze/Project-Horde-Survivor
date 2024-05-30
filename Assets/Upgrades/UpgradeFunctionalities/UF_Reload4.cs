using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UF_Reload4 : OnEventFunctionalities
{
    [SerializeField] private StatsUpgradeSO upgrade;

    public override void UpgradeEnter()
    {
        UpgradeEventsManager.Instance.AddOnEventFunctionalities(this);
        upgrade.percentUpgradeToApply[Stat.moveSpeed] = 0;
        upgrade.DoUpgrade();
    }

    public override void AfterReload()
    {
        upgrade.percentUpgradeToApply[Stat.moveSpeed] = 50;
        Invoke("ResetMoveSpeed", 2);
    }

    private void ResetMoveSpeed()
    {
        upgrade.percentUpgradeToApply[Stat.moveSpeed] = 0;
    }
}
