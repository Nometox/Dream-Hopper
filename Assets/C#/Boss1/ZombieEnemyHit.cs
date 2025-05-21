using UnityEngine;

public class ZombieEnemyHit : MonoBehaviour
{
    ZombieBoss boss;
    void Start()
    {
        boss = GameObject.FindWithTag("Enemy").GetComponent<ZombieBoss>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        if (collision.gameObject.CompareTag("Player"))
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
