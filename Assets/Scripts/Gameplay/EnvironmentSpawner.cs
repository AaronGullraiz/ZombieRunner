using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public float environmentLength = 762;
    public GameObject[] prefabs;

    private GameObject previousEnvironment, currentEnvironment;

    void Start()
    {
        currentEnvironment = Instantiate(prefabs[Random.Range(0, prefabs.Length)],transform);
        currentEnvironment.transform.position = new Vector3(6.1f,0,environmentLength);
    }

    public void SpawnNewEnvironment()
    {
        if(previousEnvironment)
            Destroy(previousEnvironment);
        previousEnvironment = currentEnvironment;
        currentEnvironment = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
        currentEnvironment.transform.position = new Vector3(6.1f, 0, previousEnvironment.transform.position.z + environmentLength);
    }
}