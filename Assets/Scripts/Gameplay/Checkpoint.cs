using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform[] waypoints;

    private void Start()
    {
        foreach (var item in waypoints)
        {
            GameManager.Instance.waypoint.Add(item);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.environmentSpawner.SpawnNewEnvironment();
        }
    }
}