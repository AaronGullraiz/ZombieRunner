using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public AudiosManager audioManager;

    public EnvironmentSpawner environmentSpawner;


    #region Instance

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
}