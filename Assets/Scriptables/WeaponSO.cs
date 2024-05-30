using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public GameObject projectilePrefab;

    public float damage = 50f;
    public float fireRate = 2f;
    public float reloadTime = 1f;
    public int maxProjectilesInMag = 6;
    public float projectileSpeed = 50f;

}
