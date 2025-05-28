using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool giveMaxHeart = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (giveMaxHeart)
                player.IncreaseMaxHearts();
            else
                player.AddHeart();

            Destroy(gameObject);
        }
    }
}
