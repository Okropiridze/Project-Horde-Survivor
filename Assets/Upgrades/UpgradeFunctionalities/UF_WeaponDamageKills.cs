using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UF_WeaponDamageKills : UpgradeFunctionality
{
    [SerializeField] StatsUpgradeSO upgrade;
    int startingKillCount = 0;
    int currentKillCount = 0;

    private int toUpgradeAfter = 20;

    public override void UpgradeEnter()
    {
        startingKillCount = KillCounter.Instance.GetKillCount(DamageDealer.weapon);
        currentKillCount = startingKillCount;
    }

    public override void UpgradeUpdate()
    {
        currentKillCount = KillCounter.Instance.GetKillCount(DamageDealer.weapon);
        if(currentKillCount - toUpgradeAfter >= startingKillCount)
        {
            upgrade.DoUpgrade();
            startingKillCount = currentKillCount;
        }
    }
}
