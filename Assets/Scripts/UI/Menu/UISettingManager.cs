using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.UI;

public class UISettingManager : MonoBehaviour
{
    public Slider switchBMG;
    public Slider switchSFX;
    public Button btnBack;
    
    private void Awake()
    {
        btnBack.onClick.RemoveAllListeners();
        btnBack.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(0);
            gameObject.SetActive(false);
        });
    }

    private void Start()
    {
        AudioManager.Instance.SetVolumeMusic(PlayerPrefs.GetFloat(AudioManager.KEY_GLOBAL_VOL_MUSIC, 1));
        AudioManager.Instance.SetVolumeSound(PlayerPrefs.GetFloat(AudioManager.KEY_GLOBAL_VOL_SOUND, 1));
        
        switchBMG.onValueChanged.AddListener(value =>
        {
            AudioManager.Instance.SetVolumeMusic(value);
        });
        
        switchSFX.onValueChanged.AddListener(value =>
        {
            AudioManager.Instance.SetVolumeSound(value);
        });
    }
}
