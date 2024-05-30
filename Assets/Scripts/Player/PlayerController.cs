using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] Transform orientation;
    private StatsSO _stats;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Collider2D coll;

    private float rollDuration = 0.3f;
    private bool isRolling = false;
    private bool wishRoll = false;

    private float moveSpeedModifier = 0;

    private Weapon gun;

    private Vector2 wishDir;
    private bool wishShoot;
    private bool wishSpecial;
    private bool wishReload;

    bool isCustomOrientation = false;

    Vector3 mouseWorldPos;
    [SerializeField] private Transform cursorFollow;
    void Start()
    {
        _stats = UpgradeEventsManager.Instance.GetPlayerStats();
        gun = orientation.GetComponentInChildren<Weapon>();
        gun.SceneEnter();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        GetInput();
        if (wishShoot) gun.WishShoot();
        if (wishSpecial) gun.WishSpecial();
        if (wishReload) gun.WishRealod();


        anim.SetFloat("speed", wishDir.magnitude);

        if (wishDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(wishDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        if(isRolling == false) MovePlayer();
        MouseAim();
    }

    private void ResetRoll()
    {
        coll.enabled = true;
        isRolling = false;
        anim.SetBool("rolling", isRolling);
    }

    private void MovePlayer()
    {
        float speed = _stats.GetStat(Stat.moveSpeed) + _stats.GetStat(Stat.moveSpeed) * moveSpeedModifier / 100;
        rb.velocity = wishDir.normalized * speed;
    }
    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }
    public void ChangeMoveSpeed(float percent)
    {
        moveSpeedModifier = percent;
    }
    public void ResetMoveSpeed()
    {
        moveSpeedModifier = 0;
    }

    private void MouseAim()
    {
        Vector3 diff = cursorFollow.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (isCustomOrientation == false) orientation.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


        if (diff.x > 0)
            gun.GetSprite().flipY = false;
        else gun.GetSprite().flipY = true;
    }

    public void SetOrientation(float rotZ)
    {
        orientation.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    public void EnableCustomOrientation()
    {
        isCustomOrientation = true;
    }

    public void DisableCustomOrientation()
    {
        isCustomOrientation = false;
    }

    public float GetOrientationZ()
    {
        return orientation.eulerAngles.z;
    }

    private void GetInput()
    {
        wishDir.x = Input.GetAxisRaw("Horizontal");
        wishDir.y = Input.GetAxisRaw("Vertical");

        wishShoot = Input.GetKey(KeyCode.Mouse0);
        wishSpecial = Input.GetKey(KeyCode.Mouse1);
        wishReload = Input.GetKey(KeyCode.R);
        wishRoll = Input.GetKeyDown(KeyCode.LeftShift);

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
    }
}
