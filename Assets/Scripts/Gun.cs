using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    public Transform firePoint;
    public float range = 100;
    //public GameObject FireVFX;
    //public GameObject HitVFX;

    // Update is called once per frame
    void Update()
    {
        // left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
    }

    public void Shooting()
    {

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.yellow);

            //vfx hookup
            //GameObject a = Instantiate(FireVFX, firePoint.position , Quaternion.identity);
            //GameObject b = Instantiate(HitVFX, hit.point, Quaternion.identity);

            Enemy script = hit.transform.gameObject.GetComponent<Enemy>();
            if (script != null)
            {
                script.ChangeHealth(-2);
            }
        }
    }
}
