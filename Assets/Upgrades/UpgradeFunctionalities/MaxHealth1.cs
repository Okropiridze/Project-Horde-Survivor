using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealth1 : UpgradeFunctionality
{
    private PlayerHealth pHealth;
    public override void UpgradeEnter()
    {
        pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        pHealth.IncreaseHealth();
    }

    public override void UpgradeUpdate()
    {
        
    }
}
