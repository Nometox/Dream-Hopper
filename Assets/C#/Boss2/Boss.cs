using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("References")]
    public Transform[] patrolPoints;
    public BoxCollider2D hitPoint;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private EnemyHit enemyHit;
    public GameObject mashroom;

    BoxCollider2D boxC2D;

    [Header("Boss Settings")]
    [SerializeField] private float health = 500;
    float Normalspeed = 2f;
    public float speed;
    float normalWaitTime = 2f;
    public float waitTime;
    float ragespeed = 5;
    float waitTimeRage = 2.5f;
    private int currentPointIndex = 0;
    private bool isWaiting = false;
    private bool coroutineStarted = false;
    bool isDead = false;

    public GameObject hitSpot;
    public GameObject bossGetHit;
    bool isRaging = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyHit = GameObject.FindWithTag("HitPoint").GetComponent<EnemyHit>();
        rb = GetComponent<Rigidbody2D>();
        boxC2D = GetComponent<BoxCollider2D>();
        speed = Normalspeed;
        waitTime = normalWaitTime;
    }

    void Update()
    {
        if (isDead == true)
        {
            GameManager.Instance.Audio.PlayBossDeadSound();
            mashroom.SetActive(true);
            boxC2D.enabled = false;
            hitSpot.SetActive(false);
            bossGetHit.SetActive(false);
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (isRaging == false && health <= 100)
        {
            EnterRageMode();
        }
        Patrol();
        FlipDirection();
    }

    void EnterRageMode()
    {
        spriteRenderer.color = Color.red;
        isRaging = true;
        anim.SetTrigger("Rage");
        GameManager.Instance.Audio.PlayBossRageSound();
        speed = ragespeed;
        waitTime = waitTimeRage;
    }

    void Patrol()
    {
        if (isWaiting) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        float distance = Vector2.Distance(transform.position, targetPoint.position);
        bool isMoving = distance > 0.05f;

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        }
        else if (!coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(WaitAtPoint());
        }

        anim.SetBool("isMoving", isMoving);
    }

    IEnumerator WaitAtPoint()
    {
        isWaiting = true;

        if (Random.value < 0.5f)
        {
            anim.SetTrigger("FakeAttack");
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(waitTime);
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        isWaiting = false;
        coroutineStarted = false;
    }

    void FlipDirection()
    {
        if (transform.position.x < patrolPoints[currentPointIndex].position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (isWaiting)
        {
            if (currentPointIndex == 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (currentPointIndex == 2)
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void StartAttack()
    {
        anim.SetTrigger("isAttacking");
        GameManager.Instance.Audio.PlayBossAttackSound();
    }
    public void DealAttackDamage() => GameManager.Instance.PlayerisHurt();

    public void TakeDamageDemon()
    {
        anim.SetTrigger("Hurt");
        GameManager.Instance.Audio.PlayBossGotHurtSound();
        if (health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Die");
            rb.linearVelocity = Vector2.zero;
        }
        health -= 20;
    }

    public bool IsWaitingAtPoint() => isWaiting;
}
