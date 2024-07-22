using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyMuzzleFlash : MonoBehaviour
{
    GameObject friendlyFirePoint;
    // Start is called before the first frame update'
    void OnEnable()
    {
        friendlyFirePoint = GameObject.Find("fireSpot");
        StartCoroutine(Destroy());
    }
    private void Update()
    {
        gameObject.transform.position = friendlyFirePoint.transform.position;
        gameObject.transform.rotation = friendlyFirePoint.transform.rotation;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(.2f);
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
