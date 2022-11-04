using System;
using UnityEngine;

public class NotifyUI : Singleton<NotifyUI>
{
    [SerializeField] private GameObject loadingGO;
    [SerializeField] private GameObject fullscreenGO;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (loadingGO) DisableLoading();
        if (fullscreenGO) DisableFullScreen();
    }

    public void ShowLoading()
    {
        loadingGO.gameObject.SetActive(true);
    }

    public void DisableLoading()
    {
        loadingGO.gameObject.SetActive(false);
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
