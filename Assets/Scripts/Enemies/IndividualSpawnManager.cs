using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualSpawnManager : MonoBehaviour
{
    public void EnableSpawn(float timeBetweenSpawn)
    {
        StartCoroutine(Spawn(timeBetweenSpawn));
    }

    IEnumerator Spawn(float timeBetweenSpawn)
    {
        while (true)
        {
            Debug.Log("enemy spawned");
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    public void DisableSpawn()
    {
        StopAllCoroutines();
    }
}
