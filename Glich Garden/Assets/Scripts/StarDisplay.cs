using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarDisplay : MonoBehaviour
{
    // configurable parameters
    [SerializeField] int startingStars = 100;

    // cashed references
    TMPro.TextMeshProUGUI starText;

    // state parameters
    int currentStars;

    void Start()
    {
        starText = GetComponent<TextMeshProUGUI>();
        currentStars = startingStars;
        UpdateDisplay();
    }

    public bool HaveEnoughStars(int numStars)
    {
        return numStars <= currentStars ? true : false;
    }

    private void UpdateDisplay()
    {
        starText.text = currentStars.ToString();
    }

    public void AddStars(int numStars)
    {
        currentStars += numStars;
        UpdateDisplay();
    }

    public void SubtractStars(int numStars)
    {
        if (currentStars >= numStars)
        {
            currentStars -= numStars;
            UpdateDisplay();
        }
    }
}
