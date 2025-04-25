using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    Animator anim;
    public Transform[] patrolPoints;
    SpriteRenderer spriteRenderer;

    int currentPointIndex;
    public float speed;
    public float waitTime;
    private bool once;
    bool waitingOnPoint;

    public BoxCollider2D hitpoint;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        isMoving();
        flipX();
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
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
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

    void isAttack()
    {
        anim.SetBool("isAtacking", true);
    }
    void endAttack()
    {
        anim.SetBool("isAtacking", false);
    }
}
