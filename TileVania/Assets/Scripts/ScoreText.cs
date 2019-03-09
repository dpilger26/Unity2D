using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    // cached parameters
    Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void UpdateDisplayText(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }
}
