using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Enemy
{
    private float detonationTime = 2f;
    private bool detonating = false;

    private float explositonDamage = 30f;
    private float explosionRange = 4f;
    private LayerMask whatIsEnemy;

    [SerializeField] private GameObject effectPrefab;

    protected override void EnterScene()
    {
        base.EnterScene();
        whatIsEnemy |= (1 << LayerMask.NameToLayer("Enemy"));
    }

    protected override void AttackPlayer()
    {
        if (detonating == false)
        {
            detonating = true;
            anim.SetBool("isDetonating", true);
            Invoke("Explode", detonationTime);
        }
        MoveTowardsThePlayer();
    }

    void Explode()
    {
        CameraManager.Instance.ShakeCamera(0.3f, 0.2f);
        GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);

        HashSet<EnemyHealth> damagedEnemies = new HashSet<EnemyHealth>();

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, whatIsEnemy);

        foreach (Collider2D enemyCollider in enemies)
        {
            EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
            if (enemyHealth != null && !damagedEnemies.Contains(enemyHealth))
            {
                Vector3 direction = enemyCollider.transform.position - transform.position;
                direction.Normalize();
                enemyHealth.TakeDamage(explositonDamage, 250f, direction, null, DamageDealer.unasigned);
                damagedEnemies.Add(enemyHealth);
            }
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= explosionRange)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(explositonDamage);
        }

        GetComponent<EnemyHealth>().TakeDamage(enemySO.health, 0f, Vector3.zero, null, DamageDealer.unasigned);
    }
}
