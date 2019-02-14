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

    private void Start()
    {
        SetLaneSpawner();
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            Debug.Log("Pew Pew");
        }
        else
        {
            Debug.Log("sit and wait");
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
