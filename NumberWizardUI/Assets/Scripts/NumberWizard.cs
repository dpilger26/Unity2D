using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    // min/max values for the game
    [SerializeField] int min;
    [SerializeField] int max;
    [SerializeField] TMPro.TextMeshProUGUI guessText;

    // values for updating each guess
    private int minGuess;
    private int maxGuess;
    private int guess;

    // Start is called before the first frame update
    public void Start()
    {
        StartGame();
    }

    // starts the game
    public void StartGame()
    {
        minGuess = min;
        maxGuess = max;
        guess = 0; // just set this to any value to start
        NextGuess();
    }

    // method called with the higher button gets pressed
    public void OnPressHigher()
    {
        minGuess = guess + 1;
        NextGuess();
    }

    // method called when the lower button gets pressed
    public void OnPressLower()
    {
        maxGuess = guess - 1;
        NextGuess();
    }

    // calculates the next guess
    private void NextGuess()
    {
        guess = Random.Range(minGuess, maxGuess + 1);
        guessText.text = guess.ToString();
    }
}
