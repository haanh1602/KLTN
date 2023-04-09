using System;
using System.Collections;
using System.Collections.Generic;
using com.ootii.Messages;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BonusPointUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bonusPointTMP;
    [SerializeField] private DOTweenAnimation bonusPointDOT;

    private int bonusPoint = 0;

    private void Awake()
    {
        MessageDispatcher.AddListener(BonusPointUIMessageKey.AddBonusPoint, OnGetBonusPoint, true);
    }

    private Queue<int> addPointNotHandleQueue = new Queue<int>(); 

    private void OnGetBonusPoint(IMessage message)
    {
        int point = (int) message.Data;
        addPointNotHandleQueue.Enqueue(point);
        if (gameObject.activeInHierarchy)
        {
            if (addingBonusPointCoroutine == null)
            {
                addingBonusPointCoroutine = StartCoroutine(IEAddBonusPoint());
            }
        }
    }

    private Coroutine addingBonusPointCoroutine;

    IEnumerator IEAddBonusPoint()
    {
        if (addPointNotHandleQueue.Count > 0)
        {
            int point = addPointNotHandleQueue.Dequeue();
            bonusPointDOT.DORestartById("start_adding_point");
            for (int i = 0; i < point; i++)
            {
                yield return new WaitForSeconds(0.05f);
                bonusPoint++;
                bonusPointTMP.text = bonusPoint.ToString();
            }
        }

        if (addPointNotHandleQueue.Count == 0)
        {
            bonusPointDOT.DORestartById("end_adding_point");
            addingBonusPointCoroutine = null;
        }
        else
        {
            addingBonusPointCoroutine = StartCoroutine(IEAddBonusPoint());
        }
    }
}

public class BonusPointUIMessageKey
{
    public const string AddBonusPoint = "ADD_BONUS_POINT";
}
