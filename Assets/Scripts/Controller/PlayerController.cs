using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strafDistance,speed;

    public Animator playerAnimator;

    public Transform bulletSpawnPosition;
    public GameObject bullet;

    private bool isAlive = true;
    private Transform player;

    private Vector3 currentWaypoint;

    private float health = 100;

    private IEnumerator fireRoutine;

    private bool isFiring;

    private bool hasPistol, hasSMG;

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
        #region Controls
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (childPos.x > -strafDistance)
            {
                childPos.x -= strafDistance;
            }
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (childPos.x < strafDistance)
            {
                childPos.x += strafDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            isFiring = true;
            if (fireRoutine == null)
            {
                fireRoutine = Fire();
                StartCoroutine(fireRoutine);
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isFiring = false;
        }
        #endregion

        #region movement
        if (isAlive && GameManager.Instance.isPlayingGame)
        {
            if (playerAnimator.GetInteger("State") < (int)AnimationState.RUN)
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
        #endregion
    }

    private IEnumerator Fire()
    {
        float fireRate = Utilities.PistolFireRate;
        while (isFiring)
        {
            var bull = Instantiate(bullet,GameManager.Instance.bulletsParent) as GameObject;
            bull.transform.position = bulletSpawnPosition.position;
            bull.transform.forward = bulletSpawnPosition.forward;

            if (hasPistol)
            {
                fireRate = Utilities.PistolFireRate;
            }
            else if (hasSMG)
            {
                fireRate = Utilities.SMGFireRate;
            }
            yield return new WaitForSeconds(fireRate);
        }
        fireRoutine = null;
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