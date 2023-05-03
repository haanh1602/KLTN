using System;
using TMPro;
using UnityEngine;

public class NotifyUI : Singleton<NotifyUI>
{
    [SerializeField] private Animator loadingGO;
    [SerializeField] private GameObject fullscreenGO;
    [SerializeField] private Animator toastAnimator;
    [SerializeField] private TextMeshProUGUI toastTMP;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (loadingGO) loadingGO.gameObject.SetActive(false);
        if (fullscreenGO) DisableFullScreen();
    }

    public void ShowLoading()
    {
        loadingGO.gameObject.SetActive(true);
        loadingGO.Play("loading_fadein", 0,0f);
    }

    public void DisableLoading()
    {
        loadingGO.Play("loading_fadeout", 0,0f);
    }
    
    public void ShowFullScreen()
    {
        fullscreenGO.gameObject.SetActive(true);
    }

    public void DisableFullScreen()
    {
        fullscreenGO.gameObject.SetActive(false);
    }

    public void ShowToast(string content)
    {
        toastTMP.text = content;
        toastAnimator.gameObject.SetActive(true);
        toastAnimator.Play("LockingToast", 0, 0f);
    }
}
