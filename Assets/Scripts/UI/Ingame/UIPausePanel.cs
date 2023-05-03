using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePanel : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation dotContinueBtn;
    [SerializeField] private DOTweenAnimation dotExitBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button exitBtn;

    private void Awake()
    {
        continueBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayOnClick();
            Continue();
        });
        
        exitBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayOnClick();
            Exit();
        });
    }

    void Continue()
    {
        Time.timeScale = 1f;
        OnClose();
    }

    void Exit()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.StopAllMusic();
        SceneController.Instance.LoadScene("Menu");
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
        dotContinueBtn.DORestartById("pause_on_show");
        dotExitBtn.DORestartById("pause_on_show");
        Time.timeScale = 0f;
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }
}
