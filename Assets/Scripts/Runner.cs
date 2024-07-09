using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Runner : Enemy
{
    private NavMeshAgent agent;
    private Transform player;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 5;        
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }
}
