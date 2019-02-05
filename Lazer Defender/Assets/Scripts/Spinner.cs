using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float spinRate = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, spinRate * Time.deltaTime));
    }
}
