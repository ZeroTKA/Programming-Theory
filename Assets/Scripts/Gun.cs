using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Gun : MonoBehaviour
{
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform firePoint;
    [SerializeField] float range = 100;
    [SerializeField] float fireRate;
    [SerializeField] float prevShotTime = 0;
    [SerializeField] int maxClipSize;
    // ammo in the clip ready to fire
    [SerializeField] int currentClipAmmo;
    // ammo available to load the clip with.
    [SerializeField] int ammoStock;
    [SerializeField] bool isReloading = false;
    

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reloading();
        }

    }

    public void Shooting()
    {
        if(prevShotTime + fireRate < Time.time && currentClipAmmo > 0 && !isReloading)
        {
            //set new prevShotTime
            prevShotTime = Time.time;
            Debug.Log("Shooting");

            //fire shot
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.yellow);

                //vfx hookup
                //GameObject a = Instantiate(FireVFX, firePoint.position , Quaternion.identity);
                //GameObject b = Instantiate(HitVFX, hit.point, Quaternion.identity);

                Runner script = hit.transform.gameObject.GetComponent<Runner>();

                if (script != null)
                {
                    script.ChangeHealth(-2);
                }
            }
            //lose ammo
            currentClipAmmo--;
        }
        

    }
    public void Reloading()
    {
        if(!isReloading && ammoStock > 0 &&  currentClipAmmo < maxClipSize)
        {
            Debug.Log("Reloading");
            isReloading = true;
            StartCoroutine(ReloadingAmmo());
        }

    }
    IEnumerator ReloadingAmmo()
    {
        yield return new WaitForSeconds(3);
        int refillAmount = HowManyToRefill(maxClipSize, currentClipAmmo);
        if(ammoStock >= refillAmount)
        {
            ammoStock -= refillAmount;
            currentClipAmmo += refillAmount;
        }
        else
        {
            currentClipAmmo += ammoStock;
            ammoStock = 0;            
        }
        
        isReloading = false;

    }
    int HowManyToRefill(int clipSize, int clipAmmo)
    {
        return maxClipSize - currentClipAmmo;
    }
}
