using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeManager : MonoBehaviour
{
    public static EnemyTypeManager Instance { get; private set; }
    private Dictionary<EnemyType, GameObject> enemies = new Dictionary<EnemyType, GameObject>();
    [SerializeField] GameObject melee;
    [SerializeField] GameObject ranged;
    [SerializeField] GameObject ladybug;
    [SerializeField] GameObject kamikaze;
    [SerializeField] GameObject dragonfly;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        enemies[EnemyType.melee] = melee;
        enemies[EnemyType.ranged] = ranged;
        enemies[EnemyType.ladybug] = ladybug;
        enemies[EnemyType.kamikaze] = kamikaze;
        enemies[EnemyType.dragonfly] = dragonfly;


    }

    public GameObject GetEnemy(EnemyType type)
    {
        return enemies[type];
    }
}
