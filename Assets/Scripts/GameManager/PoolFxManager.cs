using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFxManager : MonoBehaviour
{
    public static PoolFxManager _ins;

    public List<FxPool> fxPools;


    public Transform cacheFx;

    public void Awake()
    {
        _ins = this;
    }
    public FxPool SpawnFx(GameObject obj)
    {
        FxPool fxPool = obj.GetComponent<FxPool>();
        if (fxPool != null)
        {
            for(int i = 0; i < fxPools.Count; i++)
            {
                if (fxPool.idPool == fxPools[i].idPool)
                {
                    fxPool = fxPools[i];
                    fxPools.RemoveAt(i);
                    return fxPool;
                }
            }

            // ko co trong pool;

            GameObject fxObj= Instantiate(obj, cacheFx);
            fxObj.SetActive(false);
            return fxObj.GetComponent<FxPool>();
        }
        else
        {
            Debug.LogError("obj name is not FxPool");

        }

        return null;
    }
    

    public void AddToPool(FxPool fxPool)
    {
        fxPools.Add(fxPool);
    }
}
