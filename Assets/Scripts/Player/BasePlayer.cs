using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public static BasePlayer _ins;

    [SerializeField] private DOTweenAnimation warningDOT;
    
    public float exp;
    public int level = 1;

    public void Awake()
    {
        _ins = this;
    }

    public void LevelUp()
    {
        level++;
    }
    public void Die()
    {
        UImanager._ins.GameOver();
    }

    public void ShowWarning()
    {
        if (showWarningCoroutine == null) showWarningCoroutine = StartCoroutine(IEShowWarning());
    }

    public void HideWarning()
    {
        if (showWarningCoroutine != null) StopCoroutine(showWarningCoroutine);
        if (_isShowingWarning)
        {
            warningDOT.DORestartById("warning_close");
            _isShowingWarning = false;
        }
    }

    private Coroutine showWarningCoroutine = null;
    IEnumerator IEShowWarning()
    {
        yield return new WaitUntil(() => !GameManager.IsAnswering);
        yield return new WaitForSeconds(0.5f);
        if (GameManager.IsAnswering) yield break;   // Nếu đang trả lời câu mới thì tắt
        warningDOT.DORestartById("warning_open");
        _isShowingWarning = true;
        yield return new WaitForSeconds(8f);
        if (_isShowingWarning) warningDOT.DORestartById("warning_close");
        _isShowingWarning = false;
    }

    private bool _isShowingWarning = false;
}
