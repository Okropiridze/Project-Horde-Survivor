using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesList : MonoBehaviour
{
    public static UpgradesList Instance { get; private set; }

    public List<UpgradeFunctionality> upgrades = new List<UpgradeFunctionality>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        for(int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].UpgradeUpdate();
        }
    }

    public void AddNewUpgrade(UpgradeFunctionality upgrade)
    {
        upgrades.Add(upgrade);
        upgrade.UpgradeEnter();
    }
}
