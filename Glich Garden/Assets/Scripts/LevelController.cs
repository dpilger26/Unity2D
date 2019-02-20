using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // configuration parameters
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject lossLabel;
    [SerializeField] float nextLevelDelay = 3f;

    // state parameters
    int numberOfAliveAttackers = 0;
    bool gameLost = false;

    // cached parameters
    GameTimer gameTimer;

    private void Start()
    {
        winLabel.SetActive(false);
        lossLabel.SetActive(false);
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
        if (gameLost)
        {
            return;
        }

        --numberOfAliveAttackers;

        if (gameTimer.TimerComplete() && numberOfAliveAttackers <= 0)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    private IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(nextLevelDelay);

        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void TriggerLossScreen()
    {
        if (gameLost)
        {
            return;
        }

        gameLost = true;
        lossLabel.SetActive(true);
        Time.timeScale = 0;  // Stops the time
    }

    public bool GameLost()
    {
        return gameLost;
    }
}
