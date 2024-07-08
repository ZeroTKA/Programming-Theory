using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BiggerZombie : Enemy
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        health = 15;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RunTowardTarget(player);
    }
}
