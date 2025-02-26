using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    Transform heartPosition;

    private void Awake()
    {
        heartPosition = GameObject.Find("Heart").transform;
        Agent = GetComponent<NavMeshAgent>();
    }
    public virtual void OnEnable()
    {
        VariablesToResetOnEnable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Heart")
        {
            Heart.instance.ChangeHealth(DealDamage());
        }
    }

    protected void Move()
    {
        Agent.destination = heartPosition.position;
    }
    public void ChangeHealth(float change)
    {
        Health += change;

        if (Health <= 0)
        {
            int random = Random.Range(0, 100);
            if(random <= 5)
            {
                SoundManager.instance.PlaySoundFXClip(SoundManager.instance.death, transform, .3f);
            }
           

            
            PoolManager.ReturnObjectToPool(gameObject);
            //gameObject.SetActive(false);
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
    public virtual int DealDamage()
    {
        return -7;
    }
}


