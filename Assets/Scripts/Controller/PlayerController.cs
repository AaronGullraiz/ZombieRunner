using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Animator playerAnimator;

    private bool isAlive = true;

    private Vector3 currentWaypoint;

    void Start()
    {
        
    }

    void Update()
    {
        if (isAlive && GameManager.Instance.isPlayingGame)
        {
            if (playerAnimator.GetInteger("State") < 1)
            {
                playerAnimator.SetInteger("State", 1);
            }
            var dist = Vector3.Distance(transform.position, currentWaypoint);
            Debug.Log("Dist: " + dist);
            if (dist < 2.5f)
            {
                currentWaypoint = GameManager.Instance.GetNextWaypoint().position;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Time.deltaTime * speed);
        }
    }

    public void SetFirstWaypoint(Transform waypoint)
    {
        currentWaypoint = waypoint.position;
        currentWaypoint.y = transform.position.y;
    }
}