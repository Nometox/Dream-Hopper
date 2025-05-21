using System.Collections;
using UnityEngine;

public enum BossState { Follow, Launch, Idle, Rage }

public class ZombieBoss : MonoBehaviour
{
    [Header("Boss Settings")]
    public float speed = 2f;
    public float launchForce = 10f;
    public float Health = 200f;
    public float weakPhaseDuration = 3f;

    [Header("References")]
    public Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public bool waitingOnPoint = false;

    private BossState currentState = BossState.Follow;
    private float originalSpeed;
    private float rageMultiplier = 2f;
    private bool isRaging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalSpeed = speed;

        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        StartCoroutine(BossPhaseLoop());
    }

    void Update()
    {
        if (!isRaging && Health <= 100)
        {
            EnterRageMode();
        }

        if (currentState == BossState.Follow)
        {
            FollowPlayer();
        }
        else if (currentState == BossState.Idle)
        {
            rb.linearVelocity = Vector2.zero;
        }

        anim.SetBool("isMoving", rb.linearVelocity.magnitude > 0.1f);
    }

    IEnumerator BossPhaseLoop()
    {
        while (Health > 0)
        {
            // Phase 1 - Follow
            currentState = BossState.Follow;
            waitingOnPoint = false;
            yield return new WaitForSeconds(3f);

            // Phase 2 - Launch
            currentState = BossState.Launch;
            LaunchAtPlayer();
            yield return new WaitForSeconds(1.5f);

            // Phase 3 - Weak
            currentState = BossState.Idle;
            anim.SetTrigger("Weak");
            // setactive 
            yield return new WaitForSeconds(weakPhaseDuration);
            waitingOnPoint = false;
        }
    }

    void FollowPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        if (direction.x < 0)
        {
            Vector3 pos = new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
            transform.rotation = Quaternion.Euler(pos);
        }
        else
        {
            Vector3 pos = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            transform.rotation = Quaternion.Euler(pos);
        }
    }

    void LaunchAtPlayer()
    {
        anim.SetTrigger("Launch");
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * launchForce;
    }

    void EnterRageMode()
    {
        isRaging = true;
        speed *= rageMultiplier;
        launchForce *= rageMultiplier;
        anim.SetTrigger("Rage");
        Debug.Log("Zombie Boss entered Rage Mode!");
    }

    public void BossGotHurtZombie()
    {
        Health -= 20;
        anim.SetTrigger("Hurt");
        Debug.Log("Boss Health: " + Health);

        if (Health <= 0)
        {
            Die();
        }
    }

    public void isAttack()
    {
        anim.SetBool("isAtacking", true);
    }

        public void AttackDamage()
    {
        GameManager.Instance.PlayerisHurt();
    }

    public void endAttack()
    {
        anim.SetBool("isAtacking", false);
    }

    void Die()
    {
        anim.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;
    }
}
