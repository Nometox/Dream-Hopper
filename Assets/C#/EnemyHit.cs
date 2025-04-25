using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    Boss boss;
    void Start()
    {
        boss= GameObject.FindWithTag("Enemy").GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
