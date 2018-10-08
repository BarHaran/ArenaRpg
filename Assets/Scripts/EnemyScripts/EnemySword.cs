using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Properites;

public class EnemySword : MonoBehaviour
{
    //public bool blocking;
    public bool attacking;
    public bool attackPending;
    //Transform arm;
    public EnemySwordControl sword;
    public EnemyController controller;

    protected void Start()
    {
        attacking = false;
        attackPending = true;
        //blocking = false;
        //arm = transform.GetChild(0);
        Physics.IgnoreCollision(GetComponentInChildren<Collider>(), transform.parent.GetComponent<Collider>());
    }

    //public void Block()
    //{
    //    if (blocking)
    //    {
    //        if (attacking)
    //        {
    //            attacking = false;
    //            attackPending = true;
    //        }
    //        transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, -30, sword.Speed * Time.deltaTime), 0, 0);
    //        arm.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arm.localEulerAngles.z, 90, 0.2f));
    //    }
    //    else
    //    {
    //        if (!attacking)
    //            transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, -10, sword.Speed * Time.deltaTime), 0, 0);
    //        arm.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arm.localEulerAngles.z, 0, 0.2f));
    //    }
    //}

    public void Attack()
    {
        //if (!blocking)
        {
            if (attackPending)
            {
                if (controller.attemptAttack)
                {
                    attacking = true;
                    attackPending = false;
                    sword.hitState = HitState.Attacking;
                }
            }
            else if (attacking)
            {
                transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, 40, sword.Speed * Time.deltaTime * 1.5f), 0, 0);
                if (transform.localEulerAngles.x > 39 && transform.localEulerAngles.x < 60)
                    attacking = false;
            }
            else
            {
                if (transform.localEulerAngles.x > 345 && transform.localEulerAngles.x < 355)
                {
                    attackPending = true;
                    sword.hitState = HitState.Pending;
                }
            }
        }
    }

    void ResetAttack()
    {
        if (sword.hitState == HitState.Blocked)
        {
            attacking = false;
            sword.hitState = HitState.Pending;
        }
    }
}
