using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // cashed parameters
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<MusicPlayer>().Length == 1)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
