using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int ZombieType = 0;
    public Animator anim;
    public float speed,damage;

    private bool isAlive = true, isAttacking;

    void Update()
    {
        if (isAlive)
        {
            if (isAttacking)
            {
                transform.LookAt(GameManager.Instance.player.transform);
                var rot = transform.localEulerAngles;
                rot.x = rot.z = 0;
                transform.localEulerAngles = rot;
            }
            else if (ZombieType == 1)
            {
                transform.Translate(transform.forward * -speed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isAlive && other.CompareTag("Player"))
        {
            isAttacking = true ;
            anim.SetInteger("State", (int)AnimationState.ATTACK);
        }
    }

    public void GiveDamage()
    {
        GameManager.Instance.player.GiveDamage(damage);
    }
}