using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    // cached references
    TextMeshProUGUI scoreText;
    Player player;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
        DisplayScore();
    }

    private void Update()
    {
        DisplayScore();
    }

    private void DisplayScore()
    {
        scoreText.text = player.GetCurrentHealth().ToString();
    }
}
