using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    // cashed references
    TMPro.TextMeshProUGUI healthText;
    Base baseObject;

    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        baseObject = FindObjectOfType<Base>();
        UpdateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        healthText.text = baseObject.GetCurrentHealth().ToString();
    }
}
