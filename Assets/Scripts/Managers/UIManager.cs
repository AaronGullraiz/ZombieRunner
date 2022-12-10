using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menus, settings, gameplayUI, pauseMenu;

    [SerializeField]
    private Text bestDistance, distanceText;

    [SerializeField]
    private Slider musicScroller, soundScroller;

    [Header("Managers")]
    [SerializeField]
    private AudiosManager audiosManager;

    void Start()
    {
        SetDistanceText("0");

        SetBestDistanceText(PreferenceHandler.BestDistanceTravelled);

        musicScroller.value = PreferenceHandler.MusicVolume;
        soundScroller.value = PreferenceHandler.SoundVolume;
    }

    public void SetDistanceText(string distance)
    {
        distanceText.text = "Distance: " + distance;
    }

    public void SetBestDistanceText(float distance)
    {
        bestDistance.text = "BEST: " + distance;
    }


    #region Click Events

    public void ButtonPressed(string btn)
    {
        switch (btn)
        {
            case "Play":
                {
                    menus.SetActive(false);
                    gameplayUI.SetActive(true);

                    GameManager.Instance.isPlayingGame = true;
                    GameManager.Instance.player.SetFirstWaypoint(GameManager.Instance.waypoint[0]);
                    break;
                }
            case "Pause":
                {
                    gameplayUI.SetActive(false);
                    pauseMenu.SetActive(true);
                    GameManager.Instance.isPlayingGame = false;
                    break;
                }
            case "Resume":
                {
                    pauseMenu.SetActive(false);
                    gameplayUI.SetActive(true);
                    GameManager.Instance.isPlayingGame = true;
                    break;
                }
            case "End":
                {
                    gameplayUI.SetActive(false);
                    pauseMenu.SetActive(false);
                    menus.SetActive(true);
                    break;
                }
            case "Settings":
                {
                    settings.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }

    public void OnSettingsScrollerUpdated(string scroller)
    {
        switch (scroller)
        {
            case "music":
                {
                    PreferenceHandler.MusicVolume = musicScroller.value;
                    break;
                }
            case "sound":
                {
                    PreferenceHandler.SoundVolume = soundScroller.value;
                    break;
                }
            default:
                break;
        }
        audiosManager.SetVolume();
    }

    #endregion
}