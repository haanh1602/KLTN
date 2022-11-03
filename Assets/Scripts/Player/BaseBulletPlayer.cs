using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletPlayer : MonoBehaviour
{
    [SerializeField] UbhBullet ubhBullet;
    [SerializeField] FxPool fxDestroy;
    public long power;
    public bool isSpreadDmg;
    public bool isDestroyOutScreen = true;
    public void DestroyBullet()
    {
        if(fxDestroy!=null)
        {
            FxPool fx = PoolFxManager._ins.SpawnFx(fxDestroy.gameObject);
            fx.transform.position = transform.position;
            fx.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
        if(ubhBullet!=null)
            UbhObjectPool.instance.ReleaseBullet(ubhBullet);
    }

    public void Update()
    {
        if (!isDestroyOutScreen) return;
        Vector3 pos = transform.position;
        if(pos.x<CheckCamSize._ins.bottomLeft.x-2||
           pos.x > CheckCamSize._ins.topRight.x + 2||
           pos.y < CheckCamSize._ins.bottomLeft.y - 2||
           pos.y > CheckCamSize._ins.topRight.y + 2)
        {
            UbhObjectPool.instance.ReleaseBullet(ubhBullet);
        }
    }
}
