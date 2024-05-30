using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;

    private bool hasCollided = false;

    float damage;
    float knockback;
    float projectileSpeed;
    float projectileBounce;
    float projectilePierce;



    public void SetGunStats(float _damage, float _knockback, float _projectileSpeed, float destroyAfter, float _projectileBounce, float _projectilePierce, float _projectileSize)
    {
        damage = _damage;
        knockback = _knockback;
        projectileSpeed = _projectileSpeed;
        projectileBounce = _projectileBounce;
        projectilePierce = _projectilePierce;

        GetComponentInChildren<Transform>().localScale = Vector3.one * _projectileSize;
        GetComponent<TrailRenderer>().widthMultiplier = _projectileSize;

        //GetComponent<CircleCollider2D>().radius = _projectileSize * 0.2f;

        Invoke("DestroyifNotHit", destroyAfter);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
    }

    private Collider2D GetClosestEnemy(GameObject hitEnemy)
    {
        float closestDistance = Mathf.Infinity;
        Collider2D closestEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 100f, whatIsEnemy);
        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && enemy.gameObject != hitEnemy)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && hasCollided == false)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage, knockback, transform.up, UpgradeEventsManager.Instance.GetOnEventFunctionalities(), DamageDealer.weapon);
            if(projectileBounce > 0 && GetClosestEnemy(collision.gameObject) != null)
            {
                Vector3 closestEnemy = GetClosestEnemy(collision.gameObject).transform.position;
                Vector3 targetPosition = new Vector3(closestEnemy.x, closestEnemy.y, closestEnemy.z);
                Vector3 diff = transform.position - targetPosition;
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 180f;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                transform.rotation = targetRotation;
                
                projectileBounce--;
            }
            else if(projectilePierce > 0)
            {
                projectilePierce--;
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                hasCollided = true;
                Destroy(gameObject, 0.02f);
            }
        }
    }

    private void DestroyifNotHit()
    {
        Destroy(gameObject);
        Destroy(this);
    }
}
