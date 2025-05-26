using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private Boss boss;
    private bool playerInRange = false;

    void Start()
    {
        boss = GameObject.FindWithTag("Enemy").GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && boss.IsWaitingAtPoint())
        {
            playerInRange = true;
            boss.StartAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void TryDealAttackDamage()
    {
        if (playerInRange)
        {
            boss.DealAttackDamage();
        }
    }
}
