using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    // cached parameters
    ScoreText scoreText;

    // state parameters
    int totalScore = 0;
    int scoreSinceLastDeath = 0;

    // Start is called before the first frame update
    private void Start()
    {
        if (FindObjectsOfType<GameScore>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        scoreText = FindObjectOfType<ScoreText>();
        if (scoreText)
        {
            scoreText.UpdateDisplayText(scoreSinceLastDeath);
        }
    }

    public void IncramentScore(int amount)
    {
        scoreSinceLastDeath += amount;
    }

    public void ResetScore()
    {
        totalScore = 0;
        scoreSinceLastDeath = 0;
    }

    public void SetNewScore()
    {
        totalScore = scoreSinceLastDeath;
    }

    public void ResetScoreOnDeath()
    {
        scoreSinceLastDeath = totalScore;
    }
}
