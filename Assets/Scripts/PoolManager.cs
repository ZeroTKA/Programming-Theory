using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private static GameObject QuickFootGameobjectEmpty;
    private static GameObject EnemeyGameobjectEmpty;
    private static GameObject NoneGameobjectEmpty;
    private static GameObject RunnerGameobjectEmpty;
    private static GameObject VFXGameobjectEmpty;
    private static GameObject SFXGameobjectEmpty;


    private void Awake()
    {
        SetupEmpties();
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolEmpty poolEmpty = PoolEmpty.None)        
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        //if pool doesn't exist, create it.
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // check if there are any inactive objects in pool
        GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();
       

        if (spawnableObject == null)
        {
            //Find the parent and then assign the new Instantiate to it.
            GameObject parentObject = SetParentObject(poolEmpty);           
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            if(parentObject != null)
            {
                spawnableObject.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
         // found an object in the pool, reusing it.   
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }
        return spawnableObject;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        // Remove clone from the OBJ name to match when it was put in the pool.
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        PooledObjectInfo pool = ObjectPools.Find(p =>p.LookupString == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
    public void SetupEmpties()
    {
        SFXGameobjectEmpty = new GameObject("Pooled SFX Empty");
        VFXGameobjectEmpty = new GameObject("Pooled VFX Empty");
        QuickFootGameobjectEmpty = new GameObject("Pooled QuickFoot");
        RunnerGameobjectEmpty = new GameObject("Pooled Runner Empty");
        EnemeyGameobjectEmpty = new GameObject("Pooled Enemy Empty");
        NoneGameobjectEmpty = new GameObject("None Empty");
    }
    public enum PoolEmpty
    {
        QuickFoot,
        SFX,
        VFX,
        Enemies,
        Runner,
        None
    }
    private static GameObject SetParentObject(PoolEmpty p)
    {
        
        switch (p)
        {

            case PoolEmpty.Runner:
                return RunnerGameobjectEmpty;
            case PoolEmpty.Enemies:
                return EnemeyGameobjectEmpty;
            case PoolEmpty.None:
                return NoneGameobjectEmpty;
            case PoolEmpty.SFX:
                return SFXGameobjectEmpty;
            case PoolEmpty.VFX:
                return VFXGameobjectEmpty;
            case PoolEmpty.QuickFoot:
                return VFXGameobjectEmpty;
            default:
                return null;               
        }
    }
    public static PoolEmpty FindPool(GameObject obj)
    {
        
        switch (obj.name)
        {
            case "Runner":
                return PoolEmpty.Runner;
            default:
                return PoolEmpty.None;
        }
    }

}


public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject> ();
}
