using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFlash : MonoBehaviour
{
    public bool OnlyDeactivate;

    void OnEnable()
    {
        StartCoroutine("CheckIfAlive");
    }

    IEnumerator CheckIfAlive()
    {
        yield return new WaitForSeconds(.15f);
        gameObject.SetActive(false);
    }
}
