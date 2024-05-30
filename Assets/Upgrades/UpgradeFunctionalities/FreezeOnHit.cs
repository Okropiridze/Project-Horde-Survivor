using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnHit : OnEventFunctionalities
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
        Destroy(effect, weaponStats.GetStat(Stat.freezeDuration));
        enemy.GetComponent<Enemy>().ChangeMoveSpeed(weaponStats.GetStat(Stat.freezeSpeedReduction), weaponStats.GetStat(Stat.freezeDuration));
    }
}
