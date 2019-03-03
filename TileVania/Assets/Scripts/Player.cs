using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    // state parameters
    bool isAlive = true;

    // cached references
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidBody;
    Collider2D myCollider;
    Animator myAnimator;

    private void Start()
    {
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        Jump();
        ClimbLadder();
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
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void ClimbLadder()
    {
        if (!IsTouchingLadder())
        {
            UpdateClimbAnimationState(false);
            return;
        }

        var deltaY = Input.GetAxis("Vertical");
        if (deltaY < 0f && IsTouchingGround())
        {
            UpdateClimbAnimationState(false);
            return;
        }

        bool isClimbing = Mathf.Abs(deltaY) > Mathf.Epsilon;

        UpdateClimbAnimationState(isClimbing);
        if (!isClimbing)
        {
            return;
        }

        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, climbSpeed);
    }

    private bool IsTouchingGround()
    {
        return myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsTouchingLadder()
    {
        return myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }
}
