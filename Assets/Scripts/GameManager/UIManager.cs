using System;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public QuestionController questionController;
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI questionThTMP;

    public void NextQuest(QuestionData questionData)
    {
        //questionController.Init(questionData);
        //questionThTMP.text = GameManager.Instance.QuestTh + " / " + GameManager.Instance.MaxQuest;
    }

    public void RefreshScore()
    {
        scoreTMP.text = GameManager.Instance.Score.ToString();
    }
}
