using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // configuration parameters
    [SerializeField] int pointsPerCoin = 10;
    [SerializeField] AudioClip coinPickupClip;
    [SerializeField] [Range(0, 1)] float coinPickupVolume = 0.5f;

    // cached parameters
    GameScore gameScore;

    private void Start()
    {
        gameScore = FindObjectOfType<GameScore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ConsumeCoin();
        }
    }

    private void ConsumeCoin()
    {
        AudioSource.PlayClipAtPoint(coinPickupClip, Camera.main.transform.position, coinPickupVolume);
        gameScore.IncramentScore(pointsPerCoin);
        Destroy(gameObject);
    }
}
