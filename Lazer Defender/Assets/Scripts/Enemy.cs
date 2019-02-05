using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // configuration parameters
    [Header("Enemy")]
    [SerializeField] int health = 100;
    [SerializeField] int scoreValue = 83;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float minTimeBetweenBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenBetweenShots = 1f;

    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 1f;

    [Header("Audio")]
    [SerializeField] AudioClip projectileSoundClip;
    [SerializeField] [Range(0, 1)] float projectileVolume = 1f;
    [SerializeField] AudioClip deathSoundClip;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;

    // state parameters
    float shotCounter;

    // cashed parameters
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ResetShotCounter();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void ResetShotCounter()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenBetweenShots, maxTimeBetweenBetweenShots);
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void Fire()
    {
        var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(projectileSoundClip, Camera.main.transform.position, projectileVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            return;
        }

        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();

        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameSession.AddToScore(scoreValue);
        Destroy(gameObject);
        ExplosionVFX();
        AudioSource.PlayClipAtPoint(projectileSoundClip, Camera.main.transform.position, deathVolume);
    }

    private void ExplosionVFX()
    {
        var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }
}
