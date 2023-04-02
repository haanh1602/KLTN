using System;
using com.ootii.Messages;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BonusPointUIFx : MonoBehaviour
{
    private int _point = 0;
    public RectTransform RectTransform;
    [SerializeField] private TextMeshProUGUI pointTmp;
    [SerializeField] private DOTweenAnimation pointDOT;

    private static Transform destinationTransform = null;

    private bool moving = false;

    public Action<BonusPointUIFx> OnComplete = null;

    private void Awake()
    {
        if (!RectTransform) RectTransform = GetComponent<RectTransform>();
    }

    public static Transform Destination
    {
        get
        {
            if (destinationTransform == null)
                destinationTransform = FindObjectOfType<BonusPointUI>().transform;
            return destinationTransform;
        }
    }
    
    public void Init(int point)
    {
        _point = point;
        pointTmp.text = "+ " + _point;
        gameObject.SetActive(true);
        pointDOT.DORestartById("show_bonus_point");
    }

    public void StartMoving()
    {
        moving = true;
    }

    public float moveSpeed = 10f;
    private void Update()
    {
        if (Destination && moving)
        {
            if (Vector3.Distance(transform.position, Destination.position) > 50f)
            {
                transform.position = Vector3.LerpUnclamped(transform.position, Destination.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                OnMovingToDes(_point);
            }
        }
    }

    public void OnMovingToDes(int point)
    {
        MessageDispatcher.SendMessage(this, BonusPointUIMessageKey.AddBonusPoint, point, 0f);
        moving = false;
        gameObject.SetActive(false);
        OnComplete?.Invoke(this);
    }
}
