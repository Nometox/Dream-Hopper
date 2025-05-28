using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicMashroom : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        GameManager.Instance.Audio.PlayRoundWinSound();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
