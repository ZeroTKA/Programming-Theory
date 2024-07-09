
using UnityEngine;
using UnityEngine.AI;

public class BiggerZombie : Enemy
{
    public Transform player;
    protected static NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        health = 15;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }
}
