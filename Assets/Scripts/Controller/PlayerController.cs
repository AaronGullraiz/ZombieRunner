using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strafDistance,speed;

    public Animator playerAnimator;

    public Transform bulletSpawnPosition;
    public GameObject bullet;
    public GameObject pistolInHand,smgInHand;
    public ParticleSystem bloodSplat, bloodPool;

    private bool isAlive = true;
    private Transform player;

    private Vector3 currentWaypoint;

    private float health = 100;

    private IEnumerator fireRoutine;

    private bool isFiring;

    private bool hasPistol, hasSMG;
    private int bulletsCount = 0;

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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            isFiring = true;
            if (fireRoutine == null && bulletsCount>0)
            {
                fireRoutine = Fire();
                StartCoroutine(fireRoutine);
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
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
        while (isFiring && bulletsCount>0)
        {
            var bull = Instantiate(bullet,GameManager.Instance.bulletsParent) as GameObject;
            bull.transform.position = bulletSpawnPosition.position;
            bull.transform.forward = bulletSpawnPosition.forward;
            bulletsCount--;
            if (hasPistol)
            {
                GameManager.Instance.audioManager.PlaySoundEffect("pistolFire");
            }
            else
            {
                GameManager.Instance.audioManager.PlaySoundEffect("smgFire");
            }
            GameManager.Instance.uiManager.SetBulletsCount(bulletsCount);
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
        if (bulletsCount <= 0)
        {
            hasPistol = hasSMG = false;
            playerAnimator.SetInteger("State", (int)AnimationState.RUN);
            smgInHand.SetActive(false);
            pistolInHand.SetActive(false);
        }
        isFiring = false;
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
            bloodSplat.Play();
            health -= damage;
            GameManager.Instance.uiManager.ShowHurtOverlay();
            if (health <= 0)
            {
                health = 0;
                isAlive = false;
                GameManager.Instance.isPlayingGame = false;
                playerAnimator.SetInteger("State", (int)AnimationState.DEATH);
                bloodPool.Play();
                Invoke("EndGame", 5);
                GameManager.Instance.audioManager.PlaySoundEffect("Death");
            }
            else
            {
                GameManager.Instance.audioManager.PlaySoundEffect("Hurt");
            }
            GameManager.Instance.uiManager.SetHealthText(health);
        }
    }

    private void EndGame()
    {
        GameManager.Instance.uiManager.ShowGameEnd();
    }

    public void SetWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.PISTOL:
                if(hasPistol)
                    bulletsCount += Utilities.PistolBulets;
                else
                    bulletsCount = Utilities.PistolBulets;

                hasPistol = true;
                hasSMG = false;
                playerAnimator.SetInteger("State", (int)AnimationState.RunWithGun);
                pistolInHand.SetActive(true);
                smgInHand.SetActive(false);
                break;
            case WeaponType.SMG:
                if(hasSMG)
                    bulletsCount += Utilities.SMGBulets;
                else
                    bulletsCount = Utilities.SMGBulets;

                hasPistol = false;
                hasSMG = true;
                playerAnimator.SetInteger("State", (int)AnimationState.RunWithGun);
                smgInHand.SetActive(true);
                pistolInHand.SetActive(false);
                break;
            default:
                break;
        }
        GameManager.Instance.uiManager.SetBulletsCount(bulletsCount);
    }
}