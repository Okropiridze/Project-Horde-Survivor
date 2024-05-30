using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;

    private void Start()
    {
        Invoke("DestroyIfNotHit", 10f);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * 4f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(30f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(30f);
            Destroy(gameObject);
        }
    }

    private void DestroyIfNotHit()
    {
        Destroy(gameObject);
    }
}
