using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] float MaxHealth;

    // This is Encapsulation ///////////////////
    private float m_Speed = 3.5f;
    public float Speed
    {
        get { return m_Speed; }
        set {
            if (value < 0.0f)
            {

            }
            else
            {
                m_Speed = value;
            }
        }                
    }
    protected NavMeshAgent Agent;
    Transform Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        SetHealth();
    }
    private void OnEnable()
    {
        VariablesToResetOnEnable();
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
            gameObject.SetActive(false);
        }
    }

    protected void VariablesToResetOnEnable()
    {
        //This is Abstraction ///////////////////////////
        SetHealth();
        SetSpeed(Speed);
    }
    protected void SetHealth()
    {
        Health = MaxHealth;
    }

    public virtual void SetSpeed(float s)
    {
        Agent.speed = s;
    }
}


