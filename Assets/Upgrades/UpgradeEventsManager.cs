using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEventsManager : MonoBehaviour
{
    public static UpgradeEventsManager Instance { get; private set; }

    [SerializeField] private List<OnEventFunctionalities> onEventFunctionalities = new List<OnEventFunctionalities>();

    [SerializeField] private StatsSO playerStats;
    [SerializeField] private StatsSO weaponStats;

    private void Awake()
    {
        Instance = this;
    }

    public void AddOnEventFunctionalities(OnEventFunctionalities func)
    {
        onEventFunctionalities.Add(func);
    }


    public List<OnEventFunctionalities> GetOnEventFunctionalities()
    {
        return onEventFunctionalities;
    }

    public StatsSO GetPlayerStats()
    {
        return playerStats;
    }

    public StatsSO GetWeaponStats()
    {
        return weaponStats;
    }

}
