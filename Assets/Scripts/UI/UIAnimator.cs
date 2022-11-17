using DG.Tweening;
using UnityEngine;
// ReSharper disable CommentTypo

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation questionPanelDOT;
    
    public void ShowQuestion()
    {
        questionPanelDOT.DORestartAllById("show_question");
    }

    public void HideQuestion()
    {
        questionPanelDOT.DORestartAllById("hide_question");
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
