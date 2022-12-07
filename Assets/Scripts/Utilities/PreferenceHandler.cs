using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PreferenceHandler
{
    public static float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("Music", 1);
        }
        set
        {
            PlayerPrefs.SetFloat("Music", value);
        }
    }

    public static float SoundVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("Sound", 1);
        }
        set
        {
            PlayerPrefs.SetFloat("Sound", value);
        }
    }

    public static float BestDistanceTravelled
    {
        get
        {
            return PlayerPrefs.GetFloat("Distance", 0);
        }
        set
        {
            PlayerPrefs.SetFloat("Distance", value);
        }
    }
}