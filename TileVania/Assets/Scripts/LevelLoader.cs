using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // cashed parameters
    GameScore gameScore;

    private void Start()
    {
        gameScore = FindObjectOfType<GameScore>();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameScore.ResetScore();
    }
}
