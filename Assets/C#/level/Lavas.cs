using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lavas : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        GameManager.Instance.Audio.PlayDieSound();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
