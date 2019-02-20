using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] [Range(0, 1)] float defaultVolume = 0.8f;
    [SerializeField] Slider difficultySlider;
    [SerializeField] [Range(0, 1)] float defaultDifficulty = 0.5f;

    // cashed references
    MusicPlayer musicPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();

        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        difficultySlider.value = PlayerPrefsController.GetDifficulty();
    }

    // Update is called once per frame
    private void Update()
    {
        musicPlayer.SetVolume(volumeSlider.value);
    }

    public void SetDefaults()
    {
        PlayerPrefsController.SetMasterVolume(defaultVolume);
        volumeSlider.value = defaultVolume;

        PlayerPrefsController.SetDifficulty(defaultDifficulty);
        difficultySlider.value = defaultDifficulty;
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        PlayerPrefsController.SetDifficulty(difficultySlider.value);
        FindObjectOfType<LevelLoader>().LoadStartMenu();
    }
}
