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

    [Header("Boss Settings")]
    [SerializeField] private float health = 100;
    public float speed = 2f;
    public float waitTime = 2f;

    private int currentPointIndex = 0;
    private bool isWaiting = false;
    private bool coroutineStarted = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyHit = GameObject.FindWithTag("HitPoint").GetComponent<EnemyHit>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        Patrol();
        FlipDirection();
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

    public void StartAttack() => anim.SetTrigger("isAttacking");
    public void DealAttackDamage() => GameManager.Instance.PlayerisHurt();

    public void TakeDamageDemon()
    {
        health -= 20;
        anim.SetTrigger("Hurt");
        if (health <= 0)
        {
            anim.SetTrigger("Die");
            rb.linearVelocity = Vector2.zero;
        }
    }

    public bool IsWaitingAtPoint() => isWaiting;
}
