using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageDealer
{
    unasigned,
    weapon,
    summon,
    fire,
}
public class KillCounter : MonoBehaviour
{
    public static KillCounter Instance { get; private set; }

    Dictionary<DamageDealer, int> killCounts = new Dictionary<DamageDealer, int>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        killCounts[DamageDealer.weapon] = 0;
        killCounts[DamageDealer.summon] = 0;
        killCounts[DamageDealer.fire] = 0;
        killCounts[DamageDealer.unasigned] = 0;


    }

    public void IncreaseKillCount(DamageDealer dealer)
    {
        if(killCounts.ContainsKey(dealer))
            killCounts[dealer] = killCounts[dealer] + 1;
        else killCounts[dealer] = 1;
    }

    public int GetKillCount(DamageDealer dealer)
    {
        return killCounts[dealer];
    }
}
