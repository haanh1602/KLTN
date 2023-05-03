using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public TextMeshProUGUI txtGold;
    
    public Button btnStart;
    public Button btnDailyGift;
    public Button btnMailBox;
    public Button btnSetting;

    [Header("UI")] 
    public GameObject UICharacter;
    public GameObject UIDailyGift;
    public GameObject UIMailBox;
    public GameObject UISetting;
    public GameObject UILoading;

    private void Awake()
    {
        txtGold.SetText($"{PlayerPrefs.GetInt(Constant.PrefKeys.KEY_GOLD, 0)}");
        
        //Global Setting Game
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
        
        btnStart.onClick.RemoveAllListeners();
        btnStart.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(0);
            //StartCoroutine(_IELoadingInGame("Level"));
            SceneController.Instance.LoadScene("Level");
        });
        
        btnDailyGift.onClick.RemoveAllListeners();
        btnDailyGift.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(0);
            UIDailyGift.SetActive(true);
        });
        
        btnMailBox.onClick.RemoveAllListeners();
        btnMailBox.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(0);
            UIMailBox.SetActive(true);
        });
        
        btnSetting.onClick.RemoveAllListeners();
        btnSetting.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(0);
            UISetting.SetActive(true);
        });
        
        AudioManager.Instance.PlayThemeMusic();
    }
    
    private IEnumerator _IELoadingInGame(string sceneName)
    {
        UILoading.SetActive(true);
        yield return new  WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void OnStartGame(int level)
    {
        Debug.LogWarning("____ Starting level " + level);
        AudioManager.Instance.PlayOnClick();
        //StartCoroutine(_IELoadingInGame("Level"));
        SceneController.Instance.LoadScene("Level");
        AudioManager.Instance.StopAllMusic();
        GameData.Instance.PlayingLevel = level;
    }

    public void PlayOnClickSound()
    {
        AudioManager.Instance.PlayOnClick();
    }
}
