using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] GameObject projectilePrefab;
    private float fireRate = 4f;
    private bool charging = false;

    protected override void AttackPlayer()
    {
        if(charging == false)
        {
            Vector3 diff = GetPlayerPos() - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0f, 0f, rot_z - 90));
            charging = true;
            Invoke("ResetCharge", fireRate);
        }
    }

    void ResetCharge()
    {
        charging = false;
    }
}
