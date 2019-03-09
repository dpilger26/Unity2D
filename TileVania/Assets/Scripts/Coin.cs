using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupClip;
    [SerializeField] [Range(0, 1)] float coinPickupVolume = 0.5f;

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
        Destroy(gameObject);
    }
}
