using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBladeUpgrade : UpgradeFunctionality
{
    [SerializeField] private GameObject spinningBladePrefab;
    private Transform player;
    public override void UpgradeEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject blade = Instantiate(spinningBladePrefab, player.position, Quaternion.identity);
        
    }
}
