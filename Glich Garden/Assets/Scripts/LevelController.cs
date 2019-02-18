using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // configuration parameters
    [SerializeField] GameObject winLabel;
    [SerializeField] float nextLevelDelay = 3f;

    // state parameters
    int numberOfAliveAttackers = 0;

    // cached parameters
    GameTimer gameTimer;

    private void Start()
    {
        winLabel.SetActive(false);
        gameTimer = FindObjectOfType<GameTimer>();
    }

    public void SetWinLabel(bool value)
    {
        winLabel.SetActive(value);
    }

    public void IncramentAliveAttackers()
    {
        ++numberOfAliveAttackers;
    }

    public void DecrementAliveAttackers()
    {
        --numberOfAliveAttackers;

        if (gameTimer.TimerComplete() && numberOfAliveAttackers <= 0)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    private IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        Debug.Log("Playing Audio");
        GetComponent<AudioSource>().Play();
        Debug.Log("Done Playing Audio");

        yield return new WaitForSeconds(nextLevelDelay);

        FindObjectOfType<LevelLoader>().LoadNextScene();
    }
}
