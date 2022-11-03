using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSpine : BasePerk
{

    [SerializeField] List<GameObject> objSpine;


    public override void Attack()
    {
        if (level <= 0)
        {
            return;
        }
        for(int i = 0; i < objSpine.Count; i++)
        {
            objSpine[i].SetActive(false);
            objSpine[i].transform.DOKill();
        }

        objSpine[level - 1].SetActive(true);
        objSpine[level - 1].transform.DOLocalRotate(new Vector3(0, 0, 90), 180).SetSpeedBased().SetEase(Ease.Linear).SetRelative().SetLoops(-1, LoopType.Incremental);

    }

}
