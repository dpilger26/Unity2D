using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsController : MonoBehaviour
{
    // configuration parameters
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider difficultySlider;


    // constants
    const string MASTER_VOLUME_KEY = "master volume";
    const string DIFFICULTY_KEY = "difficulty";

    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    const float MIN_DIFFICULTY = 0f;
    const float MAX_DIFFICULTY = 1f;

    private void Start()
    {
        volumeSlider.value = GetMasterVolume();
        difficultySlider.value = GetDifficulty();
    }

    private void Update()
    {
        SetMasterVolume(volumeSlider.value);
        SetDifficulty(difficultySlider.value);
    }

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master Volume is out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetDifficulty(float difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty is out of range");
        }
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }

    public static void SetDefaults()
    {
        SetMasterVolume(0.5f);
        SetDifficulty(0.5f);
    }
}
