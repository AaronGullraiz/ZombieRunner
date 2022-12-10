using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesSpawner : MonoBehaviour
{
    public GameObject[] zombies;

    public Vector3 zombiePos;

    public void StartSpawning()
    {
        SpawnZombie();
        InvokeRepeating("SpawnZombie", 1, 1);
    }

    private void SpawnZombie()
    {
        if (GameManager.Instance.isPlayingGame)
        {
            var rand = Random.Range(0, 3);
            if (rand == 0)
            {
                zombiePos.x = -3;
            }
            else if (rand == 1)
            {
                zombiePos.x = 0;
            }
            else
            {
                zombiePos.x = 3;
            }
            zombiePos.z = GameManager.Instance.player.transform.position.z + Random.Range(80f, 85.5f);

            var zombie = Instantiate(zombies[Random.Range(0, zombies.Length)]) as GameObject;
            zombie.transform.position = zombiePos;
            zombie.transform.forward = Vector3.back;
        }
    }
}