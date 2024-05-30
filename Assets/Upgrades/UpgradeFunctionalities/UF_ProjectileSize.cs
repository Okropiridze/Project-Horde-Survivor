using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UF_ProjectileSize : UpgradeFunctionality
{
    private GameObject player;
    private StatsSO stats;
    private Weapon weapon;
    public override void UpgradeEnter()
    {
        stats = UpgradeEventsManager.Instance.GetWeaponStats();
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = player.GetComponentInChildren<Weapon>();

        weapon.ChangeProjectileSize();
    }
}
