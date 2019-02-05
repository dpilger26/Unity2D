using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float boundaryPadding = 0.5f;
    [SerializeField] int startingHealth = 500;
    [SerializeField] int healthGain = 100;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserFiringPeriod = 0.1f;

    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 1f;

    [Header("Audio")]
    [SerializeField] AudioClip projectileSoundClip;
    [SerializeField] [Range(0, 1)] float projectileVolume = 1f;
    [SerializeField] AudioClip deathSoundClip;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;
    [SerializeField] AudioClip extraLifeSoundClip;
    [SerializeField] [Range(0, 1)] float extraLiftVolume = 1f;

    // state parameters
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine fireCoroutine;
    int currentHealth;

    // cached references
    GameSession gameSession;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = startingHealth;
        SetupMoveBoundaries();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Fire();
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            return;
        }

        var damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            ProcessHit(damageDealer);
        }
    }

    private void Move()
    {
        var deltaTime = Time.deltaTime * moveSpeed; // NOTE: makes things time independent
        var deltaX = Input.GetAxis("Horizontal") * deltaTime;
        var deltaY = Input.GetAxis("Vertical") * deltaTime;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    // coroutine
    private IEnumerator FireContinuously()
    {
        while (true)
        {
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(projectileSoundClip, Camera.main.transform.position, projectileVolume);
            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();

        currentHealth = Mathf.Clamp(currentHealth - damageDealer.GetDamage(), 0, startingHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        ExplosionVFX();
        AudioSource.PlayClipAtPoint(deathSoundClip, Camera.main.transform.position, deathVolume);
    }

    private void ExplosionVFX()
    {
        var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + boundaryPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - boundaryPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + boundaryPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - boundaryPadding;
    }

    private void UpdateHealth()
    {
        var extraLives = gameSession.GetEarnedLives();
        if (extraLives > 0)
        {
            currentHealth += extraLives * healthGain;
            AudioSource.PlayClipAtPoint(extraLifeSoundClip, Camera.main.transform.position, extraLiftVolume);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
