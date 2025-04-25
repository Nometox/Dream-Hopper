using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    Boss boss;
    void Start()
    {
        boss = GameObject.FindWithTag("Enemy").GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && boss.waitingOnPoint == true)
        {
            Debug.Log("hi");
            boss.isAttack();
        }
        else if (collision.gameObject.CompareTag("Player") && boss.waitingOnPoint == false)
        {
            boss.endAttack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && boss.waitingOnPoint == true)
        {
            Debug.Log("hi");
            boss.isAttack();
        }
        else if (collision.gameObject.CompareTag("Player") && boss.waitingOnPoint == false)
        {
            boss.endAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && boss.waitingOnPoint == false)
        {
            boss.endAttack();
        }
    }
}
