using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactiveCoRoutine : MonoBehaviour
{
    private Coroutine returnToPoolTimerCoroutine;
    
    private void OnEnable()
    {
        returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(2);
        PoolManager.ReturnObjectToPool(gameObject);

    }
}
