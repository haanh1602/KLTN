using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIAnimator uiAnimator;
    [SerializeField] private QuestionController questionController;
    [SerializeField] private MyTextMeshProUGUI scoreTMP;
    [SerializeField] private MyTextMeshProUGUI questionIndexTMP;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI continueButtonTMP;
    [SerializeField] private UIEndgamePanel endgamePanel;

    public QuestionController QuestionController => questionController;
    public UIAnimator UIAnimator => uiAnimator;

    private void Awake()
    {
        scoreTMP.OnChangeText += uiAnimator.OnChangeScoreText;
        questionIndexTMP.OnChangeText += uiAnimator.OnChangeQuestionThText;
        continueButton.onClick.AddListener(OnContinueButtonClick);
    }

    public void SetScore(int score)
    {
        scoreTMP.text = score.ToString();
    }

    public void SetQuestionIndex(int questionIndex, int maxQuest)
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

    public void ShowContinueButton()
    {
        if (GameManager.Instance.QuestIndex == GameManager.Instance.MaxQuest)
        {
            continueButtonTMP.text = "KẾT THÚC HÀNH TRÌNH";
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                OnFinishGameClick();
                //continueButton.onClick.RemoveAllListeners();
            });
        }
        UIAnimator.ShowContinueButton();
    }

    private void OnContinueButtonClick()
    {
        HideQuestion();
    }

    private void OnFinishGameClick()
    {
        endgamePanel.Init(GameManager.Instance.RightAnswers, GameManager.Instance.WrongAnswers, 
            GameManager.Instance.TimeLapse, GameManager.Instance.Score);
        endgamePanel.Show();
    }
}
