using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeCardUI : MonoBehaviour
{
    public StatsUpgradeSO statsUpgradeSO;
    public UpgradeFunctionality upgradeFunctionality { get; private set; }
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text desc;

    private void Start()
    {
        title.text = statsUpgradeSO.upgradeName;
        desc.text = statsUpgradeSO.upgradeDescription;
    }

    public void ApplyUpgrade()
    {
        statsUpgradeSO.DoUpgrade();
        UpgradesUIManager.Instance.RemoveUpgradeFromList(statsUpgradeSO);
        Time.timeScale = 1;
        UpgradesUIManager.Instance.CleanUp();
        UpgradesUIManager.Instance.upgradesUI.SetActive(false);
    }
}
