using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // configuration parameters
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] GameObject gun;

    public void ThrowProjectile()
    {
        Instantiate(projectilePrefab, gun.transform.position, Quaternion.identity);
    }
}
