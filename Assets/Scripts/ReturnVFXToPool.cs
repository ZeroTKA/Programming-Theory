using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnVFXToPool : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
            yield return new WaitForSeconds(2.5f);
            PoolManager.ReturnObjectToPool(gameObject);
    }
}
