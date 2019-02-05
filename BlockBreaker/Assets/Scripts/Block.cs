using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // configuration parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached references
    Level level;
    GameSession gameSession;

    // state variables
    private int numCurrentHits = 0;

    // Start is called before the first frame update
    private void Start()
    {
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.IncramentBlocks();
        }

        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // play the hit sound
        PlayBreakSFX();

        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        numCurrentHits++;

        if (numCurrentHits > hitSprites.Length)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = numCurrentHits - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[numCurrentHits - 1];
        }
    }

    private void DestroyBlock()
    {
        // destroy the block
        Destroy(gameObject);

        // trigger the destroy animation
        TriggerSparklesVFX();

        // let the level object know that the object has been destroyed
        level.BlockDestroyed();

        // incrament the score in the game statis
        gameSession.IncramentScore();
    }

    private void PlayBreakSFX()
    {
        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }
    }

    private void TriggerSparklesVFX()
    {
        var sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
