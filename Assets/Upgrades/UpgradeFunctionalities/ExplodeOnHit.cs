using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ExplodeOnHit : OnEventFunctionalities
{
    private StatsSO weaponStats;
    private LayerMask whatIsEnemy;
    [SerializeField] private GameObject effectPrefab;
    public override void UpgradeEnter()
    {
        weaponStats = UpgradeEventsManager.Instance.GetWeaponStats();
        UpgradeEventsManager.Instance.AddOnEventFunctionalities(this);
        whatIsEnemy |= (1 << LayerMask.NameToLayer("Enemy"));
    }

    public override void Hit(GameObject enemy)
    {
        CameraManager.Instance.ShakeCamera(0.3f, 0.2f);
        GameObject effect = Instantiate(effectPrefab, enemy.transform.position, Quaternion.identity);
        //effect.transform.parent = enemy.transform;
        Destroy(effect, 0.5f);

        HashSet<EnemyHealth> damagedEnemies = new HashSet<EnemyHealth>();

        Collider2D[] enemies = Physics2D.OverlapCircleAll(enemy.transform.position, weaponStats.GetStat(Stat.explosionRange), whatIsEnemy);

        foreach (Collider2D enemyCollider in enemies)
        {
            EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
            if (enemyHealth != null && !damagedEnemies.Contains(enemyHealth))
            {
                Vector3 direction = enemyCollider.transform.position - enemy.transform.position;
                direction.Normalize();
                enemyHealth.TakeDamage(weaponStats.GetStat(Stat.explosiveDamage), 250f, direction, null, DamageDealer.unasigned);
                damagedEnemies.Add(enemyHealth);
            }
        }
    }


}
