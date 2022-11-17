using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation dotAnimation;
    [SerializeField] private DOTweenAnimation imageDotAnimation;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI answerTMP;
    [SerializeField] private Image answerImage;
    
    public bool IsRight { get; private set; }

    public void Init(string content, bool isRightAnswer)
    {
        IsRight = isRightAnswer;
        answerTMP.text = content;
        button.onClick.RemoveAllListeners();
        if (IsRight)
        {
            button.onClick.AddListener(GameManager.Instance.OnRightClicked);
        }
        else
        {
            button.onClick.AddListener(GameManager.Instance.OnWrongClicked);
        }
        button.onClick.AddListener(EnemyManager._ins.DeSpawnActiveEnemy);
        button.onClick.AddListener(PlayResultAnimation);
    }

    public void PlayResultAnimation()
    {
        imageDotAnimation.DORestartById("answer_on_click");
        imageDotAnimation.DORestartById(IsRight ? "answer_right_click" : "answer_wrong_click");
    }
}
