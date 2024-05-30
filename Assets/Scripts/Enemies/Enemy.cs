using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;
    protected GameObject player;
    protected float bumpForce = 0.7f;
    protected Rigidbody2D rb;
    protected bool inAttackRange = true;
    private bool isOnFire = false;
    private float moveSpeed = 0f;

    float distance;
    Vector2 direction;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject fireEffectPrefab;
    protected Animator anim;


    // remake enemy state
    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking,
    }



    private void Start()
    {
        EnterScene();
    }

    protected virtual void EnterScene()
    {
        anim = GetComponentInChildren<Animator>();
        moveSpeed = enemySO.maxMoveSpeed;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance > enemySO.attackRange) inAttackRange = false;
        else inAttackRange = true;

        if(player.transform.position.x > transform.position.x)
            spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }

    public void ChangeMoveSpeed(float amount, float duration)
    {
        moveSpeed = enemySO.maxMoveSpeed + enemySO.maxMoveSpeed * amount / 100;

        if (duration != 0) Invoke("ResetMoveSpeed", duration);
    }

    private void ResetMoveSpeed()
    {
        moveSpeed = enemySO.maxMoveSpeed;
    }

    public void ApplyFire(float damage, float duration)
    {
        if (isOnFire) return;
        isOnFire = true;
        GameObject firePrefab = Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
        firePrefab.transform.parent = transform;
        Destroy(firePrefab, duration);
        StartCoroutine(DamageTick(damage, duration, DamageDealer.fire));
        Invoke("RemoveFire", duration);
    }

    private void RemoveFire()
    {
        isOnFire = false;
    }

    private IEnumerator DamageTick(float damage, float duration, DamageDealer dealer)
    {
        float interval = 0.5f;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            GetComponent<EnemyHealth>().TakeDamage(damage, 0, Vector3.zero, null, dealer);
            yield return new WaitForSeconds(interval);
        }
    }

    private void FixedUpdate()
    {
        if (inAttackRange == false)
        {
            MoveTowardsThePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    protected void MoveTowardsThePlayer()
    {
        rb.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode2D.Force);
    }

    protected Vector2 GetDirection()
    {
        return direction;
    }

    protected virtual void AttackPlayer()
    {

    }

    public void KnockBack(float force, Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode2D.Force);
    }
}
