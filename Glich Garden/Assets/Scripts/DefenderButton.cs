using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        LabelButtonsWithCost();
    }

    private void LabelButtonsWithCost()
    {
        Text costText = GetComponentInChildren<Text>();
        costText.text = defenderPrefab.GetStarCost().ToString();
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
