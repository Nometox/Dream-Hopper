using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    Boss boss;
    PlayerController plController;
    void Start()
    {
        boss = GameObject.FindWithTag("Enemy").GetComponent<Boss>();
        plController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (boss.waitingOnPoint == false)
        {
            boss.endAttack();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && boss.waitingOnPoint == true)
        {
            boss.isAttack();
        }
        else
        {
            boss.endAttack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && boss.waitingOnPoint == true)
        {
            boss.isAttack();
        }
        else
        {
            boss.endAttack();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.endAttack();
        }
    }
}
