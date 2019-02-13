using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    //  configurable parameters
    [SerializeField] int starCost = 100;

    // cached parameters
    StarDisplay starDisplay;

    private void Update()
    {
        starDisplay = FindObjectOfType<StarDisplay>();
    }

    public void AddStars(int numStars)
    {
        starDisplay.AddStars(numStars);
    }

    public int GetStarCost()
    {
        return starCost;
    }
}
