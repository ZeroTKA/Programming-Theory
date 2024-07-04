using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The whole purpose of this is to kill the thing after certain time. Can Probably Delete.
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
