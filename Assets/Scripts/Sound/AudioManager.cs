using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public const string KEY_GLOBAL_VOL_MUSIC = "Global_Vol_Music";
    public const string KEY_GLOBAL_VOL_SOUND = "Global_Vol_Sound";
    
    [Header("Music")]
    public AudioClip music;

    [Header("Music in game")] 
    public AudioClip musicInGame;

    [Header("Sound")] 
    public AudioClip[] sounds;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        //Get Setting Audio
        EazySoundManager.GlobalMusicVolume = PlayerPrefs.GetFloat(KEY_GLOBAL_VOL_MUSIC, 1);
        EazySoundManager.GlobalSoundsVolume = PlayerPrefs.GetFloat(KEY_GLOBAL_VOL_SOUND, 1);
        
        PlayThemeMusic();
    }

    public void PlaySound(int indexSound)
    {
        if (indexSound > sounds.Length - 1)
        {
            return;
        }
        EazySoundManager.PlaySound(sounds[indexSound], EazySoundManager.GlobalSoundsVolume);
    }

    public void PlayOnClick()
    {
        EazySoundManager.PlaySound(sounds[0], EazySoundManager.GlobalSoundsVolume);
    }

    public void PlayRightAnswer()
    {
        EazySoundManager.PlaySound(sounds[1], EazySoundManager.GlobalSoundsVolume);
    }
    
    public void PlayWrongAnswer()
    {
        EazySoundManager.PlaySound(sounds[2], EazySoundManager.GlobalSoundsVolume);
    }

    public void PlayInGameMusic()
    {
        EazySoundManager.PlayMusic(musicInGame, EazySoundManager.GlobalMusicVolume, true, false, 0.5f, 0.5f);
    }

    public void PlayThemeMusic()
    {
        EazySoundManager.PlayMusic(music, EazySoundManager.GlobalMusicVolume, true, false, 0.5f, 0.5f);
    }

    public void StopAllMusic()
    {
        EazySoundManager.StopAllMusic(0.5f);
    }

    public void SetVolumeMusic(float vol)
    {
        PlayerPrefs.SetFloat(KEY_GLOBAL_VOL_MUSIC, vol);
        EazySoundManager.GlobalMusicVolume = vol;
    }
    
    public void SetVolumeSound(float vol)
    {
        PlayerPrefs.SetFloat(KEY_GLOBAL_VOL_SOUND, vol);
        EazySoundManager.GlobalSoundsVolume = vol;
    }

    private void OnDestroy()
    {
        EazySoundManager.StopAll(0.5f);
    }
}
