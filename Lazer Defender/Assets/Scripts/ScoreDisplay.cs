using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    // cached references
    TextMeshProUGUI scoreText;
    GameSession gameSession;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        DisplayScore();
    }

    private void Update()
    {
        DisplayScore();
    }

    private void DisplayScore()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
