using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicMashroomLevel2 : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level3");
        }
    }
}
