using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Speed Options")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float deathSpeed = 100f;
    [SerializeField] float respawnDelay = 2f;

    [Header("Sound Options")]
    [SerializeField] AudioClip jumpClip;
    [SerializeField] [Range(0, 1)] float jumpVolume = 0.25f;
    [SerializeField] AudioClip deathClip;
    [SerializeField] [Range(0, 1)] float deathVolume = 0.5f;

    // constants
    float beginningGravityScale;

    // state parameters
    bool isAlive = true;

    // cached references
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCollider;
    BoxCollider2D feetCollider;
    Animator myAnimator;
    GameScore gameScore;

    private void Start()
    {
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        gameScore = FindObjectOfType<GameScore>();

        beginningGravityScale = myRigidBody.gravityScale;
    }

    private void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        Jump();
        ClimbLadder();

        if (IsTouchingHazard())
        {
            DeathSequence();
        }
    }

    private void Run()
    {
        var deltaX = Input.GetAxis("Horizontal");
        bool isMoving = Mathf.Abs(deltaX) > Mathf.Epsilon;

        UpdateRunAnimationState(isMoving);
        if (!isMoving)
        {
            return;
        }

        UpdateSpriteDirection(deltaX);

        var deltaTime = Time.deltaTime * runSpeed; // NOTE: makes things time independent
        transform.position = new Vector2(transform.position.x + deltaX * deltaTime, transform.position.y);
    }

    private void UpdateSpriteDirection(float deltaX)
    {
        mySpriteRenderer.flipX = deltaX < 0 ? true : false;
    }

    private void UpdateRunAnimationState(bool isRunning)
    {
        myAnimator.SetBool("isRunning", isRunning);
    }

    private void UpdateClimbAnimationState(bool isClimbing)
    {
        myAnimator.SetBool("isClimbing", isClimbing);
    }

    private void Jump()
    {
        if (!IsTouchingGround())
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            PlayJumpSound();
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void ClimbLadder()
    {
        if (!IsTouchingLadder())
        {
            UpdateClimbAnimationState(false);
            myRigidBody.gravityScale = beginningGravityScale;
            return;
        }

        var deltaY = Input.GetAxis("Vertical");
        bool isClimbing = Mathf.Abs(deltaY) > Mathf.Epsilon;
        UpdateClimbAnimationState(isClimbing);

        myRigidBody.gravityScale = 0f;
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, deltaY * climbSpeed);
    }

    private bool IsTouchingGround()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsTouchingLadder()
    {
        return myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    private bool IsTouchingHazard()
    {
        return myCollider.IsTouchingLayers(LayerMask.GetMask("Water2", "Spikes"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        PlayDeathSound();

        isAlive = false;
        myAnimator.SetTrigger("isDead");
        mySpriteRenderer.color = Color.red;
        myRigidBody.velocity = new Vector2(0, deathSpeed);
        myCollider.enabled = false;

        StartCoroutine(ResartLevel());
    }

    IEnumerator ResartLevel()
    {
        yield return new WaitForSecondsRealtime(respawnDelay);
        gameScore.ResetScoreOnDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlayJumpSound()
    {
        AudioSource.PlayClipAtPoint(jumpClip, Camera.main.transform.position, jumpVolume);
    }

    private void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathVolume);
    }
}
