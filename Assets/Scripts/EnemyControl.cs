using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    float health;
    float poison;

    Transform sword;
    bool attpending;
    public bool attacking;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        health = 60;
        poison = 0;
        sword = transform.GetChild(0);
        attpending = true;
        attacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.destination = player.transform.position;
        Attack();
    }

    private void Update()
    {
        health -= poison * Time.deltaTime;
        if (health < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Sword"))
        {
            SwordController sword = other.transform.parent.GetComponent<SwordController>();
            if (sword.attacking)
            {
                SwordProperties properties = sword.properties;
                health -= properties.GetDamage;
                poison = properties.GetPoison;
            }
        }
    }

    void Attack()
    {
        if (agent.remainingDistance < 3)
            if (!attacking && attpending)
            {
                attacking = true;
            }
        if (attacking)
        {
            attpending = false;
            sword.localEulerAngles = new Vector3(Mathf.LerpAngle(sword.localEulerAngles.x, 90, 0.25f), 0, 0);
            if (sword.localEulerAngles.x > 89)
                attacking = false;
        }
        else if (!attpending)
        {
            if (sword.localEulerAngles.x > 1)
                sword.localEulerAngles = new Vector3(Mathf.LerpAngle(sword.localEulerAngles.x, 0, 0.15f), 0, 0);
            else
                attpending = true;
        }
    }
}
