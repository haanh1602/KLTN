using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPointFxManager : MonoBehaviour
{
    [SerializeField] private BonusPointUIFx bonusPointUIFxPrefab;
    
    [SerializeField] private GameObject fxContainerGO;

    private List<BonusPointUIFx> pooling = new List<BonusPointUIFx>();
    private List<BonusPointUIFx> usingFxes = new List<BonusPointUIFx>();
    
    public void Spawn(int point, Vector3 spawnPosition)
    {
        if (pooling.Count == 0)
        {
            var fx = Instantiate(bonusPointUIFxPrefab, spawnPosition, Quaternion.identity, fxContainerGO.transform);
            fx.OnComplete = OnFxComplete;
            usingFxes.Add(fx);
            fx.Init(point);
        }
        else
        {
            var fx = pooling[0];
            pooling.RemoveAt(0);
            usingFxes.Add(fx);
            fx.transform.position = spawnPosition;
            fx.Init(point);
        }
    }

    private void OnFxComplete(BonusPointUIFx fx)
    {
        usingFxes.Remove(fx);
        pooling.Add(fx);
    }
}
