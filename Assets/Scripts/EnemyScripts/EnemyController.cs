using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Properites;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public bool attemptAttack;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        attemptAttack = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.destination = player.position;
    }
}




