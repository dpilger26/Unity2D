using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    // configurable parameters
    Defender defenderPrefab;

    // cashed parameters
    StarDisplay starDisplay;
    GameObject defenderParent; // keep the heirarchy clean!

    // constants
    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void Update()
    {
        starDisplay = FindObjectOfType<StarDisplay>();
    }

    public void SetSelectedDefender(Defender selectedDefender)
    {
        defenderPrefab = selectedDefender;
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        if (starDisplay.HaveEnoughStars(defenderPrefab.GetStarCost()))
        {
            SpawnDefender(gridPos);
        }
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 worldPos)
    {
        return new Vector2(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));
    }

    private void SpawnDefender(Vector2 worldPos)
    {
        if (!SquareOccupied(worldPos))
        {
            var newDefender = Instantiate(defenderPrefab, worldPos, Quaternion.identity) as Defender;
            newDefender.transform.parent = defenderParent.transform;

            starDisplay.SubtractStars(defenderPrefab.GetStarCost());
        }
    }

    private bool SquareOccupied(Vector2 worldPos)
    {
        var currentDefenders = FindObjectsOfType<Defender>();
        foreach (Defender defender in currentDefenders)
        {
            if (Mathf.Abs(defender.transform.position.x - worldPos.x) <= Mathf.Epsilon &&
                Mathf.Abs(defender.transform.position.y - worldPos.y) <= Mathf.Epsilon)
            {
                return true;
            }
        }

        return false;
    }
}
