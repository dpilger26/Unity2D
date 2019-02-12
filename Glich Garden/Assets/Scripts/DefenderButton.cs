using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defenderPrefab;

    // cashed references
    DefenderButton[] buttons;
    Color notAvailableColor;

    private void Start()
    {
        buttons = FindObjectsOfType<DefenderButton>();
        notAvailableColor = GetComponent<SpriteRenderer>().color;
    }
    private void OnMouseDown()
    {
        UpdateButtonColors();
        FindObjectOfType<DefenderSpawner>().SetSelectedDefender(defenderPrefab);
    }

    private void UpdateButtonColors()
    {
        foreach (DefenderButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = notAvailableColor;
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
