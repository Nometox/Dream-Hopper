using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    Animator anim;
    public Transform[] patrolPoints;
    SpriteRenderer spriteRenderer;

    [SerializeField] float Health = 100;
    int currentPointIndex;
    public float speed;
    public float waitTime;
    private bool once;
    public bool waitingOnPoint;
    EnemyHit enemyhit;

    public BoxCollider2D hitpoint;

    // PlayerController plController;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyhit = GameObject.FindWithTag("HitPoint").GetComponent<EnemyHit>();
    }
    void Update()
    {
        isMoving();
        flipX();
        BossDefeat();
    }
    void isMoving()
    {
        if (Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        }
        else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
        bool Moving = Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) > 0.05f;
        anim.SetBool("isMoving", Moving);
    }

    void flipX()
    {
        if (transform.position.x < patrolPoints[currentPointIndex].position.x)
        {
            Vector3 pos = new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
            transform.rotation = Quaternion.Euler(pos);
        }
        else
        {
            Vector3 pos = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            transform.rotation = Quaternion.Euler(pos);
        }

        if (currentPointIndex == 0)
        {
            if (waitingOnPoint == true)
            {
                Vector3 pos = new Vector3(transform.rotation.x, 0, transform.rotation.z);
                transform.rotation = Quaternion.Euler(pos);
            }
        }
        if (currentPointIndex == 2)
        {
            if (waitingOnPoint == true)
            {
                Vector3 pos = new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
                transform.rotation = Quaternion.Euler(pos);
            }
        }
    }

    IEnumerator Wait()
    {
        waitingOnPoint = true;
        yield return new WaitForSeconds(waitTime);
        waitingOnPoint = false;
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
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

    public void BossGotHurtDemon()
    {
        Health -= 20;
    }

    void BossDefeat()
    {
        if (Health == 0)
        {
            Destroy(this.gameObject);
        }
    }

}
