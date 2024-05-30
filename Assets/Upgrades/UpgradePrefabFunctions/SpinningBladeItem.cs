using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBladeItem : MonoBehaviour
{
    private StatsSO stats;
    private void Start()
    {
        stats = UpgradeEventsManager.Instance.GetPlayerStats();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, 500f * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            float damage = stats.GetStat(Stat.bladeDamage) + stats.GetStat(Stat.bladeDamage) * stats.GetStat(Stat.summonDamageMultiplayer);
            if (enemyHealth) enemyHealth.TakeDamage(damage, 0f, Vector3.zero, null, DamageDealer.summon);
        }
    }
}
