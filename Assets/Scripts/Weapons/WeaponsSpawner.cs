using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSpawner : MonoBehaviour
{
    public GameObject weapons;

    public float delayTime = 2;

    private int delay=1;

    void Start()
    {
        StartCoroutine(SpawnWeapon());
    }

    IEnumerator SpawnWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime*delay);
            if (GameManager.Instance.isPlayingGame)
            {
                delay++;
                var ob = Instantiate(weapons, transform) as GameObject;
                var pos = GameManager.Instance.player.transform.position + 
                    (GameManager.Instance.player.transform.forward * 80);
                var rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    pos.x = -3;
                }
                else if (rand == 1)
                {
                    pos.x = 0;
                }
                else
                {
                    pos.x = 3;
                }
                ob.transform.position = pos;
            }
        }
    }
}