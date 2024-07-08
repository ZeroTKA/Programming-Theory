using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Runner : Enemy
{
    public Transform player;
   
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RunTowardTarget(player);
    }
}
