using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Properites;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        attacking = false;
        attackPending = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.destination = player.position;
    }

    #region Sword control
    public const float baseDamage = 15; //Base sword damage
    public const float baseSpeed = 6; //Base speed of sword swing (n swings per 10 seconds)

    public float addedDamage; //Damage added to base damage
    public float addedSpeed; //Speed added to base swing speed
    public float criticalRate; //Rate of critical attacks (%)
    public float criticalDamage; //Critical added damage (%) ((n + 1) * usualDamage)
    public float fireDamage; //Damage over a duration of time (stacks, decreases with time)
    public float fireDuration; //Duration of fire damage

    bool attacking;
    bool attackPending;
    Transform sword;

    public float Speed
    {
        get { return baseSpeed + addedSpeed; }
    }

    public float FinalDamage()
    {
        float damage = baseDamage;
        damage += addedDamage;
        bool isCrit;
        if (criticalRate == 1)
            isCrit = true;
        else
            isCrit = Random.value < criticalRate; //Is the attack a critical
        if (isCrit)
            damage *= 1 + criticalDamage;
        return damage;
    }

    void Attack()
    {

        if (attackPending)
        {
            if (agent.remainingDistance < 4)
            {
                attacking = true;
                attackPending = false;
            }
        }
        else if (attacking)
        {
            transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, 40, Speed * Time.deltaTime * 1.5f), 0, 0);
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

    #endregion



}
