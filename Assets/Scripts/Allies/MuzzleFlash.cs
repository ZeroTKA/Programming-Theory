using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    Transform FirePoint;
    // Start is called before the first frame update'
    void OnEnable()
    {
        FirePoint = GameObject.Find("Fire Point").transform;
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        gameObject.transform.position = FirePoint.position;
        gameObject.transform.rotation = FirePoint.rotation;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(.2f);
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
