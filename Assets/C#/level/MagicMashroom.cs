using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicMashroom : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
