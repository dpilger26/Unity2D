using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // configuration parameters
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] GameObject gun;

    // cashed parameters
    AttackerSpawner[] attackerSpawners;
    AttackerSpawner myLaneSpawner;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetLaneSpawner();
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("isAttacking", true); // sets a variable in the animator
        }
        else
        {
            animator.SetBool("isAttacking", false); // sets a variable in the animator
        }
    }

    private void SetLaneSpawner()
    {
        attackerSpawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in attackerSpawners)
        {
            bool isCloseEnough = (Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon);
            if (isCloseEnough)
            {
                myLaneSpawner = spawner;
                break;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        return myLaneSpawner.transform.childCount > 0 ? true : false;
    }

    public void ThrowProjectile()
    {
        Instantiate(projectilePrefab, gun.transform.position, Quaternion.identity);
    }
}
