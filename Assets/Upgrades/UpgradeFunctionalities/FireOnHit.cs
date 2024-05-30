using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnHit : OnEventFunctionalities
{
    private StatsSO weaponStats;
    [SerializeField] private GameObject effectPrefab;
    public override void UpgradeEnter()
    {
        weaponStats = UpgradeEventsManager.Instance.GetWeaponStats();
        UpgradeEventsManager.Instance.AddOnEventFunctionalities(this);
    }

    public override void Hit(GameObject enemy)
    {
        GameObject effect = Instantiate(effectPrefab, enemy.transform.position, Quaternion.identity);
        effect.transform.parent = enemy.transform;
        Destroy(effect, weaponStats.GetStat(Stat.fireDuration));
        enemy.GetComponent<Enemy>().ApplyFire(weaponStats.GetStat(Stat.fireDamage), weaponStats.GetStat(Stat.fireDuration));
    }

}
