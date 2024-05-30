using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private List<StatsSO> statsList;
    private const string statsPath = "Assets/Upgrades/Stats";

    private void OnApplicationQuit()
    {
        statsList = HelperFunctions.GetScriptableObjects<StatsSO>(statsPath);

        foreach (var stat in statsList)
        {
            stat.ResetAppliedUpgrades();
        }
    }
}