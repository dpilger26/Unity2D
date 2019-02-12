using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // state parameters
    [SerializeField] float speed = 1f;
    //[SerializeField] float rotationRate = 360f;
    [SerializeField] int damage = 100;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        //transform.Rotate(new Vector3(0, 0, rotationRate * Time.deltaTime));

        if (transform.position.x > 12)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    //public void SetRotationRate(float newRotationRate)
    //{
    //    rotationRate = newRotationRate;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHit(collision);
    }

    private void HandleHit(Collider2D collision)
    {
        var health = collision.GetComponent<Health>();
        var attacker = collision.GetComponent<Attacker>();

        if (attacker && health)
        {
            health.DealDamage(damage);
            Destroy(gameObject);
        }
    }
}
