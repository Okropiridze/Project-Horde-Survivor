using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    public static List<T> GetScriptableObjects<T>(string path) where T : ScriptableObject
    {
#if UNITY_EDITOR


        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).ToString(), new[] { path });
        List<T> scriptableObjects = new List<T>();

        foreach (var guid in guids)
        {
            UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            scriptableObjects.Add(UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T);
        }

        return scriptableObjects;
#else
        return null;
#endif
    }
}

///EXTRA BITS...

public enum EnemyType
{
    melee,
    ranged,
    ladybug,
    kamikaze,
    dragonfly,
}

public enum Stat
{
    //character
    hitPoints,
    moveSpeed,
    reloadTime,

    //weapon
    damage,
    attackSpeed,
    magSize,
    projectiles,
    spread,

    projectileSpeed,
    knockback,

    runAndGunBonus,
    standingStillDamageBonus,

    // On Hit Effects
    explosiveDamage,
    explosionRange,
    freezeSpeedReduction,
    freezeDuration,
    fireDamage,
    fireDuration,

    // summons
    summonDamageMultiplayer,
    summonSpeedMultiplayer,

    bladeDamage,
    bladeRotationSpeed,
    bladeAmount,

    destroyBulletAfter,

    damagePercent,
    attackSpeedPercent,
    projectilesPercent,
    spreadPercent,

    bullDamage,
    bullKnockBack,
    bullChargeTime,
    bullImpactRadius,

    projectileBounce,
    projectilePiercing,
    projectileSize,
}
