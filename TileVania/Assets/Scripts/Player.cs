﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float runSpeed = 5f;

    // state parameters
    bool isAlive = true;

    // cached references
    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;

    private void Start()
    {
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
    }

    private void Run()
    {
        var deltaX = Input.GetAxis("Horizontal");
        bool isMoving = Mathf.Abs(deltaX) > Mathf.Epsilon;

        UpdateAnimationState(isMoving);
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

    private void UpdateAnimationState(bool isRunning)
    {
        myAnimator.SetBool("isRunning", isRunning);
    }
}