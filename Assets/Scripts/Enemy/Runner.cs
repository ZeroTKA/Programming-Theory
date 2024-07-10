using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// This whole file is Inheritance
public class Runner : Enemy
{
    // This is Polymorphism //////////////////////////
 public override void SetSpeed(float s)
    {
        Speed = 6;
        Agent.speed = Speed;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
