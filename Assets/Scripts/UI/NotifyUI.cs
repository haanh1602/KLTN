using System;
using UnityEngine;

public class NotifyUI : Singleton<NotifyUI>
{
    [SerializeField] private Animator loadingGO;
    [SerializeField] private GameObject fullscreenGO;
    
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
}
