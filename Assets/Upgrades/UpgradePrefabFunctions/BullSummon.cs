using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSummon : MonoBehaviour
{
    private float range = 7f;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Transform bull;
    private BullSummonChild bullChild;
    private Transform player;
    [SerializeField] private StatsSO _stats;

    private bool isAttackOnCooldown = false;

    private Vector3 offset = new Vector3(-1f, 1.5f, 0);

    private void Start()
    {
        _stats = UpgradeEventsManager.Instance.GetPlayerStats();
        bullChild = bull.GetComponent<BullSummonChild>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FollowPlayer();
        if (GetClosestEnemy() != null)
        {
            if (isAttackOnCooldown == false)
            {
                AttackEnemy();
            }
        }
    }

    private void FollowPlayer()
    {
        transform.position = player.position + offset;
    }


    private Collider2D GetClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        Collider2D closestEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, whatIsEnemy);
        foreach(Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;

    }

    private void AttackEnemy()
    {
        StartCoroutine(Charge());
        isAttackOnCooldown = true;
        float chargeTime = _stats.GetStat(Stat.bullChargeTime) - _stats.GetStat(Stat.bullChargeTime) * _stats.GetStat(Stat.summonSpeedMultiplayer);
        Invoke("ResetCooldown", chargeTime);
    }

    private void ResetCooldown()
    {
        isAttackOnCooldown = false;
    }

    IEnumerator Charge()
    {
        Vector3 initialPosition = transform.position;

        Vector3 closestEnemy = GetClosestEnemy().transform.position;
        if (closestEnemy != null)
        {

            Vector3 targetPosition = new Vector3(closestEnemy.x, closestEnemy.y, closestEnemy.z);
            
            bullChild.Charge();
            Vector3 diff = transform.position - targetPosition;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 180f;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            bull.rotation = targetRotation;
            targetPosition = bull.position + bull.up * range;

            float chargeTime = 0.15f;
            float elapsedTime = 0f;

            while (elapsedTime < chargeTime)
            {
                float t = elapsedTime / chargeTime;

                bull.position = Vector3.Lerp(initialPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            bull.rotation = Quaternion.identity;
            bull.position = targetPosition;

            float returnTime = 0.6f;
            bullChild.Idle();
            elapsedTime = 0f;
            while (elapsedTime < returnTime)
            {
                diff = bull.position - transform.position;
                diff.Normalize();
                rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                targetRotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                bull.rotation = targetRotation;
                float t = elapsedTime / returnTime;

                bull.position = Vector3.Lerp(targetPosition, transform.position, t);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            bull.rotation = Quaternion.identity;
            bull.position = transform.position;
        }
    }

}

