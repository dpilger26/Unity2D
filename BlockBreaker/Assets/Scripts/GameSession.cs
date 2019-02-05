using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // configuration parameters
    [Range(0f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 10;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    // state
    private int currentScore = 0; // serialized for debug purposes

    // the first thing that gets called by unity
    // see https://docs.unity3d.com/Manual/ExecutionOrder.html for execution order of
    // unity events
    private void Awake()
    {
        // way to implement singleton object in unity
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            gameObject.SetActive(false); // needed due to event execution order
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called once at the beginning of the game
    private void Start()
    {
        DisplayScore();
    }

    // Update is called once per frame
    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    // incraments the current score
    public void IncramentScore()
    {
        currentScore += pointsPerBlockDestroyed;
        DisplayScore();
    }

    // display the score on the screen
    private void DisplayScore()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
