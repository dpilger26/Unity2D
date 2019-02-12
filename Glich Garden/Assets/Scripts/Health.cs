using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // configuration parameters
    [SerializeField] int startingHealth = 100;
    [SerializeField] GameObject deathVFX;

    // state parameters
    int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            TriggerDeathSequence();
        }
    }

    private void TriggerDeathSequence()
    {
        if (deathVFX)
        {
            var explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
        }
        Destroy(gameObject);
    }
}
