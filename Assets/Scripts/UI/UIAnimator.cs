using DG.Tweening;
using UnityEngine;
// ReSharper disable CommentTypo

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation questionPanelDOT;
    
    public void ShowQuestion()
    {
        questionPanelDOT.DORestartAllById("show_question");
        Debug.Log("Show Question");
    }

    public void HideQuestion()
    {
        questionPanelDOT.DORestartAllById("hide_question");
        Debug.Log("Hide Question");
    }

    public void ShowContinueButton()
    {
        questionPanelDOT.DORestartAllById("show_continue");
        Debug.Log("Show continue button");
    }
    
    /// <summary>
    /// Chạy animation khi thay đổi score text
    /// </summary>
    public void OnChangeScoreText()
    {
        // TODO: Run animation on changing score text
    }

    /// <summary>
    /// Chạy animation khi thay đổi questionTh text
    /// </summary>
    public void OnChangeQuestionThText()
    {
        // TODO: Run animation on changing questionTh text
    }
}
