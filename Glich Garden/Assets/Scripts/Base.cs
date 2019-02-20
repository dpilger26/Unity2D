using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    // constants
    const float BASE_HEALTH = 4;

    // cached parameters
    LevelController levelController;

    // state parameters
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = BASE_HEALTH - PlayerPrefsController.GetDifficulty();
        levelController = FindObjectOfType<LevelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHit(collision);
    }

    private void HandleHit(Collider2D collision)
    {
        Destroy(collision.gameObject);

        levelController.DecrementAliveAttackers();

        currentHealth--;
        if (currentHealth <= 0)
        {
            levelController.TriggerLossScreen();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
