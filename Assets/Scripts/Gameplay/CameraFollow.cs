using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    public float followSpeed;

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position + offset, Time.deltaTime*followSpeed);
    }
}