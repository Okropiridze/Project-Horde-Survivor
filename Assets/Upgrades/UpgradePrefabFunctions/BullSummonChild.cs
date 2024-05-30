using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSummonChild : MonoBehaviour
{
    HashSet<EnemyHealth> damagedEnemies = new HashSet<EnemyHealth>();
    [SerializeField] private Animator anim;
    [SerializeField] private StatsSO _stats;
    private CircleCollider2D collider;
    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        _stats = UpgradeEventsManager.Instance.GetPlayerStats();
        collider = GetComponent<CircleCollider2D>();
    }

    public void Charge()
    {
        collider.radius = _stats.GetStat(Stat.bullImpactRadius);
        GetComponent<Collider2D>().enabled = true;
    }

    public void Idle()
    {
        GetComponent<Collider2D>().enabled = false;
        damagedEnemies.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 knockBackDir = collision.transform.position - transform.position;
        knockBackDir.Normalize();
        if(collision.gameObject.layer == 7)
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if(enemyHealth && !damagedEnemies.Contains(enemyHealth))
            damagedEnemies.Add(enemyHealth);
            float damage = _stats.GetStat(Stat.bullDamage) + _stats.GetStat(Stat.bullDamage) * _stats.GetStat(Stat.summonDamageMultiplayer);
            enemyHealth.TakeDamage(damage, _stats.GetStat(Stat.bullKnockBack), knockBackDir, null, DamageDealer.summon);
        }
    }
}
