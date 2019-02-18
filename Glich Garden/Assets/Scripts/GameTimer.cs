using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // configurable parameters
    [Tooltip("Our Level Timer in secodns")]
    [SerializeField] float levelTime = 10f;

    // cached parameters
    Slider timeSlider;

    private void Start()
    {
        timeSlider = GetComponent<Slider>();

        UpdateSlider();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();

        if (LevelFinished())
        {
            Debug.Log("Level Finished");
        }
    }

    void UpdateSlider()
    {
        timeSlider.value = Time.timeSinceLevelLoad / levelTime;
    }

    bool LevelFinished()
    {
        return Time.timeSinceLevelLoad >= levelTime ? true : false;
    }
}
