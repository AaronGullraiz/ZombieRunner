using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Invoke("KillBullet", 5);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed);
    }

    void KillBullet()
    {
        Destroy(gameObject);
    }
}