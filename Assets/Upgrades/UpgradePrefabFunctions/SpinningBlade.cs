using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBlade : MonoBehaviour
{
    private GameObject player;
    private StatsSO stats;

    private void Start()
    {
        stats = UpgradeEventsManager.Instance.GetPlayerStats();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        transform.position = player.transform.position;
        float rotationSpeed = stats.GetStat(Stat.bladeRotationSpeed) + stats.GetStat(Stat.bladeRotationSpeed) * stats.GetStat(Stat.summonSpeedMultiplayer);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
