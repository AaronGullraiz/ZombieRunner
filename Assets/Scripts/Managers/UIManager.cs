using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menus, settings, gameplayUI, pauseMenu, gameEnd, hurtOverlay;

    [SerializeField]
    private Text bestDistance, distanceText, healthText, gameEndDistanceText, bulletsCount;

    [SerializeField]
    private Slider musicScroller, soundScroller;

    [Header("Managers")]
    [SerializeField]
    private AudiosManager audiosManager;

    private float distance;

    void Start()
    {
        SetDistanceText("0");
        SetBestDistanceText(Mathf.RoundToInt(PreferenceHandler.BestDistanceTravelled));

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

    public void SetHealthText(float health)
    {
        healthText.text = "HEALTH: " + health;
    }

    public void SetBulletsCount(int count)
    {
        if (count < 0)
        {
            count = 0;
        }
        bulletsCount.text = "BULLETS: " + count;
    }

    public void ShowHurtOverlay()
    {
        hurtOverlay.SetActive(true);
    }

    private void Update()
    {
        if (GameManager.Instance.isPlayingGame)
        {
            distance += Time.deltaTime;
            SetDistanceText(""+Mathf.RoundToInt(distance));
        }
    }

    public void ShowGameEnd()
    {
        if (distance > PreferenceHandler.BestDistanceTravelled)
        {
            PreferenceHandler.BestDistanceTravelled = Mathf.RoundToInt(distance);
            SetBestDistanceText(Mathf.RoundToInt(distance));
        }
        gameEndDistanceText.text = "Distance: " + Mathf.RoundToInt(distance);
        gameEnd.SetActive(true);
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
                    GameManager.Instance.zombieSpawner.StartSpawning();
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
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                    break;
                }
            case "Settings":
                {
                    settings.SetActive(true);
                    break;
                }
            case "Quit":
                {
                    PreferenceHandler.SavePrefs();
                    Application.Quit();
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