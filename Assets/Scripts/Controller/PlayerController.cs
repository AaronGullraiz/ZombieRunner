using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strafDistance,speed;

    public Animator playerAnimator;

    private bool isAlive = true;
    private Transform player;

    private Vector3 currentWaypoint;

    private float health = 100;


    Vector3 childPos;

    void Start()
    {
        player = transform.GetChild(0);
        childPos = player.transform.localPosition;
        childPos.y = childPos.z = 0;

        GameManager.Instance.uiManager.SetHealthText(health);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (childPos.x > -strafDistance)
            {
                childPos.x -= strafDistance;
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if (childPos.x < strafDistance)
            {
                childPos.x += strafDistance;
            }
        }

        if (isAlive && GameManager.Instance.isPlayingGame)
        {
            if (playerAnimator.GetInteger("State") < 1)
            {
                playerAnimator.SetInteger("State", (int)AnimationState.RUN);
            }
            var dist = Vector3.Distance(transform.position, currentWaypoint);
            Debug.Log("Dist: " + dist);
            if (dist < 2.5f)
            {
                currentWaypoint = GameManager.Instance.GetNextWaypoint().position;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Time.deltaTime * speed);
            player.localPosition = Vector3.MoveTowards(player.localPosition, childPos, Time.deltaTime * speed);
        }
    }

    public void SetFirstWaypoint(Transform waypoint)
    {
        currentWaypoint = waypoint.position;
        currentWaypoint.y = transform.position.y;
    }

    public void GiveDamage(float damage)
    {
        if (isAlive)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isAlive = false;
                playerAnimator.SetInteger("State", (int)AnimationState.DEATH);

                Invoke("EndGame", 5);
            }
            GameManager.Instance.uiManager.SetHealthText(health);
        }
    }

    private void EndGame()
    {
        GameManager.Instance.uiManager.ShowGameEnd();
    }
}