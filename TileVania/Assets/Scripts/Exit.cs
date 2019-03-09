using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip exitClip;
    [SerializeField] [Range(0, 1)] float exitClipVolume = 0.5f;

    // cached parameters
    GameScore gameScore;

    private void Start()
    {
        gameScore = FindObjectOfType<GameScore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        PlayExitSound();
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(delayTime);
        Time.timeScale = 1f;
        gameScore.SetNewScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void PlayExitSound()
    {
        AudioSource.PlayClipAtPoint(exitClip, Camera.main.transform.position, exitClipVolume);
    }
}
