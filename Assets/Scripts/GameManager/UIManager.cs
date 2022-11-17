using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIAnimator uiAnimator;
    [SerializeField] private QuestionController questionController;
    [SerializeField] private MyTextMeshProUGUI scoreTMP;
    [SerializeField] private MyTextMeshProUGUI questionIndexTMP;

    public QuestionController QuestionController => questionController;
    public UIAnimator UIAnimator => uiAnimator;

    private void Awake()
    {
        scoreTMP.OnChangeText += uiAnimator.OnChangeScoreText;
        questionIndexTMP.OnChangeText += uiAnimator.OnChangeQuestionThText;
    }

    public void SetScore(int score)
    {
        scoreTMP.text = score.ToString();
    }

    public void SetQuestionTh(int questionIndex, int maxQuest)
    {
        questionIndexTMP.text = $"{questionIndex}/{maxQuest}";
    }

    public void ShowQuestion()
    {
        UIAnimator.ShowQuestion();
    }

    public void HideQuestion()
    {
        UIAnimator.HideQuestion();
    }
}
