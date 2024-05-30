using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemySO enemySO;
    private float health;
    private SpriteRenderer spriteRenderer;
    [SerializeField] GameObject expPrefab;
    [SerializeField] private GameObject deathSplash;
    [SerializeField] private GameObject damageIndicatorPrefab;
    
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemySO = GetComponent<Enemy>().enemySO;
        health = enemySO.health;
    }

    public void TakeDamage(float damage, float knockbackForce, Vector3 knockbackDirection, List<OnEventFunctionalities> onEventFunctionalities, DamageDealer dealer)
    {
        health -= damage;
        GetComponent<Enemy>().KnockBack(knockbackForce, knockbackDirection);
        HitFlash();
        GameObject damageIndicator =  Instantiate(damageIndicatorPrefab, transform.position + Vector3.right, Quaternion.identity);
        damageIndicator.GetComponent<DamageIndicator>().SetDamageText(damage);
        //on hit effects
        if (onEventFunctionalities != null)
            foreach (OnEventFunctionalities onHitEffect in onEventFunctionalities) 
                onHitEffect.Hit(gameObject);

        if (health <= 0) Die(dealer);
    }

    private void HitFlash()
    {
        StartCoroutine(HitFlasher());
    }

    private IEnumerator HitFlasher()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die(DamageDealer dealer)
    {
        KillCounter.Instance.IncreaseKillCount(dealer);
        Instantiate(deathSplash, transform.position, Quaternion.identity);
        Instantiate(expPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
