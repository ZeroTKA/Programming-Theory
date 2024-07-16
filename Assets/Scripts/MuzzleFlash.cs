using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = GameObject.Find("Fire Point").transform;
        
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        gameObject.transform.position = firePoint.position;
        gameObject.transform.rotation = firePoint.rotation;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
