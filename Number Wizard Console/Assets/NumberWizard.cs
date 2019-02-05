using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    private const int minValue = 1;
    private const int maxValue = 1000;

    private int minGuess = minValue;
    private int maxGuess = maxValue;
    private int guess;
    private bool newGuess = true;

    // Start is called before the first frame update
    public void Start()
    {
        StartGame();
    }

    // starts the game
    public void StartGame()
    {
        Debug.Log("Welcome to Number Wizard.");
        Debug.Log("Pick a number, don't tell me what it is...");
        Debug.Log("Highest number you can pick is: " + maxValue);
        Debug.Log("Lowest number you can pick is: " + minValue);
        Debug.Log("Tell me if your number is higher or lower than my guess.");
        Debug.Log("Push Up = Higher, Push Down = Lower, Push Enter = Correct");
        NextGuess();
    }

    // Update is called once per frame
    public void Update()
    {
        if (newGuess)
        {
            NextGuess();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            minGuess = guess;
            newGuess = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            maxGuess = guess;
            newGuess = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Yahtzi!");
            Reset();
        }
    }

    // calculates the next guess
    private void NextGuess()
    {
        guess = (minGuess + maxGuess) / 2;

        Debug.Log("Is your number " + guess + "?");

        newGuess = false;
    }

    // Resets the variables to run the game again
    private void Reset()
    {
        newGuess = true;
        minGuess = minValue;
        maxGuess = maxValue;
        guess = (minGuess + maxGuess) / 2;

        StartGame();
    }
}
