using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float runSpeed = 5f;

    // cached references
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
    }

    private void Run()
    {
        var deltaTime = Time.deltaTime * runSpeed; // NOTE: makes things time independent
        var deltaX = Input.GetAxis("Horizontal") * deltaTime;

        if (Mathf.Abs(deltaX) < Mathf.Epsilon)
        {
            // not moving
            return;
        }

        UpdateSpriteDirection(deltaX);
        transform.position = new Vector2(transform.position.x + deltaX, transform.position.y);
    }

    private void UpdateSpriteDirection(float deltaX)
    {
        spriteRenderer.flipX = deltaX < 0 ? true : false;
    }
}
