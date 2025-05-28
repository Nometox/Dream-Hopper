using System.Collections;
using UnityEngine;

public enum BossState { Follow, Launch, Idle, Rage }

public class ZombieBoss : MonoBehaviour
{
    [Header("Boss Settings")]
    public float speed = 2f;
    public float launchForce = 10f;
    public float Health = 200f;

    [Header("References")]
    [HideInInspector] public Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public GameObject hitCollider;
    public GameObject bossBoxColliderHit;

    public GameObject MagicMashroom;
    BoxCollider2D boxC2D;

    [HideInInspector] public bool waitingOnPoint = false;
    public float launchMinDistance = 5f;
    [HideInInspector] public bool isAttacking = false;


    private BossState currentState = BossState.Follow;
    private float originalSpeed;
    private float rageMultiplier = 2f;
    private bool isRaging = false;
    [HideInInspector] public bool isWeak = false;


    bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxC2D = GetComponent<BoxCollider2D>();
        originalSpeed = speed;
        MagicMashroom.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").transform;


        StartCoroutine(BossPhaseLoop());
    }

    void Update()
    {
        if (isRaging == false && Health <= 40)
        {
            spriteRenderer.color = Color.magenta;
            EnterRageMode();
        }

        if (isDead)
        {
            GameManager.Instance.Audio.PlayBossDeadSound();
            hitCollider.SetActive(false);
            bossBoxColliderHit.SetActive(false);
            boxC2D.enabled = false;
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (!isAttacking)
        {
            if (currentState == BossState.Follow)
            {
                FollowPlayer();
            }
            else if (currentState == BossState.Idle)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
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
            yield return new WaitForSeconds(3f);

            // Phase 2 - Launch
            currentState = BossState.Launch;
            LaunchAtPlayer();
            yield return new WaitForSeconds(1.5f);

            // Phase 3 - Weak
            currentState = BossState.Idle;
            isWeak = true;
            anim.SetTrigger("Weak");
            hitCollider.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            hitCollider.SetActive(true);
            isWeak = false;
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
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer >= launchMinDistance)
        {
            anim.SetTrigger("Launch");
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * launchForce;
        }
        else
        {
            currentState = BossState.Follow;
        }
    }


    void EnterRageMode()
    {
        GameManager.Instance.Audio.PlayBossRageSound();

        isRaging = true;
        speed *= rageMultiplier;
        launchForce *= rageMultiplier;
        anim.SetTrigger("Rage");
        Debug.Log("Zombie Boss entered Rage Mode!");
    }

    IEnumerator bossdamage()
    {
        hitCollider.SetActive(false);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.7f);
        if (isRaging)
        {
            spriteRenderer.color = Color.magenta;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
    public void BossGotHurtZombie()
    {
        GameManager.Instance.Audio.PlayBossGotHurtSound();
        StartCoroutine(bossdamage());
        Health -= 20;
        Debug.Log("Boss Health: " + Health);

        if (Health <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void isAttack()
    {
        isAttacking = true;
        anim.SetBool("isAtacking", true);
    }

    public void endAttack()
    {
        isAttacking = false;
        anim.SetBool("isAtacking", false);
    }

    public void AttackDamage()
    {
        GameManager.Instance.PlayerisHurt();
    }


    void Die()
    {
        anim.SetTrigger("Die");
        MagicMashroom.SetActive(true);
        rb.linearVelocity = Vector2.zero;
    }
}
