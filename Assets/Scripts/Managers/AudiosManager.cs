using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosManager : MonoBehaviour
{
    public AudioSource bgMusicSource, effectSource;

    public AudioData[] audios;

    void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        bgMusicSource.volume = PreferenceHandler.MusicVolume;
        effectSource.volume = PreferenceHandler.SoundVolume;
    }

    public void PlaySoundEffect(string clipName)
    {

    }


}

[System.Serializable]
public class AudioData
{
    public string clipName;
    public AudioClip clip;
}