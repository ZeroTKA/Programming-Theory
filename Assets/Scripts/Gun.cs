using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    public void Shooting()
    {
        RaycastHit hit;
        if(Physics.Raycast(firePoint.position, transform.TransformDirection(Vector3.up), out hit, 100))
        {
            Debug.DrawRay(firePoint.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
        }
    }
}
