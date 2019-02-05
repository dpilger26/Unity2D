using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int scorePerExtraHealth = 1000;

    // state parameters
    int score = 0;
    int scoreTowardNextLife = 0;
    int earnedLives = 0;

    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        int numberOfSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetEarnedLives()
    {
        var earnedLivesCopy = earnedLives;
        earnedLives = 0;
        return earnedLivesCopy;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;

        scoreTowardNextLife += scoreValue;
        if (scoreTowardNextLife > scorePerExtraHealth)
        {
            ++earnedLives;
            scoreTowardNextLife = 0;
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
