using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName = "Hex/Upgrades/Stats Upgrade")]
public class StatsUpgradeSO : UpgradeSO
{
    [Tooltip("The stats that this upgrade applies to.")]
    [SerializeField]
    public List<StatsSO> unitsToUpgrade = new List<StatsSO>();
    public Dictionary<Stat, float> upgradeToApply = new Dictionary<Stat, float>();
    public Dictionary<Stat, float> percentUpgradeToApply = new Dictionary<Stat, float>();

    [SerializeField] public UpgradeFunctionality upgradeFunctionality { get; private set; }


    public override void DoUpgrade()
    {
        foreach (var unitToUpgrade in unitsToUpgrade)
        {
            foreach (var upgrade in upgradeToApply)
            {
                unitToUpgrade.UnlockUpgrade(this);
            }
            foreach (var upgrade in percentUpgradeToApply)
            {
                unitToUpgrade.UnlockPercentUpgrade(this);
            }
        }
        if (upgradeFunctionality != null) UpgradesList.Instance.AddNewUpgrade(upgradeFunctionality);
    }

}
