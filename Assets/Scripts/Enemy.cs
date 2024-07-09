using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static float health = 10;

    public void DealDamage(int damage)
    {
        health -= damage;

        if (health < 0 )
        {
            Destroy(this.gameObject);
        }
    }

    public void HealthUpdate(int healthChange)
    {
        health += healthChange;
    }


}
