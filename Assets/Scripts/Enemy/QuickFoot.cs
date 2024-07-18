using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickFoot : Enemy
{
    [SerializeField] AudioClip annoucementSceram;

    private void Update()
    {
        Move();
    }
    public override void SetSpeed(float s)
    {
        Speed = 8.5f;
        Agent.speed = Speed;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        int random = Random.Range(0, 100);
        if (20 >= random)
        {
            SoundManager.instance.PlaySoundFXClip(annoucementSceram, transform, .4f);
        }
    }
}
