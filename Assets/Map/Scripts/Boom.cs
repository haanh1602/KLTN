using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] FxPool fxDestroy;
    [SerializeField] UbhBullet bullet;

    [SerializeField] bool needRotate;
    [SerializeField] Transform transformCache;

     


    public void Update()
    {
        if (needRotate)
        {
            transformCache.rotation = Quaternion.identity;
        }

    }


    public void BoomDestroy()
    {

        FxPool fx = PoolFxManager._ins.SpawnFx(fxDestroy.gameObject);
        fx.transform.position = transform.position;
        fx.gameObject.SetActive(true);
        UbhObjectPool.instance.ReleaseBullet(bullet);
    }

}
