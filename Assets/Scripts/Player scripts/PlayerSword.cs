using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Properites;

public class PlayerSword : MonoBehaviour
{
    public bool blocking;
    public bool attacking;
    public bool attackPending;
    Transform arm;
    public PlayerSwordControl sword;
    Rigidbody rb;

    protected void Start()
    {
        attacking = false;
        attackPending = true;
        blocking = false;
        arm = transform.GetChild(0);
        Physics.IgnoreCollision(GetComponentInChildren<Collider>(), transform.parent.GetComponent<Collider>());
        rb = GetComponent<Rigidbody>();
    }

<<<<<<< HEAD:Assets/Scripts/Player scripts/PlayerSword.cs
    public void Block()
=======
    void FixedUpdate()
    {
        if (transform.parent.tag == "Player")
        {
            Block();
            Attack();
        }
    }

    void Block()
>>>>>>> 3db1b9d20b32b02f85b38c8f9a360fb9a7ed3eec:Assets/Scripts/SwordMovement.cs
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            blocking = true;
            if (attacking)
            {
                attacking = false;
                attackPending = true;
            }
            //transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, -30, sword.Speed * Time.deltaTime), 0, 0);
            transform.Rotate(transform.right, 4 * Time.deltaTime * sword.Speed);
            arm.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arm.localEulerAngles.z, 90, 0.2f));
        }
        else
        {
            blocking = false;
            if (!attacking)
                transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, -10, sword.Speed * Time.deltaTime), 0, 0);
            arm.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arm.localEulerAngles.z, 0, 0.2f));
        }
    }

    public void Attack()
    {
        if (!blocking)
        {
            if (attackPending)
            {
                if (Input.GetKey(KeyCode.Mouse0))
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
                {
                    attacking = false;
                    if (sword.hitState == HitState.Attacking)
                        sword.ResetCombo();
                }
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

    private void FixedUpdate()
    {
        Block();
        Attack();
    }
}
