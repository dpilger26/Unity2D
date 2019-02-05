using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private float screenWidthInUnits;
    private float minX;
    private float maxX;

    // cached references
    Ball ball;
    GameSession gameSession;

    // Start is called before the first frame update
    private void Start()
    {
        Camera camera = Camera.main;

        float height = 2f * camera.orthographicSize;
        screenWidthInUnits = height * camera.aspect;

        minX = 1f; // the paddle width is 2.0;
        maxX = screenWidthInUnits - 1f;

        ball = FindObjectOfType<Ball>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    private void Update()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
