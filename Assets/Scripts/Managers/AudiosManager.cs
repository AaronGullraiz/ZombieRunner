using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosManager : MonoBehaviour
{
    public AudioSource bgMusicSource, effectSource;

    public AudioData[] audios;

    public AudioClip[] zombieAttack;
    public AudioClip[] zombieDeath;

    private Dictionary<string, AudioClip> sounds;

    void Start()
    {
        SetVolume();

        sounds = new Dictionary<string, AudioClip>();
        foreach (var item in audios)
        {
            sounds.Add(item.clipName, item.clip);
        }
    }

    public void SetVolume()
    {
        bgMusicSource.volume = PreferenceHandler.MusicVolume;
        effectSource.volume = PreferenceHandler.SoundVolume;
    }

    public void PlaySoundEffect(string clipName)
    {
        effectSource.PlayOneShot(sounds[clipName]);
    }

    public void PlayZombieDeathSound()
    {
        effectSource.PlayOneShot(zombieDeath[Random.Range(0, zombieDeath.Length)]);
    }

    public void PlayZombieAttackSound()
    {
        effectSource.PlayOneShot(zombieAttack[Random.Range(0, zombieDeath.Length)]);
    }
}

[System.Serializable]
public class AudioData
{
    public string clipName;
    public AudioClip clip;
}