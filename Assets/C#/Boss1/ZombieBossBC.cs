using UnityEngine;

public class ZombieBossBC : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ZombieBoss.AttackDamage();
        }
    }
}
