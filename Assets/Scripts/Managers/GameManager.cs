using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public AudiosManager audioManager;

    public EnvironmentSpawner environmentSpawner;

    public PlayerController player;

    public List<Transform> waypoint;

    public ZombiesSpawner zombieSpawner;

    public bool isPlayingGame;

    #region Instance

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public Transform GetNextWaypoint()
    {
        waypoint.Remove(waypoint[0]);
        return waypoint[0];
    }

    private void OnDisable()
    {
        PreferenceHandler.SavePrefs();
    }
}