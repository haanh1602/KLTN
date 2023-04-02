using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPointFxManager : MonoBehaviour
{
    [SerializeField] private BonusPointUIFx bonusPointUIFxPrefab;
    
    [SerializeField] private GameObject fxContainerGO;

    private List<BonusPointUIFx> pooling = new List<BonusPointUIFx>();

    public void Spawn(int point, Vector3 spawnPosition)
    {
        if (pooling.Count == 0)
        {
            var fx = Instantiate(bonusPointUIFxPrefab, spawnPosition, Quaternion.identity, fxContainerGO.transform);
            Vector2 transformInViewPort = Camera.main.WorldToViewportPoint(spawnPosition);
            /*fx.RectTransform.anchorMin = transformInViewPort;
            fx.RectTransform.anchorMax = transformInViewPort;*/
            fx.RectTransform.position = new Vector3(transformInViewPort.x, transformInViewPort.y, 0f);
            fx.OnComplete = OnFxComplete;
            fx.Init(point);
        }
        else
        {
            var fx = pooling[0];
            pooling.RemoveAt(0);
            fx.transform.position = spawnPosition;
            fx.Init(point);
        }
    }

    private void OnFxComplete(BonusPointUIFx fx)
    {
        pooling.Add(fx);
    }
}
