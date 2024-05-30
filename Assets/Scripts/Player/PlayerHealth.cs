using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private StatsSO _stats;
    private float health = 0;
    private bool isInvincible = false;
    private float invincibilityTime = 1f;
    [SerializeField] LayerMask whatIsEnemy;
    private float knockbackRadius = 4f;

    private void Start()
    {
        health = _stats.GetStat(Stat.hitPoints);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        StartInvincibility();
        CameraManager.Instance.ShakeCamera(0.3f, 0.4f);
        TimeManager.Instance.SlowDown(50f, 0.2f);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, knockbackRadius, whatIsEnemy);
        foreach (Collider2D enemy in enemies)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            direction.Normalize();
            enemy.GetComponent<Enemy>().KnockBack(2000f, direction);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void StartInvincibility()
    {
        isInvincible = true;
        Invoke("StopInvincibility", invincibilityTime);
    }

    private void StopInvincibility()
    {
        isInvincible = false;
    }

    private void Die()
    {
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible) return;
        if (collision.gameObject.layer == 7)
        {
            TakeDamage(30f);
        }
    }

    public void IncreaseHealth()
    {
        health = Mathf.Clamp(health+1, 0, _stats.GetStat(Stat.hitPoints));
    }
}
