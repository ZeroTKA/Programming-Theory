using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static float health = 20;


    public void HealthUpdate(int healthChange)
    {
        health += healthChange;
    }


}
