using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSummonUpgrade : UpgradeFunctionality
{
    [SerializeField] private GameObject prefab;
    private Transform player;
    public override void UpgradeEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject summon = Instantiate(prefab, player.position, Quaternion.identity);

    }
}
