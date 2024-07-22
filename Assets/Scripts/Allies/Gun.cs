using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject HitVFX;
    [SerializeField] GameObject ShootVFX;

    [SerializeField] private AudioClip shootSoundFX;
    [SerializeField] private AudioClip reloadSoundFX;
    [SerializeField] private AudioClip outOfAmmoClickSoundFX;

    [SerializeField] Camera fpsCam;

    [SerializeField] Transform firePoint;

    [SerializeField] float range = 100;
    [SerializeField] float fireRate;
    [SerializeField] float prevShotTime = 0;
    float prevShotClickTime = 0;
    float prevShotClickCD = 3f;
   

    [SerializeField] int magSize;
    // ammo in the magazine ready to fire
    [SerializeField] int ammoInMag;
    // ammo available to load the magazine with.
    [SerializeField] int ammoInInventory;

    [SerializeField] private bool isReloading = false;
    [SerializeField] private bool isOutOfAmmo = false;
    [SerializeField] private bool hasButtonBeenUp = true;
    [SerializeField] GameObject _escapeMenu;
    public static Gun instance;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (_escapeMenu.gameObject.activeSelf == false)
        {
            // left mouse button
            //Debug.Log($"{Input.GetMouseButton(0)}");
            if (Input.GetMouseButton(0))
            {
                Shooting();
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (!hasButtonBeenUp)
                {
                    hasButtonBeenUp = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reloading();
            }
        }



    }

    private void Shooting()
    {        
        //check to see if we are empty and play sound
        if (isOutOfAmmo && !isReloading && prevShotClickTime + prevShotClickCD  < Time.time && hasButtonBeenUp)
        {
            SoundManager.instance.PlaySoundFXClip(outOfAmmoClickSoundFX, firePoint, .3f);
            prevShotClickTime = Time.time;
            hasButtonBeenUp = false;
        }
        //check to see if we can shoot
        if(prevShotTime + fireRate < Time.time && ammoInMag > 0 && !isReloading)
        {
            //set new prevShotTime
            prevShotTime = Time.time;
            PoolManager.SpawnObject(ShootVFX, firePoint.position , Quaternion.identity, PoolManager.PoolEmpty.VFX);
            SoundManager.instance.PlaySoundFXClip(shootSoundFX, firePoint, .2f);

            //if you hit something
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.yellow);
                
                PoolManager.SpawnObject(HitVFX, hit.point, Quaternion.identity,PoolManager.PoolEmpty.VFX);

                Enemy script = hit.transform.gameObject.GetComponent<Enemy>();

                if (script != null)
                {
                    script.ChangeHealth(-5);
                }
            }
            //lose ammo
            ammoInMag--;
        }

        // if we are empty make sure sound doesn't play because we are holding button down.
        if (ammoInMag == 0)
        {
            isOutOfAmmo = true;
            hasButtonBeenUp = false;
        }
    }


    /// <summary>
    /// edge case where we have 3 ammo in our inventor and we need 10 to fill clip. As we are reloading, we walk over more ammo.
    /// </summary>
    public void Reloading()
    {
        if(!isReloading && ammoInInventory > 0 &&  ammoInMag < magSize)
        {
            isReloading = true;
            StartCoroutine(ReloadingAmmo());
        }

    }
    IEnumerator ReloadingAmmo()
    {
        SoundManager.instance.PlaySoundFXClip(reloadSoundFX, firePoint, .2f);
        yield return new WaitForSeconds(2.76f);
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
        isOutOfAmmo = false;

    }
    int HowManyToRefill(int magSize, int ammo)
    {
        return magSize - ammo;
    }

    public void RestartGameForGun()
    {
        prevShotTime = 0;
        prevShotClickCD = 0;
        isOutOfAmmo = false;
        isReloading = false;
        ammoInMag = magSize;
        ammoInInventory = 1000;
    }
}
