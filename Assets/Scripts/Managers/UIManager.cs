using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menus, gameplayUI, pauseMenu;

    [SerializeField]
    private Text distanceText;

    void Start()
    {
        SetDistanceText("0");
    }

    public void SetDistanceText(string distance)
    {
        distanceText.text = "Distance: " + distance;
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
                    break;
                }
            case "Pause":
                {
                    gameplayUI.SetActive(false);
                    pauseMenu.SetActive(true);
                    break;
                }
            case "Resume":
                {
                    pauseMenu.SetActive(false);
                    gameplayUI.SetActive(true);
                    break;
                }
            case "End":
                {
                    gameplayUI.SetActive(false);
                    pauseMenu.SetActive(false);
                    menus.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }

    #endregion
}