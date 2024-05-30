using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Exp : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void PickUp()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(GoToPlayer());
    }

    IEnumerator GoToPlayer()
    {
        Vector3 initialPosition = transform.position;

        float chargeTime = 0.1f;
        float elapsedTime = 0f;

        Vector3 oppositeDirection = transform.position - player.position;
        oppositeDirection.Normalize();

        while (elapsedTime < chargeTime)
        {
            float t = elapsedTime / chargeTime;
            transform.position = Vector3.Lerp(initialPosition, initialPosition + oppositeDirection, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        float followTime = 0.2f;
        elapsedTime = 0f;
        initialPosition = transform.position;

        while (elapsedTime < followTime)
        {
            float t = elapsedTime / followTime;
            transform.position = Vector3.Lerp(initialPosition, player.position, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.GetComponent<PlayerLevels>().AddExp();
        Destroy(gameObject);
    }
}
