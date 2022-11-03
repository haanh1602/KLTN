using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBumerang : MonoBehaviour
{
    [SerializeField] List<Transform> listBumerang;

    private void OnEnable()
    {
        for(int i = 0; i < listBumerang.Count; i++)
        {
            listBumerang[i].DOLocalRotate(new Vector3(0, 0, 90), 180).SetEase(Ease.Linear).SetRelative().SetSpeedBased().SetLoops(-1, LoopType.Incremental);
        }
    }

}
