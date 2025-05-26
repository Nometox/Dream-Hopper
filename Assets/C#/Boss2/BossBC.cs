using UnityEngine;

public class BossBC : MonoBehaviour
{
    private Boss boss;

    void Start()
    {
        boss = GameObject.FindWithTag("Enemy").GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.DealAttackDamage();
        }
    }
}
