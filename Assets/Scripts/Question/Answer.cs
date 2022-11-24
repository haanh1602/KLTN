using System;
using System.Collections;
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

    public Button Button => button;
    
    public bool IsRight { get; private set; }

    public void Init(string content, bool isRightAnswer)
    {
        IsRight = isRightAnswer;
        answerTMP.text = content;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(EnemyManager._ins.DeSpawnActiveEnemy);
        button.onClick.AddListener(() => PlayResultAnimation(true));
        if (IsRight)
        {
            button.onClick.AddListener(UIManager.Instance.QuestionController.OnClickRightAnswer);
        }
        else
        {
            button.onClick.AddListener(UIManager.Instance.QuestionController.OnClickWrongAnswer);
        }
    }

    public void PlayResultAnimation(bool isClicked)
    {
        if (isClicked)
        {
            imageDotAnimation.DORestartById("answer_on_click");
            imageDotAnimation.DORestartById(IsRight ? "answer_right_click" : "answer_wrong_click");
            StartCoroutine(IEDelayShowContinueButton());
        }
        else if (IsRight)
        {
            StartCoroutine(IEDelayShowRightAnswer());
        }
    }

    IEnumerator IEDelayShowRightAnswer()
    {
        yield return new WaitForSeconds(0.5f);
        imageDotAnimation.DORestartById("answer_right_click");
    }
    
    IEnumerator IEDelayShowContinueButton()
    {
        yield return new WaitForSeconds(1.5f);
        // TODO: LOG
        Debug.Log("Delay show continue button");
        UIManager.Instance.ShowContinueButton();
    }
}
