using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    // state parameters
    float currentSpeed = 1f;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);

        if (transform.position.x < -2)
        {
            Destroy(gameObject);
        }
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
