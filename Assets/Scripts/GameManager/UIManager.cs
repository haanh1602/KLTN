using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private BasePlayer player;
    [SerializeField] private UIAnimator uiAnimator;
    [SerializeField] private QuestionController questionController;
    [SerializeField] private MyTextMeshProUGUI scoreTMP;
    [SerializeField] private MyTextMeshProUGUI questionIndexTMP;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI continueButtonTMP;
    [SerializeField] private UIEndgamePanel endgamePanel;
    [SerializeField] private UIPausePanel pausePanel;
    [SerializeField] private Button pauseBtn;

    public QuestionController QuestionController => questionController;
    public UIAnimator UIAnimator => uiAnimator;
    [SerializeField] private Animator animPlayer;

    protected override void Awake()
    {
        base.Awake();
        scoreTMP.OnChangeText += uiAnimator.OnChangeScoreText;
        questionIndexTMP.OnChangeText += uiAnimator.OnChangeQuestionThText;
        continueButton.onClick.AddListener(OnContinueButtonClick);
        pauseBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayOnClick();
            pausePanel.OnShow();
        });
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
        if (GameManager.Instance.QuestIndex == GameManager.Instance.MaxQuest || !GameManager.Instance.Pass)
        {
            GameManager.Instance.Player.HideWarning();
            player.HideEnemyDirect();
            continueButtonTMP.text = "KẾT THÚC HÀNH TRÌNH";
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayOnClick();
                
                OnFinishGameClick();
                //continueButton.onClick.RemoveAllListeners();
            });
        }
        UIAnimator.ShowContinueButton();
    }

    private void OnContinueButtonClick()
    {
        AudioManager.Instance.PlayOnClick();
        
        animPlayer.SetBool("isCrying", false);
        animPlayer.SetBool("isSmiling", false);
        HideQuestion();
    }

    public void AnswerQuestion(bool check)
    {
        if (check) animPlayer.SetBool("isSmiling", true);
        else animPlayer.SetBool("isCrying", true);
    }

    private void OnFinishGameClick()
    {
        endgamePanel.Init(GameManager.Instance.Pass, GameManager.Instance.RightAnswers, GameManager.Instance.WrongAnswers, 
            GameManager.Instance.TimeLapse, GameManager.Instance.Score);
        endgamePanel.Show();
    }
}
