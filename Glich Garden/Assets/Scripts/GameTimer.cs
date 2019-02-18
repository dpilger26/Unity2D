using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // configurable parameters
    [Tooltip("Our Level Timer in seconds")]
    [SerializeField] float levelTime = 10f;

    // cached parameters
    Slider timeSlider;

    private void Start()
    {
        timeSlider = GetComponent<Slider>();
        UpdateSlider();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        if (TimerComplete())
        {
            return;
        }

        timeSlider.value = Time.timeSinceLevelLoad / levelTime;
    }

    public bool TimerComplete()
    {
        return Time.timeSinceLevelLoad >= levelTime ? true : false;
    }
}
