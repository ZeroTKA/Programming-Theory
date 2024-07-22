using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Assertions.Must;

public class Friendly : MonoBehaviour
{
    [SerializeField] GameObject friendlyHitVFX;
    [SerializeField] GameObject friendlyShootVFX;
    private GameObject target;

    [SerializeField] private AudioClip friendlyShootSoundFX;
    [SerializeField] private AudioClip friendlyReloadSoundFX;

    [SerializeField] Transform friendlyFirePoint;
    private Transform laserStart;
    private Transform laserEnd;
    LineRenderer laserLineRenderer;

    [SerializeField] float range = 100;
    float friendylFireRate;
    [SerializeField] float friendlyPrevShotTime = 0;

    [SerializeField] int friendlyMagSize;
    // ammo in the magazine ready to fire
    [SerializeField] int friendlyAmmoInMag;
    // ammo available to load the magazine with.
    [SerializeField] int friendlyAmmoInInventory;

    [SerializeField] private bool isReloading = false;
    [SerializeField] private bool isLookingForEnemy = false;

    public static Friendly instance;
    private void Awake()
    {
        instance = this;
        friendylFireRate = Random.Range(.2f, .5f);
        laserLineRenderer = GetComponent<LineRenderer>();
        laserLineRenderer.startWidth = .02f;
        laserLineRenderer.endWidth = .02f;
        
    }

    void Update()
    {

        if (TheDirector.instance.State == TheDirector.GameState.Wave)
        {
            if ((target == null || !target.activeSelf) && !isLookingForEnemy)
            {
                Debug.Log("looking for new target");
                laserLineRenderer.enabled = false;
                StartCoroutine(FindTarget());

            }
            else if(target != null && target.activeSelf)
            {
                laserLineRenderer.enabled = true;
                laserLineRenderer.SetPosition(0, friendlyFirePoint.transform.position);
                laserLineRenderer.SetPosition(1, target.transform.position);
                //Debug.DrawRay(friendlyFirePoint.transform.position, target.transform.position - transform.position, Color.green);
                LookAt();
                FiendlyShooting();
            }
        }
        else if(TheDirector.instance.State == TheDirector.GameState.Player && friendlyAmmoInMag != friendlyMagSize)
        {
            friendlyAmmoInMag = friendlyMagSize;
            target = null;
            isLookingForEnemy = false;
        }
  
    }

    private void FiendlyShooting()
    {
        //check to see if we can shoot
        if (friendlyPrevShotTime + friendylFireRate < Time.time && friendlyAmmoInMag > 0 && !isReloading)
        {
            //set new prevShotTime
            friendlyPrevShotTime = Time.time;
            PoolManager.SpawnObject(friendlyShootVFX, new Vector3 (0,0,0), Quaternion.identity, PoolManager.PoolEmpty.VFX);
            SoundManager.instance.PlaySoundFXClip(friendlyShootSoundFX, friendlyFirePoint, .2f);

            //if you hit something
            RaycastHit friendlyHit;
       
            if (Physics.Raycast(friendlyFirePoint.transform.position, target.transform.position - transform.position, out friendlyHit, range))
            {
                Debug.DrawRay(friendlyFirePoint.transform.position, target.transform.position - transform.position * friendlyHit.distance, Color.red);

                PoolManager.SpawnObject(friendlyHitVFX, friendlyHit.point, Quaternion.identity, PoolManager.PoolEmpty.VFX);

                Enemy script = friendlyHit.transform.gameObject.GetComponent<Enemy>();

                if (script != null)
                {
                    script.ChangeHealth(-3);
                }
            }
            //lose ammo
            friendlyAmmoInMag--;
            if(friendlyAmmoInMag == 0)
            {
                Reloading();
            }
        }
    }
    private void LookAt()
    {
        
        transform.parent.transform.LookAt(target.transform.position);
    }

    /// <summary>
    /// edge case where we have 3 ammo in our inventor and we need 10 to fill clip. As we are reloading, we walk over more ammo.
    /// </summary>
    private void Reloading()
    {
        if (!isReloading)
        {
            isReloading = true;
            StartCoroutine(ReloadingAmmo());
        }
    }
    public void RestartGameForFriendly()
    {
        friendlyAmmoInMag = friendlyMagSize;
        target = null;
        isLookingForEnemy = false;
        friendylFireRate = Random.Range(.2f, .5f);
        laserLineRenderer.enabled = false;
    }
    IEnumerator ReloadingAmmo()
    {
        SoundManager.instance.PlaySoundFXClip(friendlyReloadSoundFX, friendlyFirePoint, .2f);
        yield return new WaitForSeconds(2.76f);
        friendlyAmmoInMag = friendlyMagSize;        
        isReloading = false;
    }
    IEnumerator FindTarget()
    {
        isLookingForEnemy = true;
        yield return new WaitForSeconds(.3f);
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemies != null && allEnemies.Length > 0)
        {
            target = allEnemies[Random.Range(0, allEnemies.Length - 1)];
            Debug.Log("New target is " + target.name + "which is set to " + target.activeSelf);
            isLookingForEnemy = false;
        }
        else
        {
            isLookingForEnemy = false;
        }
    }
}
