using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var otherObject = collision.gameObject;
        if (otherObject.GetComponent<Defender>())
        {
            if (otherObject.GetComponent<GraveStone>())
            {
                GetComponent<Animator>().SetTrigger("jumpTrigger");
            }
            else
            {
                GetComponent<Attacker>().Attack(otherObject);
            }
        }
    }
}
