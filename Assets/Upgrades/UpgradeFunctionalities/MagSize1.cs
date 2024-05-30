using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagSize1 : UpgradeFunctionality
{
    private GameObject player;
    private Weapon weapon;
    public override void UpgradeEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = player.GetComponentInChildren<Weapon>();
        weapon.UpdateAmmoText();
    }

    public override void UpgradeUpdate()
    {

    }
}
