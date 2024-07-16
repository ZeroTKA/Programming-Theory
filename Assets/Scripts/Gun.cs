using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject HitVFX;
    [SerializeField] GameObject ShootVFX;

    [SerializeField] Camera fpsCam;

    [SerializeField] Transform firePoint;

    [SerializeField] float range = 100;
    [SerializeField] float fireRate;
    [SerializeField] float prevShotTime = 0;

    [SerializeField] int magSize;
    // ammo in the magazine ready to fire
    [SerializeField] int ammoInMag;
    // ammo available to load the magazine with.
    [SerializeField] int ammoInInventory;

    [SerializeField] bool isReloading = false;
    void Update()
    {
        // left mouse button
        //Debug.Log($"{Input.GetMouseButton(0)}");
        if (Input.GetMouseButton(0))
        {
            Shooting();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reloading();
        }

    }

    public void Shooting()
    {

        if(prevShotTime + fireRate < Time.time && ammoInMag > 0 && !isReloading)
        {
            //set new prevShotTime
            Debug.Log("Shooting");
            prevShotTime = Time.time;
            GameObject a = Instantiate(ShootVFX, firePoint.position , Quaternion.identity);
            //fire shot
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.yellow);

                //vfx hookup
                
                GameObject b = Instantiate(HitVFX, hit.point, Quaternion.identity);

                Enemy script = hit.transform.gameObject.GetComponent<Enemy>();

                if (script != null)
                {
                    script.ChangeHealth(-2);
                }
            }
            //lose ammo
            ammoInMag--;
        }
    }


    /// <summary>
    /// edge case where we have 3 ammo in our inventor and we need 10 to fill clip. As we are reloading, we walk over more ammo.
    /// </summary>
    public void Reloading()
    {
        if(!isReloading && ammoInInventory > 0 &&  ammoInMag < magSize)
        {
            Debug.Log("Reloading");
            isReloading = true;
            StartCoroutine(ReloadingAmmo());
        }

    }
    IEnumerator ReloadingAmmo()
    {
        yield return new WaitForSeconds(3);
        int refillAmount = HowManyToRefill(magSize, ammoInMag);
        // if we have enough ammo in inventory
        if(ammoInInventory >= refillAmount)
        {
            ammoInInventory -= refillAmount;
            ammoInMag += refillAmount;
        }
        //if we don't have enough ammo in inventory
        else
        {
            ammoInMag += ammoInInventory;
            ammoInInventory = 0;            
        }
        
        isReloading = false;

    }
    int HowManyToRefill(int magSize, int ammo)
    {
        return magSize - ammo;
    }
}
