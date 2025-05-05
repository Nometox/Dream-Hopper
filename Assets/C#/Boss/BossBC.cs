using UnityEngine;

public class BossBC : MonoBehaviour
{
    Boss Boss;
    void Start()
    {
        Boss = GameObject.FindWithTag("Enemy").GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss.AttackDamage();
        }
    }
}
