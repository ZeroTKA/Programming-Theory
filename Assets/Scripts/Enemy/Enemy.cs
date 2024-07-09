using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] float MaxHealth;
    NavMeshAgent Agent;
    Transform Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        SetHealth();
    }

    protected void Move()
    {
        Agent.destination = Player.position;
    }
    public void ChangeHealth(float change)
    {
        Health += change;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    protected void SetHealth()
    {
        Health = MaxHealth;
    }
}


