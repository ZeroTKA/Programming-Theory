using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerZombie : Enemy
{

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public override void SetSpeed(float s)
    {
        Speed = 1.5f;
        Agent.speed = Speed;
    }
}
