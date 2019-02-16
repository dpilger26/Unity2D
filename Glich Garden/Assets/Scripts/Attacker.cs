using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    // state parameters
    float currentSpeed = 1f;
    GameObject currentTarget;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);

        UpdateAnimation();
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }

    public void UpdateAnimation()
    {
        if (!currentTarget)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
    }

    public void StrikeCurrentTarget(int damage)
    {
        if (!currentTarget)
        {
            return;
        }

        var health = currentTarget.GetComponent<Health>();
        if (health)
        {
            bool isDead = health.DealDamage(damage);

            if (isDead)
            {
                currentTarget = null;
            }
        }
    }
}
