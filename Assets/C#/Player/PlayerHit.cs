using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    PlayerController plController;
    private bool hasAttacked = false;

    void Start()
    {
        plController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && plController.isAttacking && !hasAttacked)
        {
            plController.AttackDamage();
            hasAttacked = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && plController.isAttacking && !hasAttacked)
        {
            plController.AttackDamage();
            hasAttacked = true;
        }
    }

    void Update()
    {
        if (!plController.isAttacking)
        {
            hasAttacked = false;
        }
    }
}
