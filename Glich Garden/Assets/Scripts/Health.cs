using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // configuration parameters
    [SerializeField] int startingHealth = 100;
    [SerializeField] GameObject deathVFX;

    // cached parameters
    LevelController levelController;

    // state parameters
    int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
        levelController = FindObjectOfType<LevelController>();
    }

    // returns true if the object has died
    public bool DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            TriggerDeathSequence();
            return true;
        }

        return false;
    }

    private void TriggerDeathSequence()
    {
        if (deathVFX)
        {
            var explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
        }

        levelController.DecrementAliveAttackers();
        Destroy(gameObject);
    }
}
