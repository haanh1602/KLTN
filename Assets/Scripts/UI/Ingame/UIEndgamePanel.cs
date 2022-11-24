using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndgamePanel : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation doTweenAnimation;
    [SerializeField] private TextMeshProUGUI rightAnswersTMP;
    [SerializeField] private TextMeshProUGUI wrongAnswersTMP;
    [SerializeField] private TextMeshProUGUI timeTMP;
    [SerializeField] private TextMeshProUGUI rewardTMP;
    [SerializeField] private Button goHomeButton;

    private void Awake()
    {
        goHomeButton.onClick.AddListener(OnGoHomeClick);
        gameObject.SetActive(false);
    }

    public void Init(int rightAnswer, int wrongAnswers, int timeBySecond, int reward)
    {
        rightAnswersTMP.text = $"SỐ CÂU ĐÚNG: <color=#2DFF26>{rightAnswer}</color>";
        wrongAnswersTMP.text = $"SỐ CÂU SAI: <color=#FF2626>{wrongAnswers}</color>";
        timeTMP.text = "THỜI GIAN HOÀN THÀNH:\n" + timeBySecond / 60 + " phút " + timeBySecond % 60 + " giây";
        rewardTMP.text = "PHẦN THƯỞNG:  " + reward;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        doTweenAnimation.DORestartAllById("show_game_result");
    }
    
    private void OnGoHomeClick()
    {
        Debug.Log("On go home clicked!");
    }
}
