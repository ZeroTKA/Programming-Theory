using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Runner : Enemy
{
    public new int health = 10;
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
   
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }

    public void DealDamage(int damage)
    {
        Debug.Log($"Started with {health} health");
        health -= damage;
        Debug.Log($"Took {damage} damge. {health} health is remaining");

        if (health <= 0)
        {
            Destroy(this.gameObject);

        }
    }
}
