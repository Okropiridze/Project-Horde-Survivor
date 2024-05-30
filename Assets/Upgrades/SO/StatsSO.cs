using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[CreateAssetMenu(menuName = "Unit Stats")]
public class StatsSO : SerializedScriptableObject
{
    public Dictionary<Stat, float> stats = new Dictionary<Stat, float>();
    private List<StatsUpgradeSO> appliedUpgrades = new List<StatsUpgradeSO>();
    private List<StatsUpgradeSO> appliedPercentUpgrades = new List<StatsUpgradeSO>();


    public event Action<StatsSO, StatsUpgradeSO> upgradeApplied;

    public float GetStat(Stat stat)
    {
        if (stats.TryGetValue(stat, out float value))
            return GetUpgradedValue(stat, value);
        else
        {
            Debug.LogError($"No stat value found for {stat} on {this.name}");
            return 0;
        }
    }

    public int GetStatAsInt(Stat stat)
    {
        return (int)GetStat(stat);
    }

    public void UnlockUpgrade(StatsUpgradeSO upgrade)
    {
        if (!appliedUpgrades.Contains(upgrade))
        {
            appliedUpgrades.Add(upgrade);
            upgradeApplied?.Invoke(this, upgrade);
        }
    }

    public void UnlockRepeatingUpgrade()
    {

    }

    public void UnlockPercentUpgrade(StatsUpgradeSO upgrade)
    {
        if (!appliedPercentUpgrades.Contains(upgrade))
        {
            appliedPercentUpgrades.Add(upgrade);
            upgradeApplied?.Invoke(this, upgrade);
        }
    }

    private float GetUpgradedValue(Stat stat, float baseValue)
    {
        foreach (var upgrade in appliedUpgrades)
        {
            if (!upgrade.upgradeToApply.TryGetValue(stat, out float upgradeValue))
                continue;
            baseValue += upgradeValue;
        }
        foreach (var upgrade in appliedPercentUpgrades)
        {
            if (!upgrade.percentUpgradeToApply.TryGetValue(stat, out float upgradeValue))
                continue;
            baseValue *= (upgradeValue / 100f) + 1f;
        }

        return baseValue;
    }

    [Button]
    public void ResetAppliedUpgrades()
    {
        appliedUpgrades.Clear();
        appliedPercentUpgrades.Clear();
    }
}
