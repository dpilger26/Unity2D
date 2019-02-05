using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float laserVelocity = 10f;

    // state parameters
    float yMin;
    float yMax;

    // Start is called before the first frame update
    private void Start()
    {
        SetupBoundary();
        SetInitialVelocity();
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y < yMin || transform.position.y > yMax)
        {
            Destroy(gameObject);
        }
    }

    private void SetupBoundary()
    {
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
    }

    private void SetInitialVelocity()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserVelocity);
    }
}
