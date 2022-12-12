using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedWeaponHandler : MonoBehaviour
{
    public WeaponType weapon = WeaponType.PISTOL;

    public GameObject pistol, Smg;

    private void Start()
    {
        if (Random.Range(0f, 1f) > 0.5f)
        {
            weapon = WeaponType.SMG;
        }

        if(weapon == WeaponType.SMG)
        {
            pistol.SetActive(false);
            Smg.SetActive(true);
        }
        else 
        {
            pistol.SetActive(true);
            Smg.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerController>().SetWeapon(weapon);
            Destroy(gameObject);
        }
    }
}