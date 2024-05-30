using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wave")]
public class Wave : SerializedScriptableObject
{
    public Dictionary<EnemyType, float> waveEnemies = new Dictionary<EnemyType, float>();
}
