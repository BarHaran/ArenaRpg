using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Properites;

public class SwordMovement : MonoBehaviour
{
    SwordProperties properties;
    bool blocking;
    bool attacking;
    bool attackPending;
    Transform arm;
    HitState hit;

    void Start()
    {
        attacking = false;
        attackPending = true;
        properties = GetComponentInChildren<SwordProperties>();
        hit = properties.hit;
        blocking = false;
        arm = transform.GetChild(0);
        Physics.IgnoreCollision(GetComponentInChildren<Collider>(), transform.parent.GetComponent<Collider>());
    }

    void FixedUpdate()
    {
        Block();
        Attack();
    }

    void Block()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            blocking = true;
            if (attacking)
            {
                if (hit == HitState.Attacking)
                {
                    hit = HitState.Pending;
                    properties.ResetCombo();
                }
                attacking = false;
                attackPending = true;
            }
            transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, -30, properties.Speed * Time.deltaTime), 0, 0);
            arm.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arm.localEulerAngles.z, 90, 0.2f));
        }
        else
        {
            blocking = false;
            if (!attacking)
                transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, -10, properties.Speed * Time.deltaTime), 0, 0);
            arm.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arm.localEulerAngles.z, 0, 0.2f));
        }
    }

    void Attack()
    {
        if (!blocking)
        {
            if (attackPending)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    attacking = true;
                    attackPending = false;
                    hit = HitState.Attacking;
                }
            }
            else if (attacking)
            {
                transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, 40, properties.Speed * Time.deltaTime * 1.5f), 0, 0);
                if (transform.localEulerAngles.x > 39 && transform.localEulerAngles.x < 60)
                {
                    attacking = false;
                    if (hit == HitState.Attacking)
                        properties.ResetCombo();
                }
            }
            else
            {
                if (transform.localEulerAngles.x > 345 && transform.localEulerAngles.x < 355)
                {
                    attackPending = true;
                    hit = HitState.Pending;
                }
            }
        }
    }

    void ResetAttack()
    {
        if (hit == HitState.Blocked)
        {
            attacking = false;
            hit = HitState.Pending;
        }
    }
}
